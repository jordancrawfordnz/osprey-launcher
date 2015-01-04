using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
                new Thread(new ThreadStart(selectable.Select)).Start(); // select the item in a new thread so the view is not frozen.
        }

        public void AddApplication(string name, string path, bool suspendable = true, bool keepOpen = false)
        {
            LauncherController.GetInstance().AddSelectable(name,new LaunchableApplication(name, path, suspendable, keepOpen));
        }

        public void AddRestriction(string appliesTo, string restricted)
        {
            // find the launchable the restriction applies to.
            Launchable launchableAppliesTo = LauncherController.GetInstance().GetLaunchable(appliesTo);
            if (launchableAppliesTo != null)
            {
                // find the restricted one.
                Launchable launchableRestricted = LauncherController.GetInstance().GetLaunchable(restricted);
                if (launchableRestricted != null)
                {
                    // add it.
                    launchableAppliesTo.AddRestrictedLaunchable(launchableRestricted);
                }
            }
        }

        public void AddDesktopLaunchable(string name)
        {
            LauncherController.GetInstance().AddSelectable(name, DesktopLaunchable.GetInstance());
        }

        public void AddExitLaunchable(string name)
        {
            LauncherController.GetInstance().AddSelectable(name, ExitSelectable.GetInstance());
        }

        public void Reset()
        {
            LauncherWindow.GetInstance().getBrowser().ExecuteScriptAsync("frontend.reset()");
            Thread.Sleep(100); // minor delay to allow the frontend to reset.
        }

        public void MoveLeft()
        {
            LauncherWindow.GetInstance().getBrowser().ExecuteScriptAsync("frontend.moveLeft()");
        }

        public void MoveRight()
        {
            LauncherWindow.GetInstance().getBrowser().ExecuteScriptAsync("frontend.moveRight()");
        }

        public void MoveUp()
        {
            LauncherWindow.GetInstance().getBrowser().ExecuteScriptAsync("frontend.moveUp()");
        }

        public void MoveDown()
        {
            LauncherWindow.GetInstance().getBrowser().ExecuteScriptAsync("frontend.moveDown()");
        }

        public void SelectKey()
        {
            LauncherWindow.GetInstance().getBrowser().ExecuteScriptAsync("frontend.selectKey()");
        }
    }
}