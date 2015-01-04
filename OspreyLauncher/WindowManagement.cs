using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualBasic;
namespace OspreyLauncher
{
    public class WindowManagement
    {
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;

        // From: http://stackoverflow.com/questions/18364504/c-sharp-switching-windows-in-net
        // and: http://www.pinvoke.net/default.aspx/user32/SwitchToThisWindow.html
        [DllImport("user32.dll", SetLastError = true)]
        static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        static Process currentProcess = Process.GetCurrentProcess();

        public static void SwitchToApplication(Process toSwitchTo)
        {
            SwitchProcess(toSwitchTo);
        }

        public static void SwitchToLauncher()
        {
            FrontendBridge.GetInstance().Reset();
            SwitchProcess(Process.GetCurrentProcess());
            Taskbar.Hide();
        }

        public static void HideProcess(Process toHide)
        {
            if (toHide.MainWindowHandle != IntPtr.Zero)
            {
                ShowWindow(toHide.MainWindowHandle, 0);
            }
        }

        public static void ShowProcess(Process toShow)
        {
            if (toShow.MainWindowHandle != IntPtr.Zero)
            {
                ShowWindow(toShow.MainWindowHandle, 1);
            }
        }

        private static void SwitchProcess(Process toSwitchTo)
        {
            if (currentProcess == toSwitchTo)
                return;

            if (toSwitchTo.MainWindowHandle != IntPtr.Zero)
            {
                SwitchToThisWindow(toSwitchTo.MainWindowHandle,true);
            }
            currentProcess = toSwitchTo;
        }
    }
}
