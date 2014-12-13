using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;

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
                this.TopMost = true;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LauncherController.GetInstance().Launch(LauncherController.GetInstance().getApplications()[0]);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
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

        public void makePresent()
        {
            // this could be run from another thread!
            // http://stackoverflow.com/questions/13698704/execute-a-method-in-main-thread-from-event-handler
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { makePresent(); }); // invokes this method in the UI thread
                return;
            }
            // if in the main (UI) thread
            this.Show();
            this.BringToFront();
            this.Focus();
        }
    }
}
