namespace AoeCombatSimulator
{
    public abstract class Projectile
    {
        public Unit attacker; // the unit that launched this projectile
        public Unit target; // the unit targeted by this projectile
        public bool arrived = false; // true if the projectile arrived (then it will be removed from the battle)
        public Battle battle; // the reference to the battle instance this arrow belongs to
        public bool secondary; // some units fire secondary projectiles in addition to primary ones (chu ko nu, kipchaks, ballista elephants with unique tech, ...)


        public Projectile(Battle battle, Unit attacker, Unit target, bool secondary = false)
        {
            this.battle = battle;
            this.attacker = attacker;
            this.target = target;
            this.secondary = secondary;
        }
    }
}
