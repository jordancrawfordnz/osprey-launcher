using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace OspreyLauncher
{
    public class ProcessCloseWatcher
    {
        Process toWatch;
        List<ProcessClosureNotifiable> toNotify;
        LaunchableApplication application;

        static Dictionary<int,ProcessCloseWatcher> watchers = new Dictionary<int,ProcessCloseWatcher>();

        public static ProcessCloseWatcher GetWatcher(LaunchableApplication instance, Process toWatch)
        {
           ProcessCloseWatcher toReturn;
            if (!watchers.TryGetValue(toWatch.Id,out toReturn))
            {
                toReturn = new ProcessCloseWatcher(instance,toWatch);
                watchers.Add(toWatch.Id,toReturn);
            }
            return toReturn;
        }

        private ProcessCloseWatcher(LaunchableApplication instance, Process toWatch)
        {
            this.toWatch = toWatch;
            this.application = instance;
            toNotify = new List<ProcessClosureNotifiable>();
        }

        public void addWatcher(ProcessClosureNotifiable newToNotify)
        {
            if (!toNotify.Contains(newToNotify))
                toNotify.Add(newToNotify);
        }

        public void waitForExit()
        {
            toWatch.WaitForExit();
            foreach (ProcessClosureNotifiable notify in toNotify)
            {
                notify.notifyProcessClosure(application);
            }
        }


    }
}
