using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AoeCombatSimulator
{
    public static class AoeData
    {
        public static ArmorClass ac_infantry = new ArmorClass("Infantry");
        public static ArmorClass ac_basePierce = new ArmorClass("Base pierce");
        public static ArmorClass ac_baseMelee = new ArmorClass("Base melee");
        public static ArmorClass ac_warElephant = new ArmorClass("War elephant");
        public static ArmorClass ac_cavalry = new ArmorClass("Cavalry");
        public static ArmorClass ac_archer = new ArmorClass("Archer");
        public static ArmorClass ac_ram = new ArmorClass("Ram");
        public static ArmorClass ac_uniqueUnit = new ArmorClass("Unique unit");
        public static ArmorClass ac_siegeWeapon = new ArmorClass("Siege weapon");
        public static ArmorClass ac_gunpowderUnit = new ArmorClass("Gunpowder Unit");
        public static ArmorClass ac_spearman = new ArmorClass("Spearman");
        public static ArmorClass ac_cavalryArcher = new ArmorClass("Cavalry archer");
        public static ArmorClass ac_eagleWarrior = new ArmorClass("Eagle Warrior");
        public static ArmorClass ac_camel = new ArmorClass("Camel");
        public static ArmorClass ac_condottiero = new ArmorClass("Condottiero");
        public static ArmorClass ac_mameluke = new ArmorClass("Mameluke");


        public static UnitType ut_villager = new UnitType("Villager", 40, 2.0m, 0.0, 0.53m, Double.MaxValue, 0.968, 50, 0, 0);

        public static UnitType ut_champion = new UnitType("Champion", 70, 2.0m, 0.0, 0.63m, Double.MaxValue, 0.99, 45, 0, 20);
        public static UnitType ut_halberdier = new UnitType("Halberdier", 60, 3.05m, 0.0, 0.5m, Double.MaxValue, 0.9, 35, 25, 0);
        public static UnitType ut_eliteEagleWarrior = new UnitType("Elite Eagle Warrior", 60, 2.0m, 0.0, 0.8m, Double.MaxValue, 1.43, 20, 0, 50);

        public static UnitType ut_hussar = new UnitType("Hussar", 95, 1.9m, 0.0, 0.95m, Double.MaxValue, 1.65, 80, 0, 0, 0.4);
        public static UnitType ut_paladin = new UnitType("Paladin", 180, 1.9m, 0.0, 0.68m, Double.MaxValue, 1.485, 60, 0, 75, 0.4);
        public static UnitType ut_heavyCamelRider = new UnitType("Heavy Camel Rider", 140, 2.0m, 0.0, 0.5m, Double.MaxValue, 1.595, 55, 0, 60, 0.4);
        public static UnitType ut_eliteBattleElephant = new UnitType("Elite Battle Elephant", 320, 2.0m, 0.0, 0.5m, Double.MaxValue, 0.935, 120, 0, 70, 0.6);
        public static UnitType ut_eliteSteppeLancer = new UnitType("Elite Steppe Lancer", 100, 2.3m, 1.0, 0.68m, Double.MaxValue, 1.595, 70, 0, 45, 0.4);

        public static UnitType ut_arbalester = new UnitType("Arbalester", 40, 1.7m, 8.0, 0.35m, 7.0, 0.96, 0, 25, 45);
        public static UnitType ut_eliteSkirmisher = new UnitType("Elite Skirmisher", 35, 3.05m, 8.0, 0.51m, 7.0, 0.96, 35, 25, 0, 0.2, 1.0);
        public static UnitType ut_heavyCavalryArcher = new UnitType("Heavy Cavalry Archer", 80, 1.8m, 7.0, 1.0m, 7.0, 1.54, 0, 40, 60, 0.4);
        public static UnitType ut_handCannoneer = new UnitType("Hand Cannoneer", 35, 3.45m, 7.0, 0.35m, 5.5, 0.96, 45, 0, 50, 0.2, 0.0, 65);

        public static UnitType ut_siegeRam = new UnitType("Siege Ram", 270, 5.0m, 0.0, 0.75m, Double.MaxValue, 0.6, 0, 160, 75, 0.8);
        public static UnitType ut_heavyScorpion = new UnitType("Heavy Scorpion", 50, 3.6m, 8.0, 0.21m, 6.0, 0.65, 0, 75, 75, 0.5, 2.0, 100);

        public static UnitType ut_eliteLongbowman = new UnitType("Elite Longbowman", 40, 2.0m, 12.0, 0.5m, 7.0, 0.96, 0, 35, 40, 0.2, 0.0, 80);
        public static UnitType ut_eliteCataphract = new UnitType("Elite Cataphract", 150, 1.7m, 0.0, 0.68m, Double.MaxValue, 1.48, 70, 0, 75, 0.4);
        public static UnitType ut_eliteWoadRaider = new UnitType("Elite Woad Raider", 80, 2.0m, 0.0, 0.72m, Double.MaxValue, 1.38, 65, 0, 25);
        public static UnitType ut_eliteChuKoNu = new UnitType("Elite Chu Ko Nu", 50, 2.4m, 7.0, 0.83m, 7.0, 0.96, 0, 40, 35); // actually attack delay is 0.23s, but firing all the missiles takes longer
        public static UnitType ut_eliteThrowingAxeman = new UnitType("Elite Throwing Axeman", 70, 2.0m, 5.0, 0.82m, 7.0, 1.1, 55, 0, 25);
        public static UnitType ut_eliteHuskarl = new UnitType("Elite Huskarl", 70, 2.0m, 0.0, 0.8m, Double.MaxValue, 1.155, 52, 0, 26);
        public static UnitType ut_eliteSamurai = new UnitType("Elite Samurai", 80, 1.45m, 0.0, 0.8m, Double.MaxValue, 1.1, 60, 0, 30);
        public static UnitType ut_eliteMangudai = new UnitType("Elite Mangudai", 80, 1.445m, 7.0, 0.5m, 7.0, 1.595, 0, 55, 65, 0.4);
        public static UnitType ut_eliteWarElephant = new UnitType("Elite War Elephant", 620, 2.0m, 0.0, 0.56m, Double.MaxValue, 0.858, 200, 0, 75, 0.6);
        public static UnitType ut_eliteMameluke = new UnitType("Elite Mameluke", 130, 2.0m, 3.0, 0.2m, Double.MaxValue, 1.54, 55, 0, 85, 0.4);
        public static UnitType ut_eliteTeutonicKnight = new UnitType("Elite Teutonic Knight", 100, 2.0m, 0.0, 0.75m, Double.MaxValue, 0.88, 85, 0, 40);
        public static UnitType ut_eliteJanissary = new UnitType("Elite Janissary", 50, 3.49m, 8.0, 0.0m, 5.5, 0.96, 60, 0, 55, 0.2, 0.0, 50);
        public static UnitType ut_eliteBerserk = new UnitType("Elite Berserk", 75, 2.0m, 0.0, 0.5m, Double.MaxValue, 1.155, 65, 0, 25, 0.2, 0.0, 100, 40.0m);

        public static UnitType ut_eliteJaguarWarrior = new UnitType("Elite Jaguar Warrior", 75, 2.0m, 0.0, 0.8m, Double.MaxValue, 1.1, 60, 0, 30);
        public static UnitType ut_eliteTarkan = new UnitType("Elite Tarkan", 170, 2.1m, 0.0, 0.95m, Double.MaxValue, 1.48, 60, 0, 60, 0.4);
        public static UnitType ut_eliteWarWagon = new UnitType("Elite War Wagon", 200, 2.25m, 8.0, 1.0m, 6.0, 1.32, 0, 94, 60, 0.8);
        public static UnitType ut_elitePlumedArcher = new UnitType("Elite Plumed Archer", 65, 1.615m, 8.0, 0.5m, 7.0, 1.2, 0, 40, 40);
        public static UnitType ut_eliteConquistador = new UnitType("Elite Conquistador", 90, 2.9m, 6.0, 0.41m, 5.5, 1.43, 60, 0, 70, 0.4, 0.0, 70);

        public static UnitType ut_eliteKamayuk = new UnitType("Elite Kamayuk", 80, 2.0m, 1.0, 0.5m, Double.MaxValue, 1.1, 60, 0, 30);
        public static UnitType ut_slinger = new UnitType("Slinger", 40, 2.0m, 8.0, 0.8m, 5.5, 0.96, 30, 0, 40);
        public static UnitType ut_eliteElephantArcher = new UnitType("Elite Elephant Archer", 350, 1.7m, 7.0, 0.4m, 7.0, 0.88, 100, 0, 70, 0.6);
        public static UnitType ut_imperialCamelRider = new UnitType("Imperial Camel Rider", 160, 2.0m, 0.0, 0.5m, Double.MaxValue, 1.595, 55, 0, 60, 0.4);
        public static UnitType ut_eliteGenoeseCrossbowman = new UnitType("Elite Genoese Crossbowman", 50, 1.7m, 7.0, 0.5m, 7.0, 0.96, 0, 45, 45);
        public static UnitType ut_condottiero = new UnitType("Condottiero", 80, 1.9m, 0.0, 0.75m, Double.MaxValue, 1.32, 50, 0, 35);
        public static UnitType ut_eliteMagyarHuszar = new UnitType("Elite Magyar Huszar", 105, 1.8m, 0.0, 0.68m, Double.MaxValue, 1.65, 80, 0, 0, 0.4);
        public static UnitType ut_eliteBoyar = new UnitType("Elite Boyar", 150, 1.9m, 0.0, 0.7m, Double.MaxValue, 1.43, 50, 0, 80, 0.4);

        public static UnitType ut_eliteCamelArcher = new UnitType("Elite Camel Archer", 80, 1.7m, 7.0, 0.63m, 7.0, 1.54, 0, 50, 60, 0.4, 0.0, 100, 15.0m);
        public static UnitType ut_eliteGenitour = new UnitType("Elite Genitour", 75, 3.0m, 7.0, 0.5m, 7.0, 1.485, 50, 35, 0, 0.4, 1.0);
        public static UnitType ut_eliteShotelWarrior = new UnitType("Elite Shotel Warrior", 50, 2.0m, 0.0, 0.75m, Double.MaxValue, 1.32, 50, 0, 35);
        public static UnitType ut_eliteGbeto = new UnitType("Elite Gbeto", 45, 2.0m, 0.0, 1.2m, Double.MaxValue, 1.375, 50, 0, 40);
        public static UnitType ut_eliteOrganGun = new UnitType("Elite Organ Gun", 70, 3.45m, 8.0, 0.6m, 5.5, 0.85, 0, 80, 60, 0.4, 1.0, 50);

        public static UnitType ut_eliteArambai = new UnitType("Elite Arambai", 85, 2.0m, 5.0, 0.6m, 7.0, 1.43, 0, 75, 60, 0.4, 0.0, 30);
        public static UnitType ut_eliteBallistaElephant = new UnitType("Elite Ballista Elephant", 310, 2.5m, 6.0, 0.4m, 6.0, 0.88, 100, 0, 80, 0.6);
        public static UnitType ut_eliteKarambitWarrior = new UnitType("Elite Karambit Warrior", 40, 2.0m, 0.0, 0.81m, Double.MaxValue, 1.32, 30, 0, 15);
        public static UnitType ut_eliteRattanArcher = new UnitType("Elite Rattan Archer", 45, 1.7m, 8.0, 0.69m, 7.0, 1.1, 0, 50, 45);
        public static UnitType ut_imperialSkirmisher = new UnitType("Imperial Skirmisher", 42, 3.05m, 8.0, 0.51m, 7.0, 0.96, 25, 35, 0, 0.2, 1.0);

        public static UnitType ut_eliteKonnik = new UnitType("Elite Konnik", 140, 1.8m, 0.0, 0.7m, Double.MaxValue, 1.485, 60, 0, 70, 0.4);
        public static UnitType ut_eliteKonnikDismounted = new UnitType("Elite Konnik Dismounted", 50, 1.8m, 0.0, 0.7m, Double.MaxValue, 0.99, 30, 0, 35);
        public static UnitType ut_eliteKipchak = new UnitType("Elite Kipchak", 65, 1.98m, 6.0, 0.0m, 7.0, 1.54, 0, 60, 35, 0.4);
        public static UnitType ut_eliteLeitis = new UnitType("Elite Leitis", 150, 1.9m, 0.0, 0.7m, Double.MaxValue, 1.54, 70, 0, 50, 0.4);
        public static UnitType ut_eliteKeshik = new UnitType("Elite Keshik", 160, 1.9m, 0.0, 0.7m, Double.MaxValue, 1.54, 50, 0, 40, 0.4);
        public static UnitType ut_flamingCamel = new UnitType("Flaming Camel", 75, Decimal.MaxValue, 0.0, 0.0m, Double.MaxValue, 1.43, 75, 0, 30, 0.4);

        public static List<UnitType> unitTypesList = new List<UnitType> { ut_villager, ut_champion, ut_halberdier, ut_eliteEagleWarrior,
            ut_hussar, ut_paladin, ut_heavyCamelRider, ut_eliteBattleElephant, ut_eliteSteppeLancer,
            ut_arbalester, ut_eliteSkirmisher, ut_heavyCavalryArcher, ut_handCannoneer,
            ut_siegeRam, ut_heavyScorpion,
            ut_eliteLongbowman, ut_eliteCataphract, ut_eliteWoadRaider, ut_eliteChuKoNu, ut_eliteThrowingAxeman, ut_eliteHuskarl, ut_eliteSamurai,
            ut_eliteMangudai, ut_eliteWarElephant, ut_eliteMameluke, ut_eliteTeutonicKnight, ut_eliteJanissary,  ut_eliteBerserk,
            ut_eliteJaguarWarrior, ut_eliteTarkan, ut_eliteWarWagon, ut_elitePlumedArcher, ut_eliteConquistador,
            ut_eliteKamayuk, ut_slinger, ut_eliteElephantArcher, ut_imperialCamelRider, ut_eliteGenoeseCrossbowman, ut_condottiero, ut_eliteMagyarHuszar, ut_eliteBoyar,
            ut_eliteCamelArcher, ut_eliteGenitour, ut_eliteShotelWarrior, ut_eliteGbeto, ut_eliteOrganGun,
            ut_eliteArambai, ut_eliteBallistaElephant, ut_eliteKarambitWarrior, ut_eliteRattanArcher, ut_imperialSkirmisher,
            ut_eliteKonnik, ut_eliteKonnikDismounted, ut_eliteKipchak, ut_eliteLeitis, ut_eliteKeshik, ut_flamingCamel
        };

        public static Bitmap[] resourceImages = new Bitmap[3] {
            Properties.Resources._31905369948_baf2090435_b,
            Properties.Resources.images,
            Properties.Resources.gold_bars_4722600_960_720 };


        public static void InitializeUnitTypes()
        {
            for (int i = 0; i < unitTypesList.Count; i++)
            {
                unitTypesList[i].unitTypeIndex = i;
            }

            ut_villager.armorClasses.Add(ac_baseMelee, 1.0m);
            ut_villager.armorClasses.Add(ac_basePierce, 2.0m);
            ut_villager.attackValues.Add(ac_baseMelee, 3.0m);

            ut_champion.armorClasses.Add(ac_infantry, 0.0m);
            ut_champion.armorClasses.Add(ac_baseMelee, 4);
            ut_champion.armorClasses.Add(ac_basePierce, 5);
            ut_champion.attackValues.Add(ac_eagleWarrior, 8.0m);
            ut_champion.attackValues.Add(ac_baseMelee, 17.0m);

            ut_halberdier.armorClasses.Add(ac_infantry, 0.0m);
            ut_halberdier.armorClasses.Add(ac_spearman, 0.0m);
            ut_halberdier.armorClasses.Add(ac_baseMelee, 3.0m);
            ut_halberdier.armorClasses.Add(ac_basePierce, 4.0m);
            ut_halberdier.attackValues.Add(ac_cavalry, 32.0m);
            ut_halberdier.attackValues.Add(ac_warElephant, 28.0m);
            ut_halberdier.attackValues.Add(ac_camel, 26.0m);
            ut_halberdier.attackValues.Add(ac_mameluke, 11.0m);
            ut_halberdier.attackValues.Add(ac_eagleWarrior, 1.0m);
            ut_halberdier.attackValues.Add(ac_baseMelee, 10.0m);

            ut_eliteEagleWarrior.armorClasses.Add(ac_infantry, 0.0m);
            ut_eliteEagleWarrior.armorClasses.Add(ac_eagleWarrior, 0.0m);
            ut_eliteEagleWarrior.armorClasses.Add(ac_baseMelee, 3.0m);
            ut_eliteEagleWarrior.armorClasses.Add(ac_basePierce, 8.0m);
            ut_eliteEagleWarrior.attackValues.Add(ac_siegeWeapon, 5.0m);
            ut_eliteEagleWarrior.attackValues.Add(ac_cavalry, 4.0m);
            ut_eliteEagleWarrior.attackValues.Add(ac_camel, 3.0m);
            ut_eliteEagleWarrior.attackValues.Add(ac_baseMelee, 13.0m);

            ut_hussar.armorClasses.Add(ac_baseMelee, 3);
            ut_hussar.armorClasses.Add(ac_basePierce, 6);
            ut_hussar.armorClasses.Add(ac_cavalry, 0);
            ut_hussar.attackValues.Add(ac_baseMelee, 11);

            ut_paladin.armorClasses.Add(ac_baseMelee, 5);
            ut_paladin.armorClasses.Add(ac_basePierce, 7);
            ut_paladin.armorClasses.Add(ac_cavalry, 0);
            ut_paladin.attackValues.Add(ac_baseMelee, 18);

            ut_heavyCamelRider.armorClasses.Add(ac_baseMelee, 3);
            ut_heavyCamelRider.armorClasses.Add(ac_basePierce, 4);
            ut_heavyCamelRider.armorClasses.Add(ac_camel, 0);
            ut_heavyCamelRider.attackValues.Add(ac_baseMelee, 11);
            ut_heavyCamelRider.attackValues.Add(ac_cavalry, 18);
            ut_heavyCamelRider.attackValues.Add(ac_camel, 9);
            ut_heavyCamelRider.attackValues.Add(ac_mameluke, 7);

            ut_eliteBattleElephant.armorClasses.Add(ac_baseMelee, 4);
            ut_eliteBattleElephant.armorClasses.Add(ac_basePierce, 7);
            ut_eliteBattleElephant.armorClasses.Add(ac_cavalry, 0);
            ut_eliteBattleElephant.armorClasses.Add(ac_warElephant, 0);
            ut_eliteBattleElephant.attackValues.Add(ac_baseMelee, 18);
            ut_eliteBattleElephant.cleaveType = 2;
            ut_eliteBattleElephant.cleaveRadius = 0.4;

            ut_eliteSteppeLancer.armorClasses.Add(ac_baseMelee, 3);
            ut_eliteSteppeLancer.armorClasses.Add(ac_basePierce, 5);
            ut_eliteSteppeLancer.armorClasses.Add(ac_cavalry, 0);
            ut_eliteSteppeLancer.attackValues.Add(ac_baseMelee, 15);

            ut_arbalester.armorClasses.Add(ac_baseMelee, 3);
            ut_arbalester.armorClasses.Add(ac_basePierce, 4);
            ut_arbalester.armorClasses.Add(ac_archer, 0);
            ut_arbalester.attackValues.Add(ac_basePierce, 10);
            ut_arbalester.attackValues.Add(ac_spearman, 3);

            ut_eliteSkirmisher.armorClasses.Add(ac_baseMelee, 3);
            ut_eliteSkirmisher.armorClasses.Add(ac_basePierce, 8);
            ut_eliteSkirmisher.armorClasses.Add(ac_archer, 0);
            ut_eliteSkirmisher.attackValues.Add(ac_basePierce, 7);
            ut_eliteSkirmisher.attackValues.Add(ac_archer, 4);
            ut_eliteSkirmisher.attackValues.Add(ac_spearman, 3);
            ut_eliteSkirmisher.attackValues.Add(ac_cavalryArcher, 2);

            ut_heavyCavalryArcher.armorClasses.Add(ac_baseMelee, 5);
            ut_heavyCavalryArcher.armorClasses.Add(ac_basePierce, 6);
            ut_heavyCavalryArcher.armorClasses.Add(ac_archer, 0);
            ut_heavyCavalryArcher.armorClasses.Add(ac_cavalryArcher, 0);
            ut_heavyCavalryArcher.armorClasses.Add(ac_cavalry, 0);
            ut_heavyCavalryArcher.attackValues.Add(ac_basePierce, 11);
            ut_heavyCavalryArcher.attackValues.Add(ac_spearman, 6);

            ut_handCannoneer.armorClasses.Add(ac_baseMelee, 4);
            ut_handCannoneer.armorClasses.Add(ac_basePierce, 4);
            ut_handCannoneer.armorClasses.Add(ac_archer, 0);
            ut_handCannoneer.armorClasses.Add(ac_gunpowderUnit, 0);
            ut_handCannoneer.attackValues.Add(ac_basePierce, 17);
            ut_handCannoneer.attackValues.Add(ac_infantry, 10);
            ut_handCannoneer.attackValues.Add(ac_ram, 2);
            ut_handCannoneer.attackValues.Add(ac_spearman, 1);

            ut_siegeRam.armorClasses.Add(ac_baseMelee, -3);
            ut_siegeRam.armorClasses.Add(ac_basePierce, 195);
            ut_siegeRam.armorClasses.Add(ac_siegeWeapon, 0);
            ut_siegeRam.armorClasses.Add(ac_ram, 2);
            ut_siegeRam.attackValues.Add(ac_baseMelee, 4);
            ut_siegeRam.attackValues.Add(ac_siegeWeapon, 65);

            ut_heavyScorpion.armorClasses.Add(ac_baseMelee, 0);
            ut_heavyScorpion.armorClasses.Add(ac_basePierce, 7);
            ut_heavyScorpion.armorClasses.Add(ac_siegeWeapon, 0);
            ut_heavyScorpion.attackValues.Add(ac_baseMelee, 0);
            ut_heavyScorpion.attackValues.Add(ac_basePierce, 17);
            ut_heavyScorpion.attackValues.Add(ac_warElephant, 8);
            ut_heavyScorpion.attackValues.Add(ac_ram, 2);
            ut_heavyScorpion.attackIsMissile = true;
            ut_heavyScorpion.missileFlightDistance = 10.5; // scorpion missiles are always flying over a distance of 10.5 tiles, even if their attack range is only 8 tiles

            ut_eliteLongbowman.armorClasses.Add(ac_baseMelee, 3);
            ut_eliteLongbowman.armorClasses.Add(ac_basePierce, 5);
            ut_eliteLongbowman.armorClasses.Add(ac_archer, 0);
            ut_eliteLongbowman.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteLongbowman.attackValues.Add(ac_basePierce, 11);
            ut_eliteLongbowman.attackValues.Add(ac_spearman, 2);

            ut_eliteCataphract.armorClasses.Add(ac_baseMelee, 5);
            ut_eliteCataphract.armorClasses.Add(ac_basePierce, 5);
            ut_eliteCataphract.armorClasses.Add(ac_cavalry, 16);
            ut_eliteCataphract.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteCataphract.attackValues.Add(ac_baseMelee, 14);
            ut_eliteCataphract.attackValues.Add(ac_infantry, 18);
            ut_eliteCataphract.attackValues.Add(ac_condottiero, 10);
            ut_eliteCataphract.cleaveType = 1;
            ut_eliteCataphract.cleaveRadius = 0.4;

            ut_eliteWoadRaider.armorClasses.Add(ac_baseMelee, 3);
            ut_eliteWoadRaider.armorClasses.Add(ac_basePierce, 5);
            ut_eliteWoadRaider.armorClasses.Add(ac_infantry, 0);
            ut_eliteWoadRaider.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteWoadRaider.attackValues.Add(ac_baseMelee, 17);
            ut_eliteWoadRaider.attackValues.Add(ac_eagleWarrior, 3);

            ut_eliteChuKoNu.armorClasses.Add(ac_baseMelee, 3);
            ut_eliteChuKoNu.armorClasses.Add(ac_basePierce, 4);
            ut_eliteChuKoNu.armorClasses.Add(ac_archer, 0);
            ut_eliteChuKoNu.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteChuKoNu.attackValues.Add(ac_basePierce, 14);
            ut_eliteChuKoNu.attackValues.Add(ac_spearman, 2);
            ut_eliteChuKoNu.secondaryAttack = true;
            ut_eliteChuKoNu.secondaryAttackProjectileCount = 4;
            ut_eliteChuKoNu.secondaryAttackValues = new Dictionary<ArmorClass, decimal>();
            ut_eliteChuKoNu.secondaryAttackValues.Add(ac_baseMelee, 0);
            ut_eliteChuKoNu.secondaryAttackValues.Add(ac_basePierce, 3);

            ut_eliteThrowingAxeman.armorClasses.Add(ac_baseMelee, 4);
            ut_eliteThrowingAxeman.armorClasses.Add(ac_basePierce, 4);
            ut_eliteThrowingAxeman.armorClasses.Add(ac_infantry, 0);
            ut_eliteThrowingAxeman.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteThrowingAxeman.attackValues.Add(ac_baseMelee, 12);
            ut_eliteThrowingAxeman.attackValues.Add(ac_eagleWarrior, 2);

            ut_eliteHuskarl.armorClasses.Add(ac_baseMelee, 2);
            ut_eliteHuskarl.armorClasses.Add(ac_basePierce, 10);
            ut_eliteHuskarl.armorClasses.Add(ac_infantry, 0);
            ut_eliteHuskarl.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteHuskarl.attackValues.Add(ac_baseMelee, 16);
            ut_eliteHuskarl.attackValues.Add(ac_eagleWarrior, 3);
            ut_eliteHuskarl.attackValues.Add(ac_archer, 10);

            ut_eliteSamurai.armorClasses.Add(ac_baseMelee, 4);
            ut_eliteSamurai.armorClasses.Add(ac_basePierce, 5);
            ut_eliteSamurai.armorClasses.Add(ac_infantry, 0);
            ut_eliteSamurai.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteSamurai.attackValues.Add(ac_baseMelee, 16);
            ut_eliteSamurai.attackValues.Add(ac_eagleWarrior, 3);
            ut_eliteSamurai.attackValues.Add(ac_uniqueUnit, 12);

            ut_eliteMangudai.armorClasses.Add(ac_baseMelee, 4);
            ut_eliteMangudai.armorClasses.Add(ac_basePierce, 4);
            ut_eliteMangudai.armorClasses.Add(ac_archer, 0);
            ut_eliteMangudai.armorClasses.Add(ac_cavalryArcher, 0);
            ut_eliteMangudai.armorClasses.Add(ac_cavalry, 0);
            ut_eliteMangudai.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteMangudai.attackValues.Add(ac_basePierce, 12);
            ut_eliteMangudai.attackValues.Add(ac_spearman, 3);
            ut_eliteMangudai.attackValues.Add(ac_siegeWeapon, 5);

            ut_eliteWarElephant.armorClasses.Add(ac_baseMelee, 4);
            ut_eliteWarElephant.armorClasses.Add(ac_basePierce, 7);
            ut_eliteWarElephant.armorClasses.Add(ac_cavalry, 0);
            ut_eliteWarElephant.armorClasses.Add(ac_warElephant, 0);
            ut_eliteWarElephant.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteWarElephant.attackValues.Add(ac_baseMelee, 24);
            ut_eliteWarElephant.cleaveType = 2;
            ut_eliteWarElephant.cleaveRadius = 0.5;

            ut_eliteMameluke.armorClasses.Add(ac_baseMelee, 4);
            ut_eliteMameluke.armorClasses.Add(ac_basePierce, 4);
            ut_eliteMameluke.armorClasses.Add(ac_archer, 0);
            ut_eliteMameluke.armorClasses.Add(ac_mameluke, 0);
            ut_eliteMameluke.armorClasses.Add(ac_camel, 0);
            ut_eliteMameluke.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteMameluke.attackValues.Add(ac_baseMelee, 14);
            ut_eliteMameluke.attackValues.Add(ac_cavalry, 12);
            ut_eliteMameluke.attackValues.Add(ac_mameluke, 1);

            ut_eliteTeutonicKnight.armorClasses.Add(ac_baseMelee, 13);
            ut_eliteTeutonicKnight.armorClasses.Add(ac_basePierce, 6);
            ut_eliteTeutonicKnight.armorClasses.Add(ac_infantry, 0);
            ut_eliteTeutonicKnight.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteTeutonicKnight.attackValues.Add(ac_baseMelee, 21);
            ut_eliteTeutonicKnight.attackValues.Add(ac_eagleWarrior, 4);

            ut_eliteJanissary.armorClasses.Add(ac_baseMelee, 5);
            ut_eliteJanissary.armorClasses.Add(ac_basePierce, 4);
            ut_eliteJanissary.armorClasses.Add(ac_archer, 0);
            ut_eliteJanissary.armorClasses.Add(ac_gunpowderUnit, 0);
            ut_eliteJanissary.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteJanissary.attackValues.Add(ac_basePierce, 22);
            ut_eliteJanissary.attackValues.Add(ac_ram, 3);

            ut_eliteBerserk.armorClasses.Add(ac_baseMelee, 5);
            ut_eliteBerserk.armorClasses.Add(ac_basePierce, 5);
            ut_eliteBerserk.armorClasses.Add(ac_infantry, 0);
            ut_eliteBerserk.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteBerserk.attackValues.Add(ac_baseMelee, 18);
            ut_eliteBerserk.attackValues.Add(ac_eagleWarrior, 3);
            ut_eliteBerserk.attackValues.Add(ac_cavalry, 5);
            ut_eliteBerserk.attackValues.Add(ac_camel, 4);

            ut_eliteJaguarWarrior.armorClasses.Add(ac_baseMelee, 5);
            ut_eliteJaguarWarrior.armorClasses.Add(ac_basePierce, 5);
            ut_eliteJaguarWarrior.armorClasses.Add(ac_infantry, 0);
            ut_eliteJaguarWarrior.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteJaguarWarrior.attackValues.Add(ac_baseMelee, 20);
            ut_eliteJaguarWarrior.attackValues.Add(ac_infantry, 11);
            ut_eliteJaguarWarrior.attackValues.Add(ac_condottiero, 10);
            ut_eliteJaguarWarrior.attackValues.Add(ac_eagleWarrior, 2);

            ut_eliteTarkan.armorClasses.Add(ac_baseMelee, 4);
            ut_eliteTarkan.armorClasses.Add(ac_basePierce, 8);
            ut_eliteTarkan.armorClasses.Add(ac_cavalry, 0);
            ut_eliteTarkan.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteTarkan.attackValues.Add(ac_baseMelee, 15);

            ut_eliteWarWagon.armorClasses.Add(ac_baseMelee, 3);
            ut_eliteWarWagon.armorClasses.Add(ac_basePierce, 8);
            ut_eliteWarWagon.armorClasses.Add(ac_archer, 0);
            ut_eliteWarWagon.armorClasses.Add(ac_cavalryArcher, 0);
            ut_eliteWarWagon.armorClasses.Add(ac_cavalry, 0);
            ut_eliteWarWagon.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteWarWagon.attackValues.Add(ac_basePierce, 13);

            ut_elitePlumedArcher.armorClasses.Add(ac_baseMelee, 3);
            ut_elitePlumedArcher.armorClasses.Add(ac_basePierce, 6);
            ut_elitePlumedArcher.armorClasses.Add(ac_archer, 0);
            ut_elitePlumedArcher.armorClasses.Add(ac_uniqueUnit, 0);
            ut_elitePlumedArcher.attackValues.Add(ac_basePierce, 9);
            ut_elitePlumedArcher.attackValues.Add(ac_spearman, 2);
            ut_elitePlumedArcher.attackValues.Add(ac_infantry, 2);
            ut_elitePlumedArcher.attackValues.Add(ac_condottiero, 2);

            ut_eliteConquistador.armorClasses.Add(ac_baseMelee, 5);
            ut_eliteConquistador.armorClasses.Add(ac_basePierce, 6);
            ut_eliteConquistador.armorClasses.Add(ac_archer, 0);
            ut_eliteConquistador.armorClasses.Add(ac_cavalryArcher, 0);
            ut_eliteConquistador.armorClasses.Add(ac_cavalry, 0);
            ut_eliteConquistador.armorClasses.Add(ac_gunpowderUnit, 0);
            ut_eliteConquistador.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteConquistador.attackValues.Add(ac_basePierce, 18);
            ut_eliteConquistador.attackValues.Add(ac_ram, 6);

            ut_eliteKamayuk.armorClasses.Add(ac_baseMelee, 5);
            ut_eliteKamayuk.armorClasses.Add(ac_basePierce, 6);
            ut_eliteKamayuk.armorClasses.Add(ac_infantry, 0);
            ut_eliteKamayuk.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteKamayuk.attackValues.Add(ac_baseMelee, 12);
            ut_eliteKamayuk.attackValues.Add(ac_warElephant, 20);
            ut_eliteKamayuk.attackValues.Add(ac_cavalry, 12);
            ut_eliteKamayuk.attackValues.Add(ac_camel, 10);
            ut_eliteKamayuk.attackValues.Add(ac_mameluke, 1);

            ut_slinger.armorClasses.Add(ac_baseMelee, 4);
            ut_slinger.armorClasses.Add(ac_basePierce, 6);
            ut_slinger.armorClasses.Add(ac_archer, 0);
            ut_slinger.armorClasses.Add(ac_uniqueUnit, 0);
            ut_slinger.attackValues.Add(ac_basePierce, 8);
            ut_slinger.attackValues.Add(ac_infantry, 10);
            ut_slinger.attackValues.Add(ac_condottiero, 10);
            ut_slinger.attackValues.Add(ac_ram, 3);
            ut_slinger.attackValues.Add(ac_spearman, 1);

            ut_eliteElephantArcher.armorClasses.Add(ac_baseMelee, 4);
            ut_eliteElephantArcher.armorClasses.Add(ac_basePierce, 9);
            ut_eliteElephantArcher.armorClasses.Add(ac_archer, 0);
            ut_eliteElephantArcher.armorClasses.Add(ac_cavalryArcher, -2);
            ut_eliteElephantArcher.armorClasses.Add(ac_cavalry, 0);
            ut_eliteElephantArcher.armorClasses.Add(ac_warElephant, 0);
            ut_eliteElephantArcher.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteElephantArcher.attackValues.Add(ac_basePierce, 11);
            ut_eliteElephantArcher.attackValues.Add(ac_spearman, 2);

            ut_imperialCamelRider.armorClasses.Add(ac_baseMelee, 3);
            ut_imperialCamelRider.armorClasses.Add(ac_basePierce, 5);
            ut_imperialCamelRider.armorClasses.Add(ac_camel, 0);
            ut_imperialCamelRider.attackValues.Add(ac_baseMelee, 13);
            ut_imperialCamelRider.attackValues.Add(ac_cavalry, 18);
            ut_imperialCamelRider.attackValues.Add(ac_camel, 9);
            ut_imperialCamelRider.attackValues.Add(ac_mameluke, 7);

            ut_eliteGenoeseCrossbowman.armorClasses.Add(ac_baseMelee, 5);
            ut_eliteGenoeseCrossbowman.armorClasses.Add(ac_basePierce, 5);
            ut_eliteGenoeseCrossbowman.armorClasses.Add(ac_archer, 0);
            ut_eliteGenoeseCrossbowman.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteGenoeseCrossbowman.attackValues.Add(ac_basePierce, 10);
            ut_eliteGenoeseCrossbowman.attackValues.Add(ac_cavalry, 7);
            ut_eliteGenoeseCrossbowman.attackValues.Add(ac_warElephant, 7);
            ut_eliteGenoeseCrossbowman.attackValues.Add(ac_camel, 6);

            ut_condottiero.armorClasses.Add(ac_baseMelee, 4);
            ut_condottiero.armorClasses.Add(ac_basePierce, 4);
            ut_condottiero.armorClasses.Add(ac_infantry, 10);
            ut_condottiero.armorClasses.Add(ac_uniqueUnit, 0);
            ut_condottiero.armorClasses.Add(ac_condottiero, 0);
            ut_condottiero.attackValues.Add(ac_baseMelee, 13);
            ut_condottiero.attackValues.Add(ac_gunpowderUnit, 10);

            ut_eliteMagyarHuszar.armorClasses.Add(ac_baseMelee, 3);
            ut_eliteMagyarHuszar.armorClasses.Add(ac_basePierce, 6);
            ut_eliteMagyarHuszar.armorClasses.Add(ac_cavalry, 0);
            ut_eliteMagyarHuszar.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteMagyarHuszar.attackValues.Add(ac_baseMelee, 14);
            ut_eliteMagyarHuszar.attackValues.Add(ac_siegeWeapon, 8);
            ut_eliteMagyarHuszar.attackValues.Add(ac_ram, 2);

            ut_eliteBoyar.armorClasses.Add(ac_baseMelee, 9);
            ut_eliteBoyar.armorClasses.Add(ac_basePierce, 7);
            ut_eliteBoyar.armorClasses.Add(ac_cavalry, 0);
            ut_eliteBoyar.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteBoyar.attackValues.Add(ac_baseMelee, 18);

            ut_eliteCamelArcher.armorClasses.Add(ac_baseMelee, 4);
            ut_eliteCamelArcher.armorClasses.Add(ac_basePierce, 5);
            ut_eliteCamelArcher.armorClasses.Add(ac_archer, 0);
            ut_eliteCamelArcher.armorClasses.Add(ac_cavalryArcher, 0);
            ut_eliteCamelArcher.armorClasses.Add(ac_camel, 0);
            ut_eliteCamelArcher.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteCamelArcher.attackValues.Add(ac_basePierce, 12);
            ut_eliteCamelArcher.attackValues.Add(ac_cavalryArcher, 6);

            ut_eliteGenitour.armorClasses.Add(ac_baseMelee, 3);
            ut_eliteGenitour.armorClasses.Add(ac_basePierce, 8);
            ut_eliteGenitour.armorClasses.Add(ac_archer, 0);
            ut_eliteGenitour.armorClasses.Add(ac_cavalryArcher, 1);
            ut_eliteGenitour.armorClasses.Add(ac_cavalry, 0);
            ut_eliteGenitour.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteGenitour.attackValues.Add(ac_basePierce, 8);
            ut_eliteGenitour.attackValues.Add(ac_archer, 5);
            ut_eliteGenitour.attackValues.Add(ac_spearman, 2);
            ut_eliteGenitour.attackValues.Add(ac_cavalryArcher, 2);

            ut_eliteShotelWarrior.armorClasses.Add(ac_baseMelee, 3);
            ut_eliteShotelWarrior.armorClasses.Add(ac_basePierce, 5);
            ut_eliteShotelWarrior.armorClasses.Add(ac_infantry, 0);
            ut_eliteShotelWarrior.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteShotelWarrior.attackValues.Add(ac_baseMelee, 22);
            ut_eliteShotelWarrior.attackValues.Add(ac_eagleWarrior, 2);

            ut_eliteGbeto.armorClasses.Add(ac_baseMelee, 3);
            ut_eliteGbeto.armorClasses.Add(ac_basePierce, 4);
            ut_eliteGbeto.armorClasses.Add(ac_infantry, 0);
            ut_eliteGbeto.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteGbeto.attackValues.Add(ac_baseMelee, 15);
            ut_eliteGbeto.attackValues.Add(ac_eagleWarrior, 1);

            ut_eliteOrganGun.armorClasses.Add(ac_baseMelee, 2);
            ut_eliteOrganGun.armorClasses.Add(ac_basePierce, 6);
            ut_eliteOrganGun.armorClasses.Add(ac_siegeWeapon, 0);
            ut_eliteOrganGun.armorClasses.Add(ac_gunpowderUnit, 0);
            ut_eliteOrganGun.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteOrganGun.attackValues.Add(ac_basePierce, 20);
            ut_eliteOrganGun.attackValues.Add(ac_ram, 1);
            ut_eliteOrganGun.secondaryAttack = true;
            ut_eliteOrganGun.secondaryAttackProjectileCount = 4;
            ut_eliteOrganGun.secondaryAttackValues = new Dictionary<ArmorClass, decimal>();
            ut_eliteOrganGun.secondaryAttackValues.Add(ac_basePierce, 2);

            ut_eliteArambai.armorClasses.Add(ac_baseMelee, 1);
            ut_eliteArambai.armorClasses.Add(ac_basePierce, 3);
            ut_eliteArambai.armorClasses.Add(ac_archer, 0);
            ut_eliteArambai.armorClasses.Add(ac_cavalryArcher, 0);
            ut_eliteArambai.armorClasses.Add(ac_cavalry, 0);
            ut_eliteArambai.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteArambai.attackValues.Add(ac_basePierce, 19);
            ut_eliteArambai.attackValues.Add(ac_ram, 2);

            ut_eliteBallistaElephant.armorClasses.Add(ac_baseMelee, 3);
            ut_eliteBallistaElephant.armorClasses.Add(ac_basePierce, 7);
            ut_eliteBallistaElephant.armorClasses.Add(ac_cavalry, -2);
            ut_eliteBallistaElephant.armorClasses.Add(ac_warElephant, -2);
            ut_eliteBallistaElephant.armorClasses.Add(ac_siegeWeapon, -2);
            ut_eliteBallistaElephant.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteBallistaElephant.attackValues.Add(ac_basePierce, 10);
            ut_eliteBallistaElephant.attackIsMissile = true;
            ut_eliteBallistaElephant.missileFlightDistance = 6.0;
            ut_eliteBallistaElephant.secondaryMissileFlightDistance = 12.5;
            ut_eliteBallistaElephant.secondaryAttack = true;
            ut_eliteBallistaElephant.secondaryAttackValues = new Dictionary<ArmorClass, decimal>();
            ut_eliteBallistaElephant.secondaryAttackValues.Add(ac_basePierce, 7);

            ut_eliteKarambitWarrior.armorClasses.Add(ac_baseMelee, 4);
            ut_eliteKarambitWarrior.armorClasses.Add(ac_basePierce, 5);
            ut_eliteKarambitWarrior.armorClasses.Add(ac_infantry, 0);
            ut_eliteKarambitWarrior.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteKarambitWarrior.attackValues.Add(ac_baseMelee, 11);
            ut_eliteKarambitWarrior.attackValues.Add(ac_eagleWarrior, 2);

            ut_eliteRattanArcher.armorClasses.Add(ac_baseMelee, 3);
            ut_eliteRattanArcher.armorClasses.Add(ac_basePierce, 10);
            ut_eliteRattanArcher.armorClasses.Add(ac_archer, 0);
            ut_eliteRattanArcher.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteRattanArcher.attackValues.Add(ac_basePierce, 11);
            ut_eliteRattanArcher.attackValues.Add(ac_spearman, 2);

            ut_imperialSkirmisher.armorClasses.Add(ac_baseMelee, 3);
            ut_imperialSkirmisher.armorClasses.Add(ac_basePierce, 9);
            ut_imperialSkirmisher.armorClasses.Add(ac_archer, 0);
            ut_imperialSkirmisher.attackValues.Add(ac_basePierce, 8);
            ut_imperialSkirmisher.attackValues.Add(ac_archer, 5);
            ut_imperialSkirmisher.attackValues.Add(ac_spearman, 3);
            ut_imperialSkirmisher.attackValues.Add(ac_cavalryArcher, 3);

            ut_eliteKonnik.armorClasses.Add(ac_baseMelee, 5);
            ut_eliteKonnik.armorClasses.Add(ac_basePierce, 6);
            ut_eliteKonnik.armorClasses.Add(ac_cavalry, 0);
            ut_eliteKonnik.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteKonnik.attackValues.Add(ac_baseMelee, 18);

            ut_eliteKonnikDismounted.armorClasses.Add(ac_baseMelee, 3);
            ut_eliteKonnikDismounted.armorClasses.Add(ac_basePierce, 5);
            ut_eliteKonnikDismounted.armorClasses.Add(ac_infantry, 0);
            ut_eliteKonnikDismounted.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteKonnikDismounted.attackValues.Add(ac_baseMelee, 17);

            ut_eliteKipchak.armorClasses.Add(ac_baseMelee, 4);
            ut_eliteKipchak.armorClasses.Add(ac_basePierce, 6);
            ut_eliteKipchak.armorClasses.Add(ac_archer, 0);
            ut_eliteKipchak.armorClasses.Add(ac_cavalryArcher, 0);
            ut_eliteKipchak.armorClasses.Add(ac_cavalry, 0);
            ut_eliteKipchak.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteKipchak.attackValues.Add(ac_basePierce, 8);
            ut_eliteKipchak.attackValues.Add(ac_spearman, 3);
            ut_eliteKipchak.secondaryAttack = true;
            ut_eliteKipchak.secondaryAttackProjectileCount = 3;
            ut_eliteKipchak.secondaryAttackValues = new Dictionary<ArmorClass, decimal>();
            ut_eliteKipchak.secondaryAttackValues.Add(ac_baseMelee, 0);
            ut_eliteKipchak.secondaryAttackValues.Add(ac_basePierce, 3);

            ut_eliteLeitis.armorClasses.Add(ac_baseMelee, 5);
            ut_eliteLeitis.armorClasses.Add(ac_basePierce, 6);
            ut_eliteLeitis.armorClasses.Add(ac_cavalry, 0);
            ut_eliteLeitis.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteLeitis.attackValues.Add(ac_baseMelee, 18);

            ut_eliteKeshik.armorClasses.Add(ac_baseMelee, 4);
            ut_eliteKeshik.armorClasses.Add(ac_basePierce, 7);
            ut_eliteKeshik.armorClasses.Add(ac_cavalry, 0);
            ut_eliteKeshik.armorClasses.Add(ac_uniqueUnit, 0);
            ut_eliteKeshik.attackValues.Add(ac_baseMelee, 15);

            ut_flamingCamel.armorClasses.Add(ac_baseMelee, 0);
            ut_flamingCamel.armorClasses.Add(ac_basePierce, 0);
            ut_flamingCamel.armorClasses.Add(ac_camel, 0);
            ut_flamingCamel.armorClasses.Add(ac_uniqueUnit, 0);
            ut_flamingCamel.attackValues.Add(ac_baseMelee, 20);
            ut_flamingCamel.attackValues.Add(ac_cavalry, 50);
            ut_flamingCamel.attackValues.Add(ac_camel, 50);
            ut_flamingCamel.attackValues.Add(ac_warElephant, 100);
            ut_flamingCamel.cleaveType = 3;
            ut_flamingCamel.cleaveRadius = 1.5;
        }
    }
}
