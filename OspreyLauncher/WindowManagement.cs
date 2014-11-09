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

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool BringWindowToTop(HandleRef hWnd);


        // With help from:
        // http://msdn.microsoft.com/en-us/library/aa288468%28v=vs.71%29.aspx#pinvoke_callingdllexport
        // http://stackoverflow.com/questions/2647820/toggle-process-startinfo-windowstyle-processwindowstyle-hidden-at-runtime

        public static void HideMainWindow(Process toHide)
        {
            if (toHide.MainWindowHandle != IntPtr.Zero)
            {
                ShowWindow(toHide.MainWindowHandle, 0);
            }
        }

        public static void ShowMainWindow(Process toHide)
        {
            if (toHide.MainWindowHandle != IntPtr.Zero)
            {
                ShowWindow(toHide.MainWindowHandle, 1);
            }
        }

        public static void BringWindowToTop(Process toBringToTop)
        {
            if (toBringToTop.MainWindowHandle != IntPtr.Zero)
            {
                BringWindowToTop(toBringToTop.MainWindowHandle);
            }
        }
    }
}
