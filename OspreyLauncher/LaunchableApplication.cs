using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace OspreyLauncher
{
    public class LaunchableApplication
    {
        string name, path;
        bool suspendable;
        ApplicationInstance instance;
        
        public LaunchableApplication(string name, string path, bool suspendable = true)
        {
            this.name = name;
            this.path = path;
            this.suspendable = suspendable;
            this.instance = new ClosedApplication(this);
        }

        public string getPath()
        {
            return path;
        }

        public bool isSuspendable()
        {
            return suspendable;
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
    }
}
