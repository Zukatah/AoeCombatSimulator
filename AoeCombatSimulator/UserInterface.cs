using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AoeCombatSimulator
{
    public class UserInterface : Form
    {
        public int numberOfFights;
        public short hitAndRunMode; // 0=noHit&Run, 1=semi, 2=fullHit&Run
        public Player[] players = new Player[2];
        public ComboBox hitAndRunSettingsCombobox;
        public Label numberOfSimulationsLabel;
        public TextBox numberOfSimulationsTextbox;
        public Button startSimulationButton;


        public UserInterface()
        {
            InitializeComponent();
            players[0] = new Player(Color.FromArgb(0, 0, 128), this, 0);
            players[1] = new Player(Color.FromArgb(128, 0, 0), this, 1);
            InitializeSimulatorGui();
        }


        private void InitializeComponent() // required for designer support
        {
            SuspendLayout();

            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(1184, 987);
            DoubleBuffered = true;
            Text = "AoE Combat Simulator";
            ResumeLayout(false);
        }


        private void InitializeSimulatorGui()
        {
            hitAndRunSettingsCombobox = new ComboBox();
            hitAndRunSettingsCombobox.Location = new Point(540, 840);
            hitAndRunSettingsCombobox.Size = new Size(120, 20);
            hitAndRunSettingsCombobox.DropDownStyle = ComboBoxStyle.DropDownList;
            hitAndRunSettingsCombobox.FormattingEnabled = true;
            hitAndRunSettingsCombobox.Items.AddRange(new object[] { "No Hit&Run", "Medium Hit&Run (50% efficiency)", "Perfect Hit&Run" });
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

        private void PrintResults()
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < AoeData.unitTypesList.Count; j++)
                {
                    double avgSurv = 1.0 * players[i].survivorsSumArmy[AoeData.unitTypesList[j]] / numberOfFights;
                    double avgSurvPerc = avgSurv / players[i].amountStartUnits[j];
                    players[i].avgSurvivorsTextbox[j].Text = avgSurv.ToString();
                    players[i].avgSurvivorsTextbox[j].BackColor = players[i].amountStartUnits[j] == 0 ? Color.FromArgb(128, 128, 128) :
                        Color.FromArgb(255 - (int)(128.0 * avgSurvPerc), 127 + (int)(128.0 * avgSurvPerc), 0);
                    // 0.0 <-> (255, 128, 128), <=0.1 <-> (255, 192, 192), <=0.5 <-> (255, 255, 192), <=0.9 <-> (192, 255, 192), >0.9 <-> (128, 255, 128)

                    for (int k = 0; k < 3; k++)
                    {
                        players[i].resourcesRemaining[k] += (int)Math.Round(AoeData.unitTypesList[j].resourceCosts[k] * avgSurv);
                    }
                }

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
                    for (int j = 0; j < AoeData.unitTypesList.Count; j++)
                    {
                        players[i].amountStartUnits[j] = Int32.Parse(players[i].enterAmountTextbox[j].Text);
                        for (int k = 0; k < 3; k++)
                        {
                            players[i].resourcesInvested[k] += AoeData.unitTypesList[j].resourceCosts[k] * players[i].amountStartUnits[j];
                        }
                    }
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
                Console.WriteLine("Army " + (i + 1) + ": Attacking attacker: " + players[i].attackAttacker + ". Attacking random nearby target: " + players[i].attackRandomNearbyTarget + ".");
                Console.WriteLine(players[i] + " Hit: " + players[i].regularHit + " Total Miss MTAlive: " + players[i].missTotalMainTargetAlive + " Total Miss MTDead: " + players[i].missTotalMainTargetDead + " Miss Main Target: " + players[i].missMainTarget + " Miss Side Target: " + players[i].missSideTarget);
            }
            Console.WriteLine("Elapsed time for simulation: " + watch.Elapsed);
        }
    }
}