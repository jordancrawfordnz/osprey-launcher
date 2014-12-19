using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace OspreyLauncher
{
    public class ClosedApplication : ApplicationInstance
    {
        public ClosedApplication(LaunchableApplication application) : base(application)
        {
        }

        public override void Launch()
        {
            Process process = Process.Start(application.getPath());
            WindowManagement.SwitchProcess(process);
            application.changeInstance(new OpenApplication(application, process));
        }

        public override void Close()
        {
        }

        public override void ForceClose()
        {
        }
    }
}
