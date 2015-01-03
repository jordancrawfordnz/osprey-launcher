using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace OspreyLauncher
{
    public class BackgroundApplication : ApplicationInstance
    {
        public BackgroundApplication(LaunchableApplication application, Process process)
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
