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
            LauncherController.GetInstance().getSelectableItems()[0].Select();
        }

        public void Exit()
        {
            LauncherController.GetInstance().CloseLauncher();
        }

    }
}
        /*
         *  private void updateSelectedIcon(int newSelected)
        {
            if (newSelected == selectedIcon) return;
            if (getIcon(newSelected) == null) throw new Exception("Invalid icon.");

            appearSelected(getIcon(newSelected));
            appearNormal(getIcon(selectedIcon));
            selectedIcon = newSelected;
        }
         * 
         * 
         * private OvalShape getIcon(int selectedIndex)
        {
            return null;
        }

        private void appearSelected(OvalShape toSelect)
        {
            toSelect.BorderColor = Color.DeepSkyBlue;
        }

        private void appearNormal(OvalShape toNormal)
        {
            toNormal.BorderColor = Color.White;
        }
         * 
         * 
        int selectedIcon = 1;
         * 
         * 
         * 
         *         private void select()
        {
            LauncherController.GetInstance().getSelectableItems()[selectedIcon - 1].Select();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left)
            {
                if (getIcon(selectedIcon - 1) != null) updateSelectedIcon(selectedIcon - 1);
                return true;
            }
            else if (keyData == Keys.Right)
            {
                if (getIcon(selectedIcon + 1) != null) updateSelectedIcon(selectedIcon + 1);
                return true;
            }
            else if (keyData == Keys.Enter)
            {
                select();
                return true;
            }
            // Call the base class
            return base.ProcessCmdKey(ref msg, keyData);
         * 
         * 
        }*/

