﻿using System;
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
        public UserInterface userInterface; // a reference to the user interface instance to which this player's gui elements will be added
        public int sumWins = 0; // number of battle wins (actually win = 2 points, draw = 1 point)
        public int attackAttacker = 0; // DEBUG purposes
        public int attackRandomNearbyTarget = 0; // DEBUG purposes
        public int regularHit = 0; // DEBUG purposes
        public int missTotalMainTargetAlive = 0; // DEBUG purposes
        public int missTotalMainTargetDead = 0; // DEBUG purposes
        public int missMainTarget = 0; // DEBUG purposes
        public int missSideTarget = 0; // DEBUG purposes
        public ConcurrentDictionary<UnitType, int> survivorsSumArmy = new ConcurrentDictionary<UnitType, int>(Environment.ProcessorCount, 101); // the sum of survivors of all battles by unit type
        public int[] resourcesInvested = new int[3]; // worth (food, wood, gold) of all starting units
        public int[] resourcesRemaining = new int[3]; // worth (food, wood, gold) of all surviving units
        public int[] resourcesGenerated = new int[3]; // sum of all generated resources (currently only for Keshiks)
        public List<int> amountStartUnits = new List<int>(); // contains the number of start units of each unit type; a list of all unit types can be found in the static AoeData.cs class
        public int playerIndex; // currently either 0 (first player) or 1 (second player)


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

        public Player(Color playerColor, UserInterface userInterface, int playerIndex)
        {
            AoeData.unitTypesList.ForEach(ut => { survivorsSumArmy[ut] = 0; });
            this.playerColor = playerColor;
            this.userInterface = userInterface;
            this.playerIndex = playerIndex;
            CreatePlayerUIElements();
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

        public void CreatePlayerUIElements()
        {
            numberOfUnitsLabel = new Label();
            numberOfUnitsLabel.Location = new Point(160 + 600 * playerIndex, 185);
            numberOfUnitsLabel.Text = "#Units";
            numberOfUnitsLabel.AutoSize = true;
            numberOfUnitsLabel.ForeColor = playerColor;
            userInterface.Controls.Add(numberOfUnitsLabel);
            numberOfAverageSurvivorsLabel = new Label();
            numberOfAverageSurvivorsLabel.Location = new Point(211 + 600 * playerIndex, 185);
            numberOfAverageSurvivorsLabel.Text = "#Avg. Survivors";
            numberOfAverageSurvivorsLabel.AutoSize = true;
            numberOfAverageSurvivorsLabel.ForeColor = playerColor;
            userInterface.Controls.Add(numberOfAverageSurvivorsLabel);

            for (int j = 0; j < AoeData.unitTypesList.Count; j++)
            {
                amountStartUnits.Add(0);
                utNameLabel.Add(new Label());
                utNameLabel[j].Location = new Point(10 + 600 * playerIndex + (j / 29) * 275, 200 + 21 * (j % 29));
                utNameLabel[j].Text = AoeData.unitTypesList[j].name;
                utNameLabel[j].AutoSize = true;
                utNameLabel[j].ForeColor = playerColor;
                userInterface.Controls.Add(utNameLabel[j]);
                enterAmountTextbox.Add(new TextBox());
                enterAmountTextbox[j].Location = new Point(160 + 600 * playerIndex + (j / 29) * 275, 200 + 21 * (j % 29));
                enterAmountTextbox[j].Size = new Size(50, 20);
                enterAmountTextbox[j].Text = "0";
                userInterface.Controls.Add(enterAmountTextbox[j]);
                avgSurvivorsTextbox.Add(new TextBox());
                avgSurvivorsTextbox[j].Location = new Point(211 + 600 * playerIndex + (j / 29) * 275, 200 + 21 * (j % 29));
                avgSurvivorsTextbox[j].Size = new Size(50, 20);
                avgSurvivorsTextbox[j].ReadOnly = true;
                userInterface.Controls.Add(avgSurvivorsTextbox[j]);
            }

            armyLabel = new Label();
            armyLabel.Location = new Point(220 + 600 * playerIndex, 10);
            armyLabel.Text = "Army " + (playerIndex + 1);
            armyLabel.AutoSize = true;
            armyLabel.ForeColor = playerColor;
            userInterface.Controls.Add(armyLabel);

            resourcesInvestedLabel = new Label();
            resourcesInvestedLabel.Location = new Point(160 + 600 * playerIndex, 60);
            resourcesInvestedLabel.Text = "Resources invested";
            resourcesInvestedLabel.AutoSize = true;
            resourcesInvestedLabel.ForeColor = playerColor;
            userInterface.Controls.Add(resourcesInvestedLabel);
            resourcesLostLabel = new Label();
            resourcesLostLabel.Location = new Point(160 + 600 * playerIndex, 850);
            resourcesLostLabel.Text = "Resources lost";
            resourcesLostLabel.AutoSize = true;
            resourcesLostLabel.ForeColor = playerColor;
            userInterface.Controls.Add(resourcesLostLabel);

            for (int j = 0; j < 3; j++)
            {
                resourcesInvestedLabels[j] = new Label();
                resourcesInvestedLabels[j].Location = new Point(160 + 81 * j + 600 * playerIndex, 79);
                resourcesInvestedLabels[j].Size = new Size(80, 20);
                resourcesInvestedLabels[j].Image = AoeData.resourceImages[j];
                userInterface.Controls.Add(resourcesInvestedLabels[j]);
                resourcesInvestedTextboxes[j] = new TextBox();
                resourcesInvestedTextboxes[j].ReadOnly = true;
                resourcesInvestedTextboxes[j].Location = new Point(160 + 81 * j + 600 * playerIndex, 100);
                resourcesInvestedTextboxes[j].Size = new Size(80, 20);
                userInterface.Controls.Add(resourcesInvestedTextboxes[j]);

                resourcesLostLabels[j] = new Label();
                resourcesLostLabels[j].Location = new Point(160 + 81 * j + 600 * playerIndex, 869);
                resourcesLostLabels[j].Size = new Size(80, 20);
                resourcesLostLabels[j].Image = AoeData.resourceImages[j];
                userInterface.Controls.Add(resourcesLostLabels[j]);
                resourcesLostTextboxes[j] = new TextBox();
                resourcesLostTextboxes[j].ReadOnly = true;
                resourcesLostTextboxes[j].Location = new Point(160 + 81 * j + 600 * playerIndex, 890);
                resourcesLostTextboxes[j].Size = new Size(80, 20);
                userInterface.Controls.Add(resourcesLostTextboxes[j]);
            }

            totalResourcesInvestedTextbox = new TextBox();
            totalResourcesInvestedTextbox.ReadOnly = true;
            totalResourcesInvestedTextbox.Location = new Point(160 + 600 * playerIndex, 121);
            totalResourcesInvestedTextbox.Size = new Size(242, 20);
            userInterface.Controls.Add(totalResourcesInvestedTextbox);
            totalResourcesLostTextbox = new TextBox();
            totalResourcesLostTextbox.ReadOnly = true;
            totalResourcesLostTextbox.Location = new Point(160 + 600 * playerIndex, 911);
            totalResourcesLostTextbox.Size = new Size(242, 20);
            userInterface.Controls.Add(totalResourcesLostTextbox);

            sumWinsLabel = new Label();
            sumWinsLabel.Location = new Point(220 + 600 * playerIndex, 935);
            sumWinsLabel.Text = "#Wins Army " + (playerIndex + 1);
            sumWinsLabel.AutoSize = true;
            sumWinsLabel.ForeColor = playerColor;
            userInterface.Controls.Add(sumWinsLabel);
            sumWinsTextbox = new TextBox();
            sumWinsTextbox.ReadOnly = true;
            sumWinsTextbox.Location = new Point(220 + 600 * playerIndex, 950);
            sumWinsTextbox.Size = new Size(80, 20);
            userInterface.Controls.Add(sumWinsTextbox);
        }
    }
}
