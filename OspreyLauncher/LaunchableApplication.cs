using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace OspreyLauncher
{
    public class LaunchableApplication : Launchable
    {
        string name, path;
        bool suspendable, keepOpen;
        ApplicationInstance instance;
        List<Launchable> restricted;
        
        public LaunchableApplication(string name, string path, bool suspendable = true, bool keepOpen = false)
        {
            this.name = name;
            this.path = path;
            this.suspendable = suspendable;
            this.instance = new ClosedApplication(this);
            this.keepOpen = keepOpen;
            restricted = new List<Launchable>();
        }

        public string getPath()
        {
            return path;
        }

        public bool isSuspendable()
        {
            return suspendable;
        }

        public bool shouldKeepOpen()
        {
            return keepOpen;
        }

        public string getName()
        {
            return name;
        }

        public Type getStatus()
        {
            return instance.GetType();
        }

        public void changeInstance(ApplicationInstance instance)
        {
            this.instance = instance;
        }

        public void Launch()
        {
            instance.Launch();
        }

        public void ForceClose()
        {
            instance.ForceClose();
        }

        public void Close()
        {
            instance.Close();
        }

        public void Select()
        {
            LauncherController.GetInstance().Launch(this);
        }

        public List<Launchable> GetRestrictedLaunchables()
        {
            return restricted;
        }

        public void AddRestrictedLaunchable(Launchable toAdd)
        {
            restricted.Add(toAdd);
        }

        public void Show()
        {
            instance.Show();
        }

        public void Hide()
        {
            instance.Hide();
        }
    }
}
