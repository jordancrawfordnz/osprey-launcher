using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace OspreyLauncher
{
    public class SuspendedApplication : ApplicationInstance
    {
        public SuspendedApplication(LaunchableApplication application, Process process)
            : base(application)
        {
            this.process = process;
        }

        Process process;

        public override void Launch()
        {
            Suspend.DoResume(process);    // resume process
            WindowManagement.ShowMainWindow(process);
            WindowManagement.BringWindowToTop(process);
            application.changeInstance(new OpenApplication(application, process));
        }

        public override void Close()
        {
        }

        public override void ForceClose()
        {
            process.Kill();
            process.Close();
        }
    }
}
