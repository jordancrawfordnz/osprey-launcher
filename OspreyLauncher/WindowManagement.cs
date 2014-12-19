using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace OspreyLauncher
{
    public class WindowManagement
    {
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        // BringWindowToTop signature from: http://www.pinvoke.net/default.aspx/user32.BringWindowToTop

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        [DllImport("user32.dll")]
        static extern IntPtr SetFocus(IntPtr hWnd);


        // With help from:
        // http://msdn.microsoft.com/en-us/library/aa288468%28v=vs.71%29.aspx#pinvoke_callingdllexport
        // http://stackoverflow.com/questions/2647820/toggle-process-startinfo-windowstyle-processwindowstyle-hidden-at-runtime

        static Process currentProcess;

        public static void SwitchProcess(Process toSwitchTo)
        {
            // potentially not working
            currentProcess = toSwitchTo;
            LauncherWindow.GetInstance().hideWindow();
            ShowMainWindow(toSwitchTo);
            BringWindowToTop(toSwitchTo);
        }

        public static void SwitchToLauncher()
        {
            HideMainWindow(currentProcess);
            LauncherWindow.GetInstance().showWindow();
            //SwitchProcess(Process.GetCurrentProcess());
        }
        
        private static void HideMainWindow(Process toHide)
        {
            if (toHide.MainWindowHandle != IntPtr.Zero)
            {
                ShowWindow(toHide.MainWindowHandle, 0);
             //   EnableWindow(toHide.MainWindowHandle, false); potentially causing issue
            }
        }

        private static void ShowMainWindow(Process toShow)
        {
            if (toShow.MainWindowHandle != IntPtr.Zero)
            {
                ShowWindow(toShow.MainWindowHandle, 1);
                EnableWindow(toShow.MainWindowHandle, true);
            }
        }

        private static void BringWindowToTop(Process toBringToTop)
        {
            if (toBringToTop.MainWindowHandle != IntPtr.Zero)
            {
                SetForegroundWindow(toBringToTop.MainWindowHandle);
                SetFocus(toBringToTop.MainWindowHandle);
            }
        }
    }
}
