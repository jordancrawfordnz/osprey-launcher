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

        private LauncherController()
        {
            launcherWindow = LauncherWindow.GetInstance();
            LaunchListener.EnableListeners();
        }

        public static LauncherController GetInstance()
        {
            if(instance == null)
                instance = new LauncherController();
            return instance;
        }
    
        // === Manage launchables / selectables ===
        Launchable currentLaunchable = null;

        Dictionary<string, Selectable> selectables = new Dictionary<string, Selectable>();

        public void AddSelectable(string key, Selectable selectableToAdd)
        {
            if (selectables.ContainsKey(key)) return; // ignore it if its already in therea
            selectables.Add(key, selectableToAdd);
        }

        public Selectable GetSelectable(string keyToGet)
        {
            if (selectables.ContainsKey(keyToGet))
                return selectables[keyToGet];
            return null;
        }

        public Launchable GetLaunchable(string keyToGet)
        {
            Selectable selectableResult = GetSelectable(keyToGet);
            if (selectableResult != null && selectableResult is Launchable)
                return (Launchable)selectableResult;
            return null;
        }

        public List<Launchable> GetLaunchables()
        {
            List<Launchable> toReturn = new List<Launchable>();
            foreach (Selectable currentSelectable in GetSelectables())
            {
                if(currentSelectable is Launchable)
                    toReturn.Add((Launchable)currentSelectable);
            }
            return toReturn;
        }

        public List<Selectable> GetSelectables()
        {
            return selectables.Values.ToList<Selectable>();
        }

        // === Launchables ===
                
        public void Launch(Launchable application)
        {
            if (application == null) return;
            if (currentLaunchable != null && currentLaunchable != application)
                currentLaunchable.Close();
            foreach (Launchable currentRestricted in application.GetRestrictedLaunchables())
            {
                currentRestricted.ForceClose();
            }
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

        // === General lifecycle things ===

        public void HandleLaunch() // when a launch listener fires
        {
            if(currentLaunchable == null)
                FrontendBridge.GetInstance().Reset(); // if still in launcher and want to go back
            else
                Close(currentLaunchable);
        }

        public void CloseLauncher()
        {
            CloseAll(); // close any running apps
            Taskbar.Show();
            LauncherWindow.GetInstance().PrepareForClose();
        }

        public void CloseAll()
        {
            // Shutdown all applications.
            foreach(Launchable currentLaunchable in GetLaunchables())
            {
                currentLaunchable.ForceClose();
            }
        }

        public void notifyProcessClosure(LaunchableApplication applicationClosed)
        {
            HandleClose(applicationClosed);
        }
    }
}