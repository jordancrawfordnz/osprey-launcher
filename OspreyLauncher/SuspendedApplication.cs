using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

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
            ToolLauncher.DoResume(process);    // resume process
            WindowManagement.SwitchToApplication(process);
            application.changeInstance(new OpenApplication(application, process));
        }

        public override void Close()
        {
        }

        public override void ForceClose()
        {
            process.Kill();
            Thread.Sleep(2000);
            application.changeInstance(new ClosedApplication(application));
        }
    }
}
