using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OspreyLauncher
{
    public class LauncherController
    {
        static LauncherController instance = null;
        LauncherWindow launcherWindow;
        LaunchableApplication currentApplication = null;

        public static LauncherController GetInstance()
        {
            if(instance == null)
                instance = new LauncherController();
            return instance;
        }

        private LauncherController()
        {
            launcherWindow = LauncherWindow.GetInstance();
            Hotkey.SetupKeyHook();
            setupApplications();
        }


        void setupApplications()
        {
            applications.Add(new LaunchableApplication("MediaPortal",
    "C:\\Program Files (x86)\\Team MediaPortal\\MediaPortal\\MediaPortal.exe", true));

        }
        List<LaunchableApplication> applications = new List<LaunchableApplication>();

        public void handleHotkey()
        {
            launcherWindow.Show();
            launcherWindow.BringToFront();
            Close(currentApplication);
        }

        public List<LaunchableApplication> getApplications()
        {
            return applications;
        }

        public void Launch(LaunchableApplication application)
        {
            if (application == null) return;
            launcherWindow.Hide();
            if (currentApplication != null && currentApplication != application)
                currentApplication.Close();
            application.Launch();
            currentApplication = application;
        }

        public void Close(LaunchableApplication application)
        {
            if (application == null) return;
            if (currentApplication == application)
                currentApplication = null;
            application.Close();
        }

        public void CloseLauncher()
        {
            // Shutdown all applications.
            foreach(LaunchableApplication currentApplication in applications)
            {
                currentApplication.ForceClose();
            }
        }
    }
}
