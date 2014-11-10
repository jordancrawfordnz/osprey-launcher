using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace OspreyLauncher
{
    public class LauncherController : ProcessClosureNotifiable
    {
        static LauncherController instance = null;
        LauncherWindow launcherWindow;
        Launchable currentLaunchable = null;
        List<Launchable> launchables = new List<Launchable>();

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
            setupLaunchables();
        }


        void setupLaunchables()
        {
            launchables.Add(new LaunchableApplication("MediaPortal",
    "C:\\Program Files (x86)\\Team MediaPortal\\MediaPortal\\MediaPortal.exe"));
            launchables.Add(new LaunchableApplication("XBMC",
    "C:\\Program Files (x86)\\XBMC\\XBMC.exe",true, true));
            launchables.Add(DesktopLaunchable.GetInstance());


        }


        public void handleHotkey()
        {
            Close(currentLaunchable);
        }

        public List<Selectable> getSelectableItems()
        {
            List<Selectable> toReturn = new List<Selectable>();
            toReturn.InsertRange(0, getApplications());
            toReturn.Add(ExitSelectable.GetInstance());
            return toReturn;
        }


        public List<Launchable> getApplications()
        {
            return launchables;
        }

        public void Launch(Launchable application)
        {
            if (application == null) return;
            launcherWindow.Hide();
            if (currentLaunchable != null && currentLaunchable != application)
                currentLaunchable.Close();
            application.Launch();
            currentLaunchable = application;
        }

        public void Close(Launchable application)
        {
            if (application == null) return;
            application.Close();
            if (currentLaunchable == application)
            {
                currentLaunchable = null;
                launcherWindow.makePresent();
                WindowManagement.BringWindowToTop(Process.GetCurrentProcess());
            }
        }

        public void CloseAll()
        {
            // Shutdown all applications.
            foreach(Launchable currentApplication in launchables)
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
