using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace OspreyLauncher
{
    public class Suspend
    {

        // calls: http://technet.microsoft.com/en-gb/sysinternals/bb896649.aspx to close

        private static ProcessStartInfo getStartInfo()
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.FileName = "C:\\pstools\\pssuspend.exe";
            processInfo.WindowStyle = ProcessWindowStyle.Hidden;
            return processInfo;
        }

        public static void DoSuspend(Process toSuspend)
        {
            ProcessStartInfo processInfo = getStartInfo();
            processInfo.Arguments = toSuspend.Id.ToString();
            Process.Start(processInfo);
        }

        public static void DoResume(Process toResume)
        {
            ProcessStartInfo processInfo = getStartInfo();
            processInfo.Arguments = "-r " + toResume.Id.ToString();
            Process.Start(processInfo);
            
        }
    }
}
