using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace OspreyLauncher
{
    public class ToolLauncher
    {
        // calls: http://technet.microsoft.com/en-gb/sysinternals/bb896649.aspx to close

        private static ProcessStartInfo getStartInfo()
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.WindowStyle = ProcessWindowStyle.Hidden;
            return processInfo;
        }

        private static ProcessStartInfo getPsSuspendStartInfo()
        {
            ProcessStartInfo processInfo = getStartInfo();
            processInfo.FileName = "pssuspend.exe";
            return processInfo;
        }

        public static void DoSuspend(Process toSuspend)
        {
            ProcessStartInfo processInfo = getPsSuspendStartInfo();
            processInfo.Arguments = toSuspend.Id.ToString();
            Process.Start(processInfo);
        }

        public static void DoResume(Process toResume)
        {
            ProcessStartInfo processInfo = getPsSuspendStartInfo();
            processInfo.Arguments = "-r " + toResume.Id.ToString();
            Process.Start(processInfo);
        }

        public static void SwitchToProcess(Process toSwitchTo)
        {
            ProcessStartInfo processInfo = getStartInfo();
            processInfo.FileName = "SwitchToWindow.exe";
            processInfo.Arguments = toSwitchTo.Id.ToString();
            Process.Start(processInfo);
        }
    }
}
