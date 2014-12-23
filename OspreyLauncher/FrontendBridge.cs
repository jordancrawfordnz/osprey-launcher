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

        /*
         * 
         *        void setupLaunchables()
        {
            // Kitchen PC uses 32-bit versions of everythings
            // Currently testing: Hiding only

          // launchables.Add(new LaunchableApplication("MediaPortal", "C:\\Program Files\\Team MediaPortal\\MediaPortal\\MediaPortal.exe", true, false));
           launchables.Add(new LaunchableApplication("MediaPortal","C:\\Program Files (x86)\\Team MediaPortal\\MediaPortal\\MediaPortal.exe",false, true));
           launchables.Add(new LaunchableApplication("XBMC", "C:\\Program Files (x86)\\XBMC\\XBMC.exe",false,true));
          // launchables.Add(new LaunchableApplication("XBMC", "C:\\Program Files\\XBMC\\XBMC.exe", true, false));
         
            launchables.Add(DesktopLaunchable.GetInstance()); // the launchable to return to the desktop
        }
         * 
         * */



        /*
                public List<Selectable> getSelectableItems()
                {
                    List<Selectable> toReturn = new List<Selectable>();
                    foreach (Selectable currentSelectable in getLaunchables())
                    {
                        toReturn.Add(currentSelectable);
                    }
                    toReturn.Add(ExitSelectable.GetInstance());
                    return toReturn;
                }
               */

        /*
        public List<Launchable> getLaunchables()
        {
            return selectables;
        }
        */
              
    }
}