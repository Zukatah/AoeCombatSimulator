using System.Collections.Generic;
using System.Windows.Forms;

namespace AoeCombatSimulator
{
    public class UnitType
    {
        public int unitTypeIndex;
        public string name;

        public decimal hp;
        public decimal hpRegPerMin;

        public Dictionary<ArmorClass, decimal> attackValues = new Dictionary<ArmorClass, decimal>();
        public Dictionary<ArmorClass, decimal> armorClasses = new Dictionary<ArmorClass, decimal>();

        public decimal attackSpeed; // time between two attacks in seconds
        public double attackRange; // maximum attack range in tiles
        public double attackRangeMin; // minimum attack range (skirmishers, scorpions, ...) in tiles
        public decimal attackDelay; // the time in seconds between starting an attack and dealing the damage; especially important for Hit&Run
        public double projectileSpeed; // projectile speed in tiles/s
        public short cleaveType = 0; // 0=none, 1=flat5 (slav infantry, cataphracts), 2=50% (elephants), 3=100% (flaming camels)
        public double cleaveRadius = 0.0; // cleaves enemy units if they are closer than cleaveRadius+ownRadius to cleaving unit
        public int accuracyPercent; // 100 does always hit; 50 does mean 50% will hit and 50% are randomly distributed (they can still hit the main target or other targets)

        public bool attackIsMissile = false; // missiles damage targets on their way
        public double missileFlightDistance = 0.0; // currently only used for ballista elephant and scorpion
        public double secondaryMissileFlightDistance = 0.0; // currently only used for ballista elephant

        public bool secondaryAttack = false; // currently only used for ballista elephant
        public short secondaryAttackProjectileCount = 1; // per attack ChuKoNus, Kipchaks and Organ Guns create more than a single secondary projectile
        public Dictionary<ArmorClass, decimal> secondaryAttackValues; // currently only used for ballista elephant

        public double moveSpeed;

        public double radius;
        
        public ushort[] resourceCosts = new ushort[3]; // [0]=food, [1]=wood, [2]=gold


        public UnitType(string name, decimal hp, decimal attackSpeed, double attackRange, decimal attackDelay, double projectileSpeed, double moveSpeed, ushort foodCost, ushort woodCost, ushort goldCost, double radius = 0.2, double attackRangeMin = 0.0, int accuracyPercent = 100, decimal hpRegPerMin = 0.0m)
        {
            this.name = name;
            this.hp = hp;
            this.attackSpeed = attackSpeed;
            this.attackRange = attackRange;
            this.attackDelay = attackDelay;
            this.projectileSpeed = projectileSpeed;
            this.moveSpeed = moveSpeed;
            resourceCosts[0] = foodCost;
            resourceCosts[1] = woodCost;
            resourceCosts[2] = goldCost;
            this.attackRangeMin = attackRangeMin;
            this.radius = radius;
            this.accuracyPercent = accuracyPercent;
            this.hpRegPerMin = hpRegPerMin;
        }
    }
}
