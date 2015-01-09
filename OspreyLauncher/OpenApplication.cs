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
        ApplicationAutomaticCloser automaticClose;

        public OpenApplication(LaunchableApplication application, Process process) : base(application)
        {
            this.process = process;
            watcher = ProcessCloseWatcher.GetWatcher(application, process);
            watcher.addWatcher(this);
            watcher.addWatcher(LauncherController.GetInstance());
            
            Thread watcherThread = new Thread(new ThreadStart(watcher.waitForExit));
            watcherThread.Start(); // Start the thread

            automaticClose = new ApplicationAutomaticCloser(application, FrontendBridge.GetInstance().GetAutomaticClosingDelay(),process);
            if (FrontendBridge.GetInstance().AutomaticClosingEnabled())
                automaticClose.Begin();
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

        void onHide()
        {
            application.changeInstance(new BackgroundApplication(application, process));
        }

        public override void Close()
        {
            automaticClose.Cancel();
            if (application.shouldKeepOpen())
            {
                WindowManagement.SwitchToLauncher();
                onHide();
            }
            else
            {
                if (application.isSuspendable())
                {
                    WindowManagement.SwitchToLauncher();
                    ToolLauncher.DoSuspend(process); // suspend process
                    onSuspend();
                }
                else
                    ForceClose();
            }
        }

        public override void ForceClose()
        {
            process.Kill();
            process.Close();
            onClose();
        }

        public void notifyProcessClosure(LaunchableApplication instanceClosed)
        {
            onClose();
        }


        public override void Show()
        {
            WindowManagement.ShowProcess(process);
        }

        public override void Hide()
        {
            WindowManagement.HideProcess(process);
        }
    }
}
