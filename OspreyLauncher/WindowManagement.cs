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

        static Process currentProcess = Process.GetCurrentProcess();

        public static void SwitchToApplication(Process toSwitchTo)
        {
            SwitchProcess(toSwitchTo);
        }

        public static void SwitchToLauncher()
        {
            LauncherWindow.GetInstance().prepareToBeShown();
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
            try
            {
                Interaction.AppActivate(toSwitchTo.Id);
            }
            finally{}
            currentProcess = toSwitchTo;
        }
    }
}
