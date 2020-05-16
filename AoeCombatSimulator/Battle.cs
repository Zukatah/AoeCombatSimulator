using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AoeCombatSimulator
{
    public class Battle
    {
        private UserInterface userInterface; // a reference to the user interface instance to which this battle's results will be reported
        public short hitAndRunMode; // 0 => no hit&run (except target too close for units with min range), 1 => hit&run with 50% efficiency, 2 => maximum hit&run possible
        
        public List<Unit>[] armies = new List<Unit>[] { new List<Unit>(), new List<Unit>() }; // the armies of both players
        public const int GRID_LENGTH = 42;
        public HashSet<Unit>[,] gridUnits = new HashSet<Unit>[GRID_LENGTH, GRID_LENGTH]; // a grid of hashsets containing all the units to improve missile collision detection performance
        private List<Unit> graveyard = new List<Unit>(); // a list containing all dead units
        public List<Arrow> arrows = new List<Arrow>(); // a list containing all arrows currently in the air
        public List<Missile> missiles = new List<Missile>(); // a list containing all missiles (scorpion, battle elephant) currently in the air

        public int timeInterval = 0; // number of time intervals since begin of the battle; one time interval = 0,01s
        private Random rnd; // a random generator for slight shifts of initial unit placement (further reduces determinism and improves quality of battle results if number of iterations is large)
        public decimal[,] resourcesGenerated = new decimal[2, 3] { { 0m, 0m, 0m }, { 0m, 0m, 0m } }; // currently only relevant for the Keshik gold generation


        public Battle(UserInterface userInterface, int taskId, int battleId, short hitAndRunMode)
        {
            this.userInterface = userInterface;
            this.hitAndRunMode = hitAndRunMode;
            rnd = new Random(taskId * Environment.ProcessorCount + battleId + Environment.TickCount);

            for (int i = 0; i < GRID_LENGTH; i++)
            {
                for (int j = 0; j < GRID_LENGTH; j++)
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
                army_SizeMelee[i] = AoeData.unitTypesList.Where(ut => ut.attackRange <= 1.0).Sum(ut => userInterface.players[i].amountStartUnits[ut.unitTypeIndex]);
                army_MeleeWidth[i] = (int)Math.Ceiling(Math.Sqrt(army_SizeMelee[i] / 2.0));
                army_MeleeHeight[i] = army_MeleeWidth[i] * 2;
                army_SizeRanged[i] = AoeData.unitTypesList.Where(ut => ut.attackRange > 1.0).Sum(ut => userInterface.players[i].amountStartUnits[ut.unitTypeIndex]);
                army_RangedWidth[i] = (int)Math.Ceiling(Math.Sqrt(army_SizeRanged[i] / 2.0));
                army_RangedHeight[i] = army_RangedWidth[i] * 2;
                army_Size[i] = army_SizeMelee[i] + army_SizeRanged[i];
                army_MeleePlaced[i] = 0;
                army_RangedPlaced[i] = 0;

                for (int j = 0; j < AoeData.unitTypesList.Count; j++)
                {
                    for (int k = 0; k < userInterface.players[i].amountStartUnits[j]; k++)
                    {
                        armies[i].Add(new Unit(AoeData.unitTypesList[j], this, (short)i));
                    }
                }

                armies[i].Shuffle();
                int unitIndex = 0;
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
                    unit.SetUnitIndex(unitIndex);
                    unitIndex++;
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
                    while (userInterface.players[i].survivorsSumArmy.TryGetValue(ut, out curValue))
                    {
                        if (userInterface.players[i].survivorsSumArmy.TryUpdate(ut, curValue + utSurvivorsArmy[i], curValue))
                        {
                            break;
                        }
                    }
                });
                Interlocked.Add(ref userInterface.players[i].resourcesGenerated[2], (int)Math.Round(resourcesGenerated[i, 2]));
            }
        }

        private void SaveWinner()
        {
            if (armies[0].Count > armies[1].Count)
            {
                Interlocked.Add(ref userInterface.players[0].sumWins, 2);
            }
            else if (armies[0].Count == armies[1].Count)
            {
                Interlocked.Increment(ref userInterface.players[0].sumWins);
                Interlocked.Increment(ref userInterface.players[1].sumWins);
            }
            else
            {
                Interlocked.Add(ref userInterface.players[1].sumWins, 2);
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