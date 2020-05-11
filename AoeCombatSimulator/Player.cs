using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AoeCombatSimulator
{
    public class Player
    {
        public int sumWins = 0; // number of battle wins of player
        public int attackAttacker = 0; // DEBUG purposes
        public int attackRandomNearbyTarget = 0; // DEBUG purposes
        public int regularHit = 0;
        public int missTotalMainTargetAlive = 0;
        public int missTotalMainTargetDead = 0;
        public int missMainTarget = 0;
        public int missSideTarget = 0;
        public ConcurrentDictionary<UnitType, int> survivorsSumArmy = new ConcurrentDictionary<UnitType, int>(Environment.ProcessorCount, 101); // counts for each unit type the sum of survivors of all battles
        public int[] resourcesInvested = new int[3]; // worth (food, wood, gold) of all starting units
        public int[] resourcesRemaining = new int[3]; // worth (food, wood, gold) of all surviving units
        public int[] resourcesGenerated = new int[3]; // sum of all generated resources (currently only for Keshiks)
        public List<int> amountStartUnits = new List<int>();


        // Player GUI //
        public Color playerColor;
        public Label armyLabel;
        public Label numberOfUnitsLabel;
        public Label numberOfAverageSurvivorsLabel;
        public Label resourcesInvestedLabel;
        public TextBox[] resourcesInvestedTextboxes = new TextBox[3];
        public Label[] resourcesInvestedLabels = new Label[3];
        public TextBox totalResourcesInvestedTextbox;
        public Label resourcesLostLabel;
        public TextBox[] resourcesLostTextboxes = new TextBox[3];
        public Label[] resourcesLostLabels = new Label[3];
        public TextBox totalResourcesLostTextbox;
        public Label sumWinsLabel;
        public TextBox sumWinsTextbox;
        public List<Label> utNameLabel = new List<Label>();
        public List<TextBox> enterAmountTextbox = new List<TextBox>();
        public List<TextBox> avgSurvivorsTextbox = new List<TextBox>();

        public Player(Color playerColor)
        {
            AoeData.unitTypesList.ForEach(ut => { survivorsSumArmy[ut] = 0; });
            this.playerColor = playerColor;
        }

        public void ResetData()
        {
            sumWins = 0;
            for (int i = 0; i < 3; i++)
            {
                resourcesInvested[i] = 0;
                resourcesRemaining[i] = 0;
                resourcesGenerated[i] = 0;
            }
            attackAttacker = 0;
            attackRandomNearbyTarget = 0;
            AoeData.unitTypesList.ForEach(ut => { survivorsSumArmy[ut] = 0; });
        }


    }
}
