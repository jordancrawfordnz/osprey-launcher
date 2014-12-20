using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace OspreyLauncher
{
    public class HiddenApplication : ApplicationInstance
    {
        public HiddenApplication(LaunchableApplication application, Process process)
            : base(application)
        {
            this.process = process;
        }

        Process process;
        
        public override void Launch()
        {
            WindowManagement.SwitchToApplication(process);
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
