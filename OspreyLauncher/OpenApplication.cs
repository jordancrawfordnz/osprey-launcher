using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace OspreyLauncher
{
    public class OpenApplication : ApplicationInstance, ProcessClosureNotifiable
    {
        Process process;
        ProcessCloseWatcher watcher;

        public OpenApplication(LaunchableApplication application, Process process) : base(application)
        {
            this.process = process;
            watcher = ProcessCloseWatcher.GetWatcher(this, process);
            watcher.addWatcher(this);
            
            Thread watcherThread = new Thread(new ThreadStart(watcher.waitForExit));
            watcherThread.Start(); // Start the thread
        }

        public override void Launch()
        {
        }

        void onClose()
        {
            application.changeInstance(new ClosedApplication(application));
        }

        void onSuspend()
        {
            application.changeInstance(new SuspendedApplication(application, process));
        }

        public override void Close()
        {
            if (application.isSuspendable())
            {
                WindowManagement.HideMainWindow(process);
                Suspend.DoSuspend(process); // resume process
                onSuspend();
            }
            else
                ForceClose();
        }

        public override void ForceClose()
        {
            process.Kill();
            process.Close();
            onClose();
        }

        public void notifyProcessClosure(ApplicationInstance instanceClosed)
        {
            onClose();
        }
    }
}
