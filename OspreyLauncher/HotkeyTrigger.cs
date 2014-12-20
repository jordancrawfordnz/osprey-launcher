using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms; // for Keys

namespace OspreyLauncher
{
    public class HotkeyTrigger
    {
        public event LaunchHandler Launch;
        public delegate void LaunchHandler();

        public HotkeyTrigger()
        {
            // Taken from: http://www.codeproject.com/Articles/19004/A-Simple-C-Global-Low-Level-Keyboard-Hook
            globalKeyboardHook gkh = new globalKeyboardHook();
            gkh.HookedKeys.Add(Keys.D9);
            gkh.KeyUp += new KeyEventHandler(HandleHotkey);    
        }

        void HandleHotkey(object sender, KeyEventArgs e)
        {
            if (Launch != null)
                Launch(); 
            e.Handled = true;
        }
    }
}
