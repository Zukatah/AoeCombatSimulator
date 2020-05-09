using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AoeCombatSimulator
{
    public partial class Form1 : Form
    {
        public static int numberOfFights;
        public short hitAndRunMode; // 0=noHit&Run, 1=semi, 2=fullHit&Run
        public static Player[] players = new Player[2] { new Player(Color.FromArgb(0, 0, 128)), new Player(Color.FromArgb(128, 0, 0)) };
        public static ComboBox hitAndRunSettingsCombobox;
        public static Label numberOfSimulationsLabel;
        public static TextBox numberOfSimulationsTextbox;
        public static Button startSimulationButton;



        public Form1()
        {
            InitializeComponent();
            InitializeUnitTypes();
            InitializeSimulatorGui();
            InitializePlayerGui();
        }

        private void InitializeSimulatorGui()
        {
            hitAndRunSettingsCombobox = new ComboBox();
            hitAndRunSettingsCombobox.Location = new Point(540, 840);
            hitAndRunSettingsCombobox.Size = new Size(120, 20);
            hitAndRunSettingsCombobox.DropDownStyle = ComboBoxStyle.DropDownList;
            hitAndRunSettingsCombobox.FormattingEnabled = true;
            hitAndRunSettingsCombobox.Items.AddRange(new object[] {"No Hit&Run", "Medium Hit&Run (50% efficiency)", "Perfect Hit&Run"});
            hitAndRunSettingsCombobox.SelectedIndex = 0;
            Controls.Add(hitAndRunSettingsCombobox);

            numberOfSimulationsLabel = new Label();
            numberOfSimulationsLabel.Location = new Point(560, 864);
            numberOfSimulationsLabel.Text = "#Fights";
            numberOfSimulationsLabel.AutoSize = true;
            Controls.Add(numberOfSimulationsLabel);
            numberOfSimulationsTextbox = new TextBox();
            numberOfSimulationsTextbox.Location = new Point(560, 879);
            numberOfSimulationsTextbox.Size = new Size(80, 20);
            numberOfSimulationsTextbox.Text = "200";
            Controls.Add(numberOfSimulationsTextbox);

            startSimulationButton = new Button();
            startSimulationButton.BackgroundImage = Properties.Resources.swords_2453295_1280;
            startSimulationButton.BackgroundImageLayout = ImageLayout.Zoom;
            startSimulationButton.Location = new Point(560, 900);
            startSimulationButton.Size = new Size(80, 40);
            startSimulationButton.UseVisualStyleBackColor = true;
            startSimulationButton.Click += new EventHandler(bt_fight_Click);
            Controls.Add(startSimulationButton);
        }

        private void InitializePlayerGui()
        {
            for (int i = 0; i < 2; i++)
            {
                players[i].numberOfUnitsLabel = new Label();
                players[i].numberOfUnitsLabel.Location = new Point(160 + 600 * i, 185);
                players[i].numberOfUnitsLabel.Text = "#Units";
                players[i].numberOfUnitsLabel.AutoSize = true;
                players[i].numberOfUnitsLabel.ForeColor = players[i].playerColor;
                Controls.Add(players[i].numberOfUnitsLabel);
                players[i].numberOfAverageSurvivorsLabel = new Label();
                players[i].numberOfAverageSurvivorsLabel.Location = new Point(211 + 600 * i, 185);
                players[i].numberOfAverageSurvivorsLabel.Text = "#Avg. Survivors";
                players[i].numberOfAverageSurvivorsLabel.AutoSize = true;
                players[i].numberOfAverageSurvivorsLabel.ForeColor = players[i].playerColor;
                Controls.Add(players[i].numberOfAverageSurvivorsLabel);

                int tbIndex = 0;
                unitTypesList.ForEach(ut =>
                {
                    Label label = new Label();
                    label.Location = new Point(10 + 600 * i + (tbIndex / 29) * 275, 200 + 21 * (tbIndex % 29));
                    label.Text = ut.name;
                    label.AutoSize = true;
                    label.ForeColor = players[i].playerColor;
                    Controls.Add(label);
                    ut.enterAmountTextbox[i] = new TextBox();
                    ut.enterAmountTextbox[i].Location = new Point(160 + 600 * i + (tbIndex / 29) * 275, 200 + 21 * (tbIndex % 29));
                    ut.enterAmountTextbox[i].Size = new Size(50, 20);
                    ut.enterAmountTextbox[i].Text = "0";
                    Controls.Add(ut.enterAmountTextbox[i]);
                    ut.avgSurvivorsTextbox[i] = new TextBox();
                    ut.avgSurvivorsTextbox[i].Location = new Point(211 + 600 * i + (tbIndex / 29) * 275, 200 + 21 * (tbIndex % 29));
                    ut.avgSurvivorsTextbox[i].Size = new Size(50, 20);
                    ut.avgSurvivorsTextbox[i].ReadOnly = true;
                    Controls.Add(ut.avgSurvivorsTextbox[i]);
                    tbIndex++;
                });

                players[i].armyLabel = new Label();
                players[i].armyLabel.Location = new Point(220 + 600 * i, 10);
                players[i].armyLabel.Text = "Army " + (i+1);
                players[i].armyLabel.AutoSize = true;
                players[i].armyLabel.ForeColor = players[i].playerColor;
                Controls.Add(players[i].armyLabel);

                players[i].resourcesInvestedLabel = new Label();
                players[i].resourcesInvestedLabel.Location = new Point(160 + 600 * i, 60);
                players[i].resourcesInvestedLabel.Text = "Resources invested";
                players[i].resourcesInvestedLabel.AutoSize = true;
                players[i].resourcesInvestedLabel.ForeColor = players[i].playerColor;
                Controls.Add(players[i].resourcesInvestedLabel);
                players[i].resourcesLostLabel = new Label();
                players[i].resourcesLostLabel.Location = new Point(160 + 600 * i, 850);
                players[i].resourcesLostLabel.Text = "Resources lost";
                players[i].resourcesLostLabel.AutoSize = true;
                players[i].resourcesLostLabel.ForeColor = players[i].playerColor;
                Controls.Add(players[i].resourcesLostLabel);

                for (int j=0; j < 3; j++)
                {
                    players[i].resourcesInvestedLabels[j] = new Label();
                    players[i].resourcesInvestedLabels[j].Location = new Point(160 + 81 * j + 600 * i, 79);
                    players[i].resourcesInvestedLabels[j].Size = new Size(80, 20);
                    players[i].resourcesInvestedLabels[j].Image = resourceImages[j];
                    Controls.Add(players[i].resourcesInvestedLabels[j]);
                    players[i].resourcesInvestedTextboxes[j] = new TextBox();
                    players[i].resourcesInvestedTextboxes[j].ReadOnly = true;
                    players[i].resourcesInvestedTextboxes[j].Location = new Point(160 + 81 * j + 600 * i, 100);
                    players[i].resourcesInvestedTextboxes[j].Size = new Size(80, 20);
                    Controls.Add(players[i].resourcesInvestedTextboxes[j]);

                    players[i].resourcesLostLabels[j] = new Label();
                    players[i].resourcesLostLabels[j].Location = new Point(160 + 81 * j + 600 * i, 869);
                    players[i].resourcesLostLabels[j].Size = new Size(80, 20);
                    players[i].resourcesLostLabels[j].Image = resourceImages[j];
                    Controls.Add(players[i].resourcesLostLabels[j]);
                    players[i].resourcesLostTextboxes[j] = new TextBox();
                    players[i].resourcesLostTextboxes[j].ReadOnly = true;
                    players[i].resourcesLostTextboxes[j].Location = new Point(160 + 81 * j + 600 * i, 890);
                    players[i].resourcesLostTextboxes[j].Size = new Size(80, 20);
                    Controls.Add(players[i].resourcesLostTextboxes[j]);
                }

                players[i].totalResourcesInvestedTextbox = new TextBox();
                players[i].totalResourcesInvestedTextbox.ReadOnly = true;
                players[i].totalResourcesInvestedTextbox.Location = new Point(160 + 600 * i, 121);
                players[i].totalResourcesInvestedTextbox.Size = new Size(242, 20);
                Controls.Add(players[i].totalResourcesInvestedTextbox);
                players[i].totalResourcesLostTextbox = new TextBox();
                players[i].totalResourcesLostTextbox.ReadOnly = true;
                players[i].totalResourcesLostTextbox.Location = new Point(160 + 600 * i, 911);
                players[i].totalResourcesLostTextbox.Size = new Size(242, 20);
                Controls.Add(players[i].totalResourcesLostTextbox);

                players[i].sumWinsLabel = new Label();
                players[i].sumWinsLabel.Location = new Point(220 + 600 * i, 935);
                players[i].sumWinsLabel.Text = "#Wins Army " + (i + 1);
                players[i].sumWinsLabel.AutoSize = true;
                players[i].sumWinsLabel.ForeColor = players[i].playerColor;
                Controls.Add(players[i].sumWinsLabel);
                players[i].sumWinsTextbox = new TextBox();
                players[i].sumWinsTextbox.ReadOnly = true;
                players[i].sumWinsTextbox.Location = new Point(220 + 600 * i, 950);
                players[i].sumWinsTextbox.Size = new Size(80, 20);
                Controls.Add(players[i].sumWinsTextbox);
            }
        }
        
        private void PrintResults()
        {
            for (int i = 0; i < 2; i++)
            {
                unitTypesList.ForEach(ut => {
                    double avgSurv = 1.0 * players[i].survivorsSumArmy[ut] / numberOfFights;
                    double avgSurvPerc = avgSurv / ut.amountStartUnits[i];
                    ut.avgSurvivorsTextbox[i].Text = avgSurv.ToString();
                    ut.avgSurvivorsTextbox[i].BackColor = ut.amountStartUnits[i] == 0 ? Color.FromArgb(128,128,128) : (avgSurvPerc == 0.0 ? Color.FromArgb(255, 128, 128) : (avgSurvPerc <= 0.1 ? Color.FromArgb(255, 192, 192) : (avgSurvPerc <= 0.5 ? Color.FromArgb(255, 255, 192) : (avgSurvPerc <= 0.9 ? Color.FromArgb(192, 255, 192) : Color.FromArgb(128, 255, 128)))));
                    for (int j = 0; j < 3; j++)
                    {
                        players[i].resourcesRemaining[j] += (int)Math.Round(ut.resourceCosts[j] * avgSurv);
                    }
                });

                for (int j = 0; j < 3; j++)
                {
                    players[i].resourcesRemaining[j] += players[i].resourcesGenerated[j] / numberOfFights;
                    players[i].resourcesLostTextboxes[j].Text = (players[i].resourcesInvested[j] - players[i].resourcesRemaining[j]).ToString();
                }
                players[i].totalResourcesLostTextbox.Text = ((players[i].resourcesInvested[0] - players[i].resourcesRemaining[0])
                    + (players[i].resourcesInvested[1] - players[i].resourcesRemaining[1])
                    + (players[i].resourcesInvested[2] - players[i].resourcesRemaining[2])).ToString();
                players[i].sumWinsTextbox.Text = (players[i].sumWins / 2.0).ToString();
            }
        }

        private void bt_fight_Click(object sender, EventArgs e)
        {
            try
            {
                numberOfFights = Int32.Parse(numberOfSimulationsTextbox.Text);
                for (int i = 0; i < 2; i++)
                {
                    players[i].ResetData();
                    unitTypesList.ForEach(ut => {
                        ut.amountStartUnits[i] = Int32.Parse(ut.enterAmountTextbox[i].Text);
                        for (int j = 0; j < 3; j++)
                        {
                            players[i].resourcesInvested[j] += ut.resourceCosts[j] * ut.amountStartUnits[i];
                        }
                    });
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An integer value must be assigned to each textbox.", "Invalid inputs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    players[i].resourcesInvestedTextboxes[j].Text = players[i].resourcesInvested[j].ToString();
                }
                players[i].totalResourcesInvestedTextbox.Text = (players[i].resourcesInvested[0] + players[i].resourcesInvested[1] + players[i].resourcesInvested[2]).ToString();
            }

            hitAndRunMode = (short)hitAndRunSettingsCombobox.SelectedIndex;

            Stopwatch watch = new Stopwatch();
            watch.Start();
            
            int numTasks = Environment.ProcessorCount;
            int numFights = numberOfFights;
            var tasks = new Task[numTasks];
            for (int taskId = 0; taskId < numTasks; taskId++)
            {
                int taskIdCopy = taskId;
                int numFightsForTask = numFights / numTasks + (taskIdCopy < numFights % numTasks ? 1 : 0);
                tasks[taskId] = Task.Factory.StartNew(() => {
                    for (int i = 0; i < numFightsForTask; i++)
                    new Battle(this, taskIdCopy, i, hitAndRunMode);
                });
            }
            Task.WaitAll(tasks);
            PrintResults();

            watch.Stop();

            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine("Army " + (i+1) + ": Attacking attacker: " + players[i].attackAttacker + ". Attacking random nearby target: " + players[i].attackRandomNearbyTarget + ".");
                Console.WriteLine(players[i] + " Hit: " + players[i].regularHit + " Total Miss MTAlive: " + players[i].missTotalMainTargetAlive + " Total Miss MTDead: " + players[i].missTotalMainTargetDead + " Miss Main Target: " + players[i].missMainTarget + " Miss Side Target: " + players[i].missSideTarget);
            }
            Console.WriteLine("Elapsed time for simulation: " + watch.Elapsed);
        }
    }
}