using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OspreyLauncher
{
    public class LauncherController : ProcessClosureNotifiable
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
            applications.Add(new LaunchableApplication("XBMC",
    "C:\\Program Files (x86)\\XBMC\\XBMC.exe", true));

        }
        List<LaunchableApplication> applications = new List<LaunchableApplication>();

        public void handleHotkey()
        {
            Close(currentApplication);
        }

        public List<Selectable> getSelectableItems()
        {
            List<Selectable> toReturn = new List<Selectable>();
            toReturn.InsertRange(0, getApplications());
            toReturn.Add(ExitSelectable.GetInstance());
            return toReturn;
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
            application.Close();
            if (currentApplication == application)
            {
                currentApplication = null;
                launcherWindow.makePresent();
            }
        }

        public void CloseAll()
        {
            // Shutdown all applications.
            foreach(LaunchableApplication currentApplication in applications)
            {
                currentApplication.ForceClose();
            }
        }

        public void notifyProcessClosure(LaunchableApplication applicationClosed)
        {
            Close(applicationClosed);
        }
    }
}
