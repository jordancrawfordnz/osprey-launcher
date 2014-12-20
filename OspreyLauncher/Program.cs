using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OspreyLauncher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LauncherController controller = LauncherController.GetInstance(); // ensure the controller gets constructed
            try
            {
                Application.Run(LauncherWindow.GetInstance());
            }
            catch (Exception ex)
            {
                if (Program.DebugMode) throw;
                else MessageBox.Show("The error is:\n" + ex.Message, "Something bad happened!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            controller.CloseLauncher();
        }

        public static bool DebugMode = false;
    }
}
