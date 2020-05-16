using System;
using System.Windows.Forms;

namespace AoeCombatSimulator
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AoeData.InitializeUnitTypes();
            Application.Run(new UserInterface());
        }
    }
}
