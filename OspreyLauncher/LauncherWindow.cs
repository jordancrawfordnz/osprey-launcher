using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;
using System.Threading;

namespace OspreyLauncher
{
    public partial class LauncherWindow : Form
    {
        int selectedIcon = 1;
        static LauncherWindow instance = null;
        public static LauncherWindow GetInstance()
        {
            if (instance == null)
                instance = new LauncherWindow();
            return instance;
        }

        private LauncherWindow()
        {
            if (!Program.DebugMode)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }
            InitializeComponent();
        }

        private OvalShape getIcon(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 1:
                    return icon1;
                case 2:
                    return icon2;
                case 3:
                    return icon3;
                case 4:
                    return icon4;
                default:
                    return null;
            }
        }

        private void appearSelected(OvalShape toSelect)
        {
            toSelect.BorderColor = Color.DeepSkyBlue;
        }

        private void appearNormal(OvalShape toNormal)
        {
            toNormal.BorderColor = Color.White;
        }

        private void select()
        {
            LauncherController.GetInstance().getSelectableItems()[selectedIcon - 1].Select();
        }

        private void updateSelectedIcon(int newSelected)
        {
            if (newSelected == selectedIcon) return;
            if (getIcon(newSelected) == null) throw new Exception("Invalid icon.");

            appearSelected(getIcon(newSelected));
            appearNormal(getIcon(selectedIcon));
            selectedIcon = newSelected;
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
        }

        public void displayMessage(string message)
        {
            // this could be run from another thread!
            // http://stackoverflow.com/questions/13698704/execute-a-method-in-main-thread-from-event-handler
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { displayMessage(message); }); // invokes this method in the UI thread
                return;
            }
            // if in the main (UI) thread
            MessageBox.Show(message);
        }

        public void showWindow()
        {
            // this could be run from another thread!
            // http://stackoverflow.com/questions/13698704/execute-a-method-in-main-thread-from-event-handler
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { showWindow(); }); // invokes this method in the UI thread
                return;
            }
            // if in the main (UI) thread
            this.Show();
        }

        public void hideWindow()
        {
            // this could be run from another thread!
            // http://stackoverflow.com/questions/13698704/execute-a-method-in-main-thread-from-event-handler
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { hideWindow(); }); // invokes this method in the UI thread
                return;
            }
            // if in the main (UI) thread
            this.Hide();
        }

        private void LauncherWindow_Load(object sender, EventArgs e)
        {

        }


        public void fadeOut()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { fadeOut(); }); // invokes this method in the UI thread
                return;
            }

            while (this.Opacity > 0)
            {
                this.Opacity -= 0.1;
                Thread.Sleep(10);

            }

        }

        public void fadeIn()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { fadeIn(); }); // invokes this method in the UI thread
                return;
            }

            while (this.Opacity < 1)
            {
                this.Opacity += 0.1;
                Thread.Sleep(10);

            }

        }

        public void prepareFadeIn()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { prepareFadeIn(); }); // invokes this method in the UI thread
                return;
            }

            this.Opacity = 0;
        }

        public void prepareFadeOut()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { prepareFadeOut(); }); // invokes this method in the UI thread
                return;
            }

            this.Opacity = 1;
        }

    }
}
