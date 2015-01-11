using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CefSharp;
using System.Diagnostics;

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
           try
            {
                Cef.Initialize(new CefSettings());
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                LauncherController controller = LauncherController.GetInstance();
                UserSettings settings = UserSettings.GetInstance();
                Application.Run(LauncherWindow.GetInstance());
            }
            catch (SettingsNotFoundException ex)
            {
//                LauncherWindow.GetInstance().displayMessage("No configuration file was found.\nPlease run the configuration maker before using OspreyLauncher.");
                MessageBox.Show("No configuration file was found.\nPlease run the configuration maker before using OspreyLauncher.");
                return; // If the settings cannot be found, close the application.
            }
            catch (Exception ex)
            {
                if (UserSettings.GetInstance().isInDebugMode()) throw;
                else MessageBox.Show("The error is:\n" + ex.Message, "Something bad happened!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Process.GetCurrentProcess().Kill();
        }
    }
}
