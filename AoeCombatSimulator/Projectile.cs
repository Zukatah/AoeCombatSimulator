namespace AoeCombatSimulator
{
    public abstract class Projectile
    {
        public Unit attacker;
        public Unit target;
        public bool arrived = false;
        public Battle battle; // the reference to the battle instance this arrow belongs to
        public bool secondary;


        public Projectile(Battle battle, Unit attacker, Unit target, bool secondary = false)
        {
            this.battle = battle;
            this.attacker = attacker;
            this.target = target;
            this.secondary = secondary;
        }
    }
}
