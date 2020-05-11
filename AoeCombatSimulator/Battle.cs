using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AoeCombatSimulator
{
    public class Battle
    {
        private Form1 form;
        private int taskId;
        private int battleId;
        public short hitAndRunMode;
        
        public List<Unit>[] armies = new List<Unit>[] { new List<Unit>(), new List<Unit>() };
        public HashSet<Unit>[,] gridUnits = new HashSet<Unit>[22,22];
        private List<Unit> graveyard = new List<Unit>();
        public List<Arrow> arrows = new List<Arrow>();
        public List<Missile> missiles = new List<Missile>();

        public int timeInterval = 0; // 1 time interval = 0,01s
        private int[] unitIndex = new int[2];
        private Random rnd;
        public decimal[,] resourcesGenerated = new decimal[2, 3] { { 0m, 0m, 0m }, { 0m, 0m, 0m } }; // currently only for the Keshik gold generation


        public Battle(Form1 form1, int taskId, int battleId, short hitAndRunMode)
        {
            form = form1;
            this.taskId = taskId;
            this.battleId = battleId;
            this.hitAndRunMode = hitAndRunMode;
            rnd = new Random(taskId * Environment.ProcessorCount + battleId + Environment.TickCount);

            for (int i = 0; i < gridUnits.GetLength(0); i++)
            {
                for (int j = 0; j < gridUnits.GetLength(1); j++)
                {
                    gridUnits[i, j] = new HashSet<Unit>();
                }
            }

            CreateArmys();
            Fight();
            CountSurvivors();
            SaveWinner();
        }
        
        private void CreateArmys()
        {
            int[] army_SizeMelee = new int[2];
            int[] army_MeleeWidth = new int[2];
            int[] army_MeleeHeight = new int[2];
            int[] army_SizeRanged = new int[2];
            int[] army_RangedWidth = new int[2];
            int[] army_RangedHeight = new int[2];
            int[] army_Size = new int[2];
            int[] army_MeleePlaced = new int[2];
            int[] army_RangedPlaced = new int[2];

            for (int i = 0; i < 2; i++)
            {
                army_SizeMelee[i] = AoeData.unitTypesList.Where(ut => ut.attackRange <= 1.0).Sum(ut => form.players[i].amountStartUnits[ut.unitTypeIndex]);
                army_MeleeWidth[i] = (int)Math.Ceiling(Math.Sqrt(army_SizeMelee[i] / 2.0));
                army_MeleeHeight[i] = army_MeleeWidth[i] * 2;
                army_SizeRanged[i] = AoeData.unitTypesList.Where(ut => ut.attackRange > 1.0).Sum(ut => form.players[i].amountStartUnits[ut.unitTypeIndex]);
                army_RangedWidth[i] = (int)Math.Ceiling(Math.Sqrt(army_SizeRanged[i] / 2.0));
                army_RangedHeight[i] = army_RangedWidth[i] * 2;
                army_Size[i] = army_SizeMelee[i] + army_SizeRanged[i];
                army_MeleePlaced[i] = 0;
                army_RangedPlaced[i] = 0;

                for (int j = 0; j < AoeData.unitTypesList.Count; j++)
                {
                    for (int k = 0; k < form.players[i].amountStartUnits[j]; k++)
                    {
                        armies[i].Add(new Unit(AoeData.unitTypesList[j], this, (short)i));
                    }
                }

                armies[i].Shuffle();
                armies[i].ForEach(unit => {
                    if (unit.attackRange <= 1.0)
                    {
                        unit.SetXYInitial((i == 0 ? 1.0 : -1.0) * (-2.0 - army_MeleePlaced[i] / army_MeleeHeight[i] + rnd.NextDouble() * 0.1 - 0.05),
                            -army_MeleeHeight[i] / 2.0 + army_MeleePlaced[i] % army_MeleeHeight[i] + rnd.NextDouble() * 0.1 - 0.05);
                        army_MeleePlaced[i]++;
                    }
                    else
                    {
                        unit.SetXYInitial((i == 0 ? 1.0 : -1.0) * (-6.0 - army_RangedPlaced[i] / army_RangedHeight[i] + rnd.NextDouble() * 0.1 - 0.05),
                            -army_RangedHeight[i] / 2.0 + army_RangedPlaced[i] % army_RangedHeight[i] + rnd.NextDouble() * 0.1 - 0.05);
                        army_RangedPlaced[i]++;
                    }
                    unit.SetUnitIndex(unitIndex[i]);
                    unitIndex[i]++;
                });

                // armies[i].ForEach(unit => { Console.WriteLine("Army " + (i+1) + ":" + unit.unitType.name + " | " + unit.X + " - " + unit.Y); });
            }
        }

        private void CountSurvivors()
        {
            int[] utSurvivorsArmy = new int[2];
            int curValue;
            for (int i = 0; i < 2; i++)
            {
                AoeData.unitTypesList.ForEach(ut => {
                    utSurvivorsArmy[i] = armies[i].FindAll(u => u.unitType == ut).Count;
                    while (form.players[i].survivorsSumArmy.TryGetValue(ut, out curValue))
                    {
                        if (form.players[i].survivorsSumArmy.TryUpdate(ut, curValue + utSurvivorsArmy[i], curValue))
                        {
                            break;
                        }
                    }
                });
                Interlocked.Add(ref form.players[i].resourcesGenerated[2], (int)Math.Round(resourcesGenerated[i, 2]));
            }
        }

        private void SaveWinner()
        {
            if (armies[0].Count > armies[1].Count)
            {
                Interlocked.Add(ref form.players[0].sumWins, 2);
            }
            else if (armies[0].Count == armies[1].Count)
            {
                Interlocked.Increment(ref form.players[0].sumWins);
                Interlocked.Increment(ref form.players[1].sumWins);
            }
            else
            {
                Interlocked.Add(ref form.players[1].sumWins, 2);
            }
        }

        private void Cleanup()
        {
            for (int i = 0; i < 2; i++)
            {
                armies[i].FindAll(unit => unit.curHp <= 0.0m).ForEach(dyingUnit => {
                    dyingUnit.target.attackedBy.Remove(dyingUnit);
                    dyingUnit.alive = false;
                    gridUnits[dyingUnit.Gx, dyingUnit.Gy].Remove(dyingUnit);
                });
                graveyard.AddRange(armies[i].FindAll(unit => unit.curHp <= 0.0m));
                armies[i].RemoveAll(unit => unit.curHp <= 0.0m);
            }
            arrows.RemoveAll(arrow => arrow.arrived);
            missiles.RemoveAll(missile => missile.arrived);
        }

        public void Fight()
        {
            while (armies[0].Count > 0 && armies[1].Count > 0) // as soon as one army has no survivors, the battle ends
            {
                timeInterval++;

                foreach (Unit unit in armies[0].Concat(armies[1]))
                {
                    unit.EnsureHasTarget(); // first thing to ensure: each unit must have a target

                    unit.curHp += unit.hpRegPerMin / 6000.0m;

                    if (!unit.inAttackMotion) // if a unit is not currently in attack motion, we consider things like moving towards or away from its target or starting an attack
                    {
                        if (!unit.TargetWithinAttackRange()) // if the target is too far away, the unit moves towards it
                        {
                            unit.MoveTowardsTarget_CalculateNewPos();
                        }
                        else
                        {
                            if (!unit.TargetNotCloserThanMinimumAttackRange()) // if the target is too close (can happen for units with minimum range), the unit moves away from it
                            {
                                unit.MoveAwayFromTarget_CalculateNewPos();
                            }
                            else
                            {
                                if (unit.AttackCdReady()) // if the target is neither too far away or too close, we check whether the unit's attack cd is ready
                                {
                                    unit.StartAttackAnimation(); // attack the target, if the attack cd is ready
                                }
                                else
                                {
                                    if (unit.attackRange > 1.0 && hitAndRunMode > 0) // if the attack cd is not ready, check whether the unit is ranged and hit&run mode is activated
                                    {
                                        if (hitAndRunMode == 2 || unit.attackCd <= (unit.attackSpeed - unit.attackDelay) / 2.0m) // when hit&run is set to 'semi', units will only run with 50% efficiency (more realistic than perfect hit&run)
                                        {
                                            unit.MoveAwayFromTarget_CalculateNewPos(); //  ...if the conditions are met, the unit moves away from its target
                                        }
                                    }
                                }
                            }
                        }
                    }
                    
                    if (unit.inAttackMotion) // check, if a unit is already performing its attack animation (or just started due to the previous lines of code)
                    {
                        if (unit.AttackAnimationFinished()) // an already started attack animation can't be interrupted (except the attacker dies)
                        {
                            unit.PerformAttackOnTarget(); // after the attack animation is finished, the attack it self is executed (so the damage is dealt to the target(s) or the projectile is launched)
                        }
                        else
                        {
                            unit.ContinueAttackAnimation(); // continue the already started attack animation
                        }
                    }

                    unit.attackCd -= 0.01m; // the remaining time until the next attack (animation) can be started is reduced by 0.01s
                }

                foreach (Arrow arrow in arrows)
                {
                    if (arrow.eta <= timeInterval)
                    {
                        arrow.Impact();
                    }
                }

                foreach (Missile missile in missiles)
                {
                    missile.MoveAndCheckCollisions();
                }

                foreach (Unit unit in armies[0].Concat(armies[1]))
                {
                    unit.MoveUnit_AssumeNewPos();
                }

                Cleanup();
            }
        }

    }
}


/*
//Console.WriteLine((timeInterval / 100.0) + "s: " + dyingUnit.unitType.name + " " + dyingUnit.index + " of army 1 dead."); // DEBUG
*/
