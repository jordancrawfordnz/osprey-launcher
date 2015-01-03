using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace OspreyLauncher
{
    public class DesktopLaunchable : Launchable
    {
        bool isHidden = false;

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
            if (isHidden)
            {
                foreach (Launchable currentLaunchable in LauncherController.GetInstance().GetLaunchables())
                {
                    if (currentLaunchable != this) currentLaunchable.Show();
                }
                LauncherWindow.GetInstance().showWindow();
                Taskbar.Hide();
                isHidden = false;
            }
        }

        public void ForceClose()
        { }

        public void Launch()
        {
            if (!isHidden)
            {
                foreach (Launchable currentLaunchable in LauncherController.GetInstance().GetLaunchables())
                {
                    if (currentLaunchable != this) currentLaunchable.Hide();
                }
                LauncherWindow.GetInstance().hideWindow();
                Taskbar.Show();
                isHidden = true;
            }
        }

        public void Select()
        {
            LauncherController.GetInstance().Launch(this);
        }

        public List<Launchable> GetRestrictedLaunchables()
        {
            return new List<Launchable>();
        }

        public void AddRestrictedLaunchable(Launchable toAdd)
        {
            return; // can't have any restricted launchables!
        }

        public void Show()
        {}

        public void Hide()
        {}
    }
}
