
using System;
using System.Collections.Generic;

namespace AoeCombatSimulator
{
    public class Arrow : Projectile
    {
        public int eta;
        public double fractionOfMaxRange; // 1.0 if the arrow is fired over a distance equal to 100% of attacker's attack range, 0.0 if arrow is fired over a distance equal to 0% of attacker's attack range
        private static Random rnd = new Random();

        public Arrow(Battle battle, Unit attacker, Unit target, int eta, double fractionOfMaxRange, bool secondary) : base(battle, attacker, target, secondary)
        {
            this.eta = eta;
            this.fractionOfMaxRange = fractionOfMaxRange;
        }

        public void Impact()
        {
            arrived = true;
            int hitRoll = rnd.Next(100);
            if (target.alive && (hitRoll < attacker.accuracyPercent))
            {
                target.curHp -= Unit.CalculateDamageDealtToTarget(attacker, target, secondary);
            }
            else
            {
                double impactX = target.X;
                double impactY = target.Y;
                if (hitRoll >= attacker.accuracyPercent)
                {
                    impactX += fractionOfMaxRange * (-1.0 + rnd.NextDouble() * 2.0);
                    impactY += fractionOfMaxRange * (-1.0 + rnd.NextDouble() * 2.0);
                }

                List<Unit> targetArmy = attacker.armyIndex == 0 ? battle.armies[1] : battle.armies[0];
                Unit closestUnit = null;
                double closestUnitDistSq = Double.MaxValue;
                targetArmy.ForEach(possibleTarget => {
                    double distToArrowSq = (impactX - possibleTarget.X) * (impactX - possibleTarget.X) + (impactY - possibleTarget.Y) * (impactY - possibleTarget.Y);
                    if (distToArrowSq < possibleTarget.radius * possibleTarget.radius && distToArrowSq < closestUnitDistSq)
                    {
                        closestUnit = possibleTarget;
                        closestUnitDistSq = distToArrowSq;
                    }
                });
                if (closestUnit != null)
                {
                    // targets other than the main target only receive half of the normal damage
                    closestUnit.curHp -= (closestUnit == target ? 1.0m : 0.5m) * Unit.CalculateDamageDealtToTarget(attacker, closestUnit, secondary);
                }
            }
        }
    }
}