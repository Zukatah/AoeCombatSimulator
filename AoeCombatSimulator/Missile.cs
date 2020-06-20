using System;
using System.Collections.Generic;
using System.Linq;

namespace AoeCombatSimulator
{
    public class Missile : Projectile
    {
        double x; // x coord of missile
        double y; // y coord of missile
        double dx; // delta x of missile per frame (so not normalized)
        double dy; // delta y of missile per frame (so not normalized)
        int flightDurationMax; // the number of frames the missile will fly in total
        int flightDurationPassed = 0; // the number of frames since the missile was launched
        HashSet<Unit> alreadyAffectedUnits = new HashSet<Unit>(); // the units the missile has already hit (can't hit the same unit twice)

        
        public Missile(Battle battle, Unit attacker, Unit target, double x, double y, double dx, double dy, int flightDurationMax, bool secondary) : base(battle, attacker, target, secondary)
        {
            this.x = x;
            this.y = y;
            this.dx = dx;
            this.dy = dy;
            this.flightDurationMax = flightDurationMax;
        }

        public void MoveAndCheckCollisions()
        {
            x += dx;
            y += dy;
            flightDurationPassed++;

            int gx = x < -20.0 ? 0 : Math.Min(Battle.GRID_LENGTH - 1, 1 + (int)Math.Floor(x + 20.0));
            int gy = y < -20.0 ? 0 : Math.Min(Battle.GRID_LENGTH - 1, 1 + (int)Math.Floor(y + 20.0));
            int minXGridIndex = Math.Max(0, gx - 1);
            int minYGridIndex = Math.Max(0, gy - 1);
            int maxXGridIndex = Math.Min(Battle.GRID_LENGTH - 1, gx + 1);
            int maxYGridIndex = Math.Min(Battle.GRID_LENGTH - 1, gy + 1);
            int targetArmyIndex = attacker.armyIndex == 1 ? 0 : 1;
            List<Unit> collisionTargets = new List<Unit>();

            for (int i = minXGridIndex; i <= maxXGridIndex; i++)
            {
                for (int j = minYGridIndex; j <= maxYGridIndex; j++)
                {
                    collisionTargets.AddRange(battle.gridUnits[i, j].Where(unit =>
                        unit.armyIndex == targetArmyIndex
                        && !alreadyAffectedUnits.Contains(unit)
                        && (unit.X - x) * (unit.X - x) + (unit.Y - y) * (unit.Y - y) <= (unit.radius) * (unit.radius)
                    ));
                }
            }

            collisionTargets.ForEach(unit => {
                unit.curHp -= Unit.CalculateDamageDealtToTarget(attacker, unit, secondary) * (unit == target ? 1.0m : 0.5m);
                alreadyAffectedUnits.Add(unit);
            });

            if (flightDurationPassed >= flightDurationMax)
            {
                arrived = true;
            }
        }
    }
}
