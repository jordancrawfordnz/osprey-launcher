using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OspreyLauncher
{
    public class DesktopLaunchable : Launchable
    {
        private DesktopLaunchable()
        {
        }

        private static DesktopLaunchable instance = null;
        public static DesktopLaunchable GetInstance()
        {
            if (instance == null)
                instance = new DesktopLaunchable();
            return instance;
        }

        public void Close()
        {
        }

        public void ForceClose()
        {
        }

        public void Launch()
        {
        }

        public void Select()
        {
            LauncherController.GetInstance().Launch(this);
        }
    }
}
