using System;
using System.Collections.Generic;

namespace AoeCombatSimulator
{
    public class Unit
    {
        public decimal hp;
        public decimal hpRegPerMin;

        public Dictionary<ArmorClass, decimal> attackValues;
        public Dictionary<ArmorClass, decimal> armorClasses;

        public decimal attackSpeed;
        public double attackRange;
        public double attackRangeMin;
        public decimal attackDelay;
        public double projectileSpeed;
        public short cleaveType = 0; // 0=none, 1=flat5, 2=50%
        public double cleaveRadius = 0.0;
        public int accuracyPercent;

        public bool attackIsMissile; // missiles damage targets on their way
        public double missileFlightDistance;
        public double secondaryMissileFlightDistance;

        public bool secondaryAttack;
        public short secondaryAttackProjectileCount = 1; // per attack ChuKoNus, Kipchaks and Organ Guns create more than a single secondary projectile
        public Dictionary<ArmorClass, decimal> secondaryAttackValues;

        public double moveSpeed;

        public double radius;

        
        public UnitType unitType; // this unit's unit type; the unit type defines many attributes of each unit
        public Battle battle; // the reference to the battle instance this unit belongs to
        public decimal curHp; // the current hit points of this unit
        public bool alive = true; // a unit is alive until the END of the frame its HP reaches 0 or below 0
        public Unit target = null; // the target of this unit; this unit attacks the target or tries to attack it
        public List<Unit> attackedBy = new List<Unit>(); // all units that are currently attacking this unit (<=> all units that target this unit); used for collision simulation and movement speed of the attackers
        private Unit meleeDamagedBy = null; // the last melee attacker that dealt damage to this unit; used to find a new target for this unit if the current target dies
        public decimal attackCd = 0; // the time until the next attack anim can be started
        public bool inAttackMotion = false; // true, if the unit is currently executing its attack animation
        public decimal attackAnimDur = 0; // the time since the last attack animation was started
        public double X { get; private set; } // current x coord of the unit
        public double Y { get; private set; } // current y coord of the unit
        public double Nx { get; private set; } // new x coord which will be assumed by the unit at the end of the frame
        public double Ny { get; private set; } // new y coord which will be assumed by the unit at the end of the frame
        public int Gx { get; private set; } // the x coordinate of the tile this unit is located in; used for efficient collision detection with projectiles (range from 0-21)
        public int Gy { get; private set; } // the y coordinate of the tile this unit is located in; used for efficient collision detection with projectiles (range from 0-21)
        private Random rnd;
        public int index; // DEBUG purposes; each unit has a unique index within its army
        public short armyIndex; // 0=Army1, 1=Army2
        public bool running = false; // for hit&run calculations


        public Unit(UnitType unitType, Battle battle, short armyIndex)
        {
            hp = unitType.hp;
            attackSpeed = unitType.attackSpeed;
            attackRange = unitType.attackRange;
            attackRangeMin = unitType.attackRangeMin;
            attackDelay = unitType.attackDelay;
            attackIsMissile = unitType.attackIsMissile;
            secondaryAttack = unitType.secondaryAttack;
            secondaryAttackProjectileCount = unitType.secondaryAttackProjectileCount;
            missileFlightDistance = unitType.missileFlightDistance;
            secondaryMissileFlightDistance = unitType.secondaryMissileFlightDistance;
            secondaryAttackValues = unitType.secondaryAttackValues;
            projectileSpeed = unitType.projectileSpeed;
            cleaveType = unitType.cleaveType;
            cleaveRadius = unitType.cleaveRadius;
            moveSpeed = unitType.moveSpeed;
            attackValues = unitType.attackValues;
            armorClasses = unitType.armorClasses;
            radius = unitType.radius;
            accuracyPercent = unitType.accuracyPercent;
            hpRegPerMin = unitType.hpRegPerMin;

            this.unitType = unitType;
            this.battle = battle;
            curHp = hp;
            this.armyIndex = armyIndex;
        }

        public void SetXYInitial(double x, double y)
        {
            X = x;
            Y = y;

            Gx = x < -20.0 ? 0 : Math.Min(21, 1 + (int)Math.Floor((x + 20.0) / 2.0));
            Gy = y < -20.0 ? 0 : Math.Min(21, 1 + (int)Math.Floor((y + 20.0) / 2.0));

            battle.gridUnits[Gx, Gy].Add(this);
        }

        public void SetUnitIndex (int index)
        {
            this.index = index;
            rnd = new Random(index + armyIndex * 1000 + Environment.TickCount);
        }

        public bool TargetWithinAttackRange()
        {
            return (X - target.X) * (X - target.X) + (Y - target.Y) * (Y - target.Y) <= (attackRange + radius) * (attackRange + radius);
        }

        public bool TargetNotCloserThanMinimumAttackRange()
        {
            return attackRangeMin == 0.0 || (X - target.X) * (X - target.X) + (Y - target.Y) * (Y - target.Y) >= (attackRangeMin + radius) * (attackRangeMin + radius);
        }

        public bool AttackCdReady()
        {
            return attackCd <= 0.0m;
        }

        public void MoveTowardsTarget_CalculateNewPos()
        {
            double dx = target.X - X;
            double dy = target.Y - Y;
            double dlength = Math.Sqrt(dx * dx + dy * dy);
            dlength = (dlength == 0.0 ? 1.0 : dlength);
            dx /= dlength;
            dy /= dlength;
            double speedAfterBumpReduction = target.attackedBy[0] == this ? moveSpeed * 0.01 : moveSpeed * 0.01 / Math.Pow(target.attackedBy.Count, 0.2); // 54
            Nx = X + (speedAfterBumpReduction > dlength ? dlength : speedAfterBumpReduction) * dx;
            Ny = Y + (speedAfterBumpReduction > dlength ? dlength : speedAfterBumpReduction) * dy;
        }

        public void MoveAwayFromTarget_CalculateNewPos()
        {
            double dx = X - target.X;
            double dy = Y - target.Y;
            double dlength = Math.Sqrt(dx * dx + dy * dy);
            dlength = dlength == 0.0 ? 1.0 : dlength;
            dx /= dlength;
            dy /= dlength;
            if (dlength + moveSpeed * 0.01 > attackRange)
            {
                Nx = target.X + attackRange * dx;
                Ny = target.Y + attackRange * dy;
            }
            else
            {
                Nx = X + moveSpeed * 0.01 * dx;
                Ny = Y + moveSpeed * 0.01 * dy;
            }
            Nx = Nx > 120 ? 120.0 : (Nx < -120.0 ? -120.0 : Nx);
            Ny = Ny > 120 ? 120.0 : (Ny < -120.0 ? -120.0 : Ny);
        }

        public void MoveUnit_AssumeNewPos()
        {
            if (Nx != X || Ny != Y)
            {
                X = Nx;
                Y = Ny;

                int n_gx = X < -20.0 ? 0 : Math.Min(21, 1 + (int)Math.Floor((X + 20.0) / 2.0));
                int n_gy = Y < -20.0 ? 0 : Math.Min(21, 1 + (int)Math.Floor((Y + 20.0) / 2.0));

                if (n_gx != Gx || n_gy != Gy)
                {
                    battle.gridUnits[Gx, Gy].Remove(this);
                    battle.gridUnits[n_gx, n_gy].Add(this);
                    Gx = n_gx;
                    Gy = n_gy;
                }
            }
        }

        public void StartAttackAnimation()
        {
            inAttackMotion = true;
            attackAnimDur = 0;
            attackCd = attackSpeed;
        }

        public bool AttackAnimationFinished()
        {
            return attackAnimDur >= attackDelay;
        }

        public void ContinueAttackAnimation()
        {
            attackAnimDur += 0.01m;
        }

        public static decimal CalculateDamageDealtToTarget(Unit attacker, Unit target, bool secondary = false)
        {
            decimal damageDealt = 0.0m;
            if (attacker.unitType == AoeData.ut_eliteLeitis) // leitis ignore armor and don't have any attack bonusses
            {
                damageDealt = attacker.attackValues[AoeData.ac_baseMelee];
            }
            else if (secondary && attacker.unitType == AoeData.ut_eliteOrganGun) // secondary missiles of organ guns always deal 2 damage (and 1 if target wasn't the main target)
            {
                damageDealt = 2;
            }
            else
            {
                foreach (KeyValuePair<ArmorClass, decimal> attackValueEntry in (secondary ? attacker.secondaryAttackValues : attacker.attackValues)) // all of the attacker's attack values are compared to the armor class values of the defender
                {
                    damageDealt += Math.Max(0, target.armorClasses.ContainsKey(attackValueEntry.Key) ? attackValueEntry.Value - target.armorClasses[attackValueEntry.Key] : 0);
                }
            }
            if (damageDealt < 1)
            {
                damageDealt = 1;
            }
            return damageDealt;
        }

        public void PerformAttackOnTarget()
        {
            if (attackRange <= 1.0)
            {
                decimal damageDealt = CalculateDamageDealtToTarget(this, target);
                target.curHp -= damageDealt;
                target.meleeDamagedBy = this;
                if (cleaveType != 0)
                {
                    List<Unit> targetArmy = armyIndex == 0 ? battle.armies[1] : battle.armies[0];
                    int affectedTargets = 0;
                    int maxTargets = 6 + (int)Math.Round(5.0 * (radius - 0.4)); // infantry cleaves up to 6 units, cavalry up to 7, elephants up to 8 (limit to offset non-existing collision detection)
                    targetArmy.ForEach(possibleTarget => {
                        if (possibleTarget != target && affectedTargets < maxTargets && (X - possibleTarget.X) * (X - possibleTarget.X) + (Y - possibleTarget.Y) * (Y - possibleTarget.Y) < (cleaveRadius + target.radius)* (cleaveRadius + target.radius))
                        {
                            if (cleaveType == 1)
                            {
                                damageDealt = 5.0m;
                            }
                            else
                            {
                                damageDealt = CalculateDamageDealtToTarget(this, possibleTarget);
                                if (cleaveType == 2) // cleaveType 2 (elephants) deal 50% area damage; cleaveType 3 (petards, flaming camels) deal 100% area damage
                                {
                                    damageDealt *= 0.5m;
                                }
                                damageDealt = damageDealt < 1 ? 1 : damageDealt;
                            }
                            possibleTarget.curHp -= damageDealt;
                            affectedTargets++;
                        }
                    });
                }

                if (unitType == AoeData.ut_eliteKeshik)
                {
                    battle.resourcesGenerated[armyIndex, 2] += 0.695m;
                }
                if (unitType == AoeData.ut_flamingCamel)
                {
                    curHp = 0.0m;
                }
            }
            else // ranged units create arrows or missiles
            {
                if (attackIsMissile)
                {
                    double dx = target.X - X;
                    double dy = target.Y - Y;
                    double distanceToTarget = Math.Sqrt(dx*dx + dy*dy);
                    double dxNorm = distanceToTarget == 0 ? 1 : dx / distanceToTarget;
                    double dyNorm = distanceToTarget == 0 ? 0 : dy / distanceToTarget;
                    double dxPerFrame = dxNorm * projectileSpeed / 100.0;
                    double dyPerFrame = dyNorm * projectileSpeed / 100.0;
                    battle.missiles.Add(new Missile(battle, this, target, X, Y, dxPerFrame, dyPerFrame, (int)(missileFlightDistance * 100.0 / projectileSpeed), false));

                    if (secondaryAttack) // only relevant for elite ballista elephant with double crossbow (and theoretically heavy scorpion with double crossbow too)
                    {
                        double directionRadian = Math.Atan2(dyNorm, dxNorm);
                        directionRadian += (-0.17453 + 0.34906 * rnd.NextDouble()); // double crossbow bolts have an angle varying between +10 degree and -10 degree of the primary missile
                        double secDxNorm = Math.Cos(directionRadian);
                        double secDyNorm = Math.Sin(directionRadian);
                        double secDxPerFrame = secDxNorm * projectileSpeed / 100.0;
                        double secDyPerFrame = secDyNorm * projectileSpeed / 100.0;
                        battle.missiles.Add(new Missile(battle, this, target, X, Y, secDxPerFrame, secDyPerFrame, (int)(secondaryMissileFlightDistance * 100.0 / projectileSpeed), true));
                    }
                }
                else
                {
                    double distanceToTarget = Math.Sqrt((X - target.X) * (X - target.X) + (Y - target.Y) * (Y - target.Y));
                    battle.arrows.Add(new Arrow(battle, this, target, battle.timeInterval + (int)(100.0 * distanceToTarget / projectileSpeed), distanceToTarget / (attackRange + radius), false));

                    if (secondaryAttack)
                    {
                        for (int i = 0; i < secondaryAttackProjectileCount; i++)
                        {
                            battle.arrows.Add(new Arrow(battle, this, target, battle.timeInterval + (int)(100.0 * distanceToTarget / projectileSpeed), distanceToTarget / (attackRange + radius), true));
                        }
                    }
                }
            }
            inAttackMotion = false; // Attack animation finished
            attackAnimDur = 0; // Reset attack animation time
        }

        public void EnsureHasTarget()
        {
            List<Unit> targetArmy = armyIndex == 0 ? battle.armies[1] : battle.armies[0];

            if (target == null || !target.alive)
            {
                inAttackMotion = false;
                attackAnimDur = 0;

                if (meleeDamagedBy != null && meleeDamagedBy.alive)
                {
                    target = meleeDamagedBy;
                    target.attackedBy.Add(this);
                    return;
                }

                List<int> closestUnitIndex = new List<int> { -1, -1, -1, -1, -1, -1 };
                List<double> closestUnitDistSq = new List<double> { Double.MaxValue, Double.MaxValue, Double.MaxValue, Double.MaxValue, Double.MaxValue, Double.MaxValue };
                double distCurUnit;
                for (int i = 0; i < targetArmy.Count; i++)
                {
                    distCurUnit = (X - targetArmy[i].X) * (X - targetArmy[i].X) + (Y - targetArmy[i].Y) * (Y - targetArmy[i].Y);
                    for (int j = 0; j < 6; j++)
                    {
                        if (distCurUnit < closestUnitDistSq[j])
                        {
                            closestUnitIndex.Insert(j, i);
                            closestUnitIndex.RemoveAt(6);
                            closestUnitDistSq.Insert(j, distCurUnit);
                            closestUnitDistSq.RemoveAt(6);
                            break;
                        }
                    }
                    
                }
                target = targetArmy[closestUnitIndex[rnd.Next(targetArmy.Count >= 6 ? 6 : targetArmy.Count)]];
                target.attackedBy.Add(this);
            }
        }
    }
}





/*
int[] possibleTargetIndex = new int[targetArmy.Count > 3 ? 3 : targetArmy.Count];
double[] possibleTargetDistSq = new double[targetArmy.Count > 8 ? 8 : targetArmy.Count];
int closestUnitIndex = -1;
double closestUnitDistSq = Double.MaxValue;
for (int i = 0; i < possibleTargetIndex.Length; i++)
{
    possibleTargetIndex[i] = targetArmy.Count > 8 ? rnd.Next(targetArmy.Count) : i;
    possibleTargetDistSq[i] = (x - targetArmy[possibleTargetIndex[i]].x) * (x - targetArmy[possibleTargetIndex[i]].x) + (y - targetArmy[possibleTargetIndex[i]].y) * (y - targetArmy[possibleTargetIndex[i]].y);
    if (closestUnitDistSq > possibleTargetDistSq[i])
    {
        closestUnitDistSq = possibleTargetDistSq[i];
        closestUnitIndex = possibleTargetIndex[i];
    }
}
target = targetArmy[closestUnitIndex];
*/

/*
int maxUnitsComingIntoQuestion = targetArmy.Count > 80 ? 80 : targetArmy.Count;
int possibleTargetIndex;
double possibleTargetDistSq;
int closestUnitIndex = -1;
double closestUnitDistSq = Double.MaxValue;
for (int i = 0; i < maxUnitsComingIntoQuestion; i++)
{
    possibleTargetIndex = targetArmy.Count > 80 ? rnd.Next(targetArmy.Count) : i;
    possibleTargetDistSq = (x - targetArmy[possibleTargetIndex].x) * (x - targetArmy[possibleTargetIndex].x) + (y - targetArmy[possibleTargetIndex].y) * (y - targetArmy[possibleTargetIndex].y);
    if (closestUnitDistSq > possibleTargetDistSq)
    {
        closestUnitDistSq = possibleTargetDistSq;
        closestUnitIndex = possibleTargetIndex;
    }
}
target = targetArmy[closestUnitIndex];
*/