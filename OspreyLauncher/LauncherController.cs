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
            // Kitchen PC uses 32-bit versions of everythings
            // Currently testing: Hiding only

           //launchables.Add(new LaunchableApplication("MediaPortal", "C:\\Program Files\\Team MediaPortal\\MediaPortal\\MediaPortal.exe", false, true));
           launchables.Add(new LaunchableApplication("MediaPortal","C:\\Program Files (x86)\\Team MediaPortal\\MediaPortal\\MediaPortal.exe",false, true));
           launchables.Add(new LaunchableApplication("XBMC", "C:\\Program Files (x86)\\XBMC\\XBMC.exe",false,true));
       //    launchables.Add(new LaunchableApplication("XBMC", "C:\\Program Files\\XBMC\\XBMC.exe", false, true));
         
            launchables.Add(DesktopLaunchable.GetInstance()); // the launchable to return to the desktop
        }

        public void handleHotkey()
        {
            Close(currentLaunchable);
        }

        public List<Selectable> getSelectableItems()
        {
            List<Selectable> toReturn = new List<Selectable>();
            foreach (Selectable currentSelectable in getApplications())
            {
                toReturn.Add(currentSelectable);
            }
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
            if (currentLaunchable != null && currentLaunchable != application)
                currentLaunchable.Close();
            application.Launch();
            currentLaunchable = application;
        }

        public void Close(Launchable application)
        {
            if (application == null) return;
            application.Close();
            HandleClose(application);
        }

        private void HandleClose(Launchable application)
        {
            if (application == null) return;
            if (currentLaunchable == application)
            {
                currentLaunchable = null;
                WindowManagement.SwitchToLauncher();
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
            HandleClose(applicationClosed);
        }
    }
}
