using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OspreyLauncher
{
    public class FrontendBridge
    {
        static FrontendBridge instance = null;

        private FrontendBridge()
        {}

        public static FrontendBridge GetInstance()
        {
            if(instance == null)
            {
                instance = new FrontendBridge();
            }
            return instance;
        }

        public void SelectItem(string toSelect)
        {
            Selectable selectable = LauncherController.GetInstance().GetSelectable(toSelect);
            if (selectable != null)
                selectable.Select();
        }

        public void AddApplication(string name, string path, bool suspendable = true, bool keepOpen = false)
        {
            LauncherController.GetInstance().AddSelectable(name,new LaunchableApplication(name, path, suspendable, keepOpen));
        }

        public void AddDesktopLaunchable(string name)
        {
            LauncherController.GetInstance().AddSelectable(name, DesktopLaunchable.GetInstance());
        }

        public void AddExitLaunchable(string name)
        {
            LauncherController.GetInstance().AddSelectable(name, ExitSelectable.GetInstance());
        }              
    }
}