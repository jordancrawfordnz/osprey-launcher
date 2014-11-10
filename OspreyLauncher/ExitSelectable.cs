using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OspreyLauncher
{
    public class ExitSelectable : Selectable
    {
        private ExitSelectable()
        {
        }

        public void Select()
        {
            LauncherWindow.GetInstance().Close();
        }


        private static ExitSelectable instance = null;
        public static ExitSelectable GetInstance()
        {
            if (instance == null)
                instance = new ExitSelectable();
            return instance;
        }
    }
}
