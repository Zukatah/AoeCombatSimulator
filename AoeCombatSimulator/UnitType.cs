using System.Collections.Generic;
using System.Windows.Forms;

namespace AoeCombatSimulator
{
    public class UnitType
    {
        public decimal hp; // hit points
        public decimal hpRegPerMin; // hit points regeneration per minute (relevant for berserk and camel archer)

        public Dictionary<ArmorClass, decimal> attackValues = new Dictionary<ArmorClass, decimal>(); // contains all armor classes this unit attacks (including baseMelee and basePierce) and the respective damage values
        public Dictionary<ArmorClass, decimal> armorClasses = new Dictionary<ArmorClass, decimal>(); // contains all armor classes this unit has (including baseMelee and basePierce) and the respective armor values

        public decimal attackSpeed; // time between the beginning of two consecutive attacks
        public double attackRange; // maximum attack range in tiles; the actual attack range is attackRange + radius
        public double attackRangeMin; // minimum attack range in tiles (skirmishers, genitours, ...); the actual minimum attack range is attackRangeMin + radius
        public decimal attackDelay; // the time in seconds between starting an attack and dealing the damage (or launching the projectile for ranged units); especially important for Hit&Run
        public double projectileSpeed; // projectile speed in tiles/s
        public short cleaveType = 0; // 0=none, 1=flat5 (slav infantry, cataphracts), 2=50% (elephants), 3=100% (flaming camels)
        public double cleaveRadius = 0.0; // cleaves enemy units if they are closer than cleaveRadius+ownRadius to cleaving unit
        public int accuracyPercent; // 100 does always hit; 50 does mean 50% will hit and 50% are randomly distributed (they can still hit the main target or other targets)

        public bool attackIsMissile = false; // only true for ranged units that fire missiles which damage targets on their way (scorpions and ballista elephants)
        public double missileFlightDistance = 0.0; // flight distance of missiles, since they don't necessarily stop flying at the intended target
        public double secondaryMissileFlightDistance = 0.0; // flight distance of secondary missiles, since they don't necessarily stop flying at the intended target

        public bool secondaryAttack = false; // some units fire secondary projectiles in addition to primary ones (chu ko nu, kipchaks, ballista elephants with unique tech, ...)
        public short secondaryAttackProjectileCount = 1; // per attack ChuKoNus, Kipchaks and Organ Guns create more than a single secondary projectile
        public Dictionary<ArmorClass, decimal> secondaryAttackValues; // // contains all armor classes this unit attacks with its secondary attack (including baseMelee and basePierce) and the respective damage values

        public double moveSpeed; // move speed in tiles/s
        public double radius; // size of the unit in tiles
        

        public ushort[] resourceCosts = new ushort[3]; // [0]=food, [1]=wood, [2]=gold
        public int unitTypeIndex; // this unit type's index in the unittypes list (for performance purposes)
        public string name; // this unit type's name


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
