using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OspreyLauncher
{
    public class LaunchListener
    {
        
        private static LaunchListener instance = null;

        public static void EnableListeners()
        {
            if(instance == null)
            {
                instance = new LaunchListener();
            }
        }
       
        private LaunchListener()
        {
            new UDPTrigger().Launch += new UDPTrigger.LaunchHandler(HandleLaunch);
            new HotkeyTrigger().Launch += new HotkeyTrigger.LaunchHandler(HandleLaunch);
        }
        
        private void HandleLaunch()
        {
            LauncherController.GetInstance().handleLaunch();
        }
    }
}
