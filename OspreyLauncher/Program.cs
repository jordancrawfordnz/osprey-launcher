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
            Application.Run(LauncherWindow.GetInstance());
            controller.CloseLauncher();
        }
    }
}
