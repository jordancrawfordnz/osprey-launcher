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
        LaunchableApplication application;
        int selectedIcon = 1;

        static LauncherWindow instance = null;
        public static LauncherWindow GetInstance()
        {
            if(instance == null)
                instance = new LauncherWindow();
            return instance;
        }

        private LauncherWindow()
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
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


        private void updateSelectedIcon(int newSelected)
        {
            if (newSelected == selectedIcon) return;
            if (getIcon(newSelected) == null) throw new Exception("Invalid icon.");
            
            appearSelected(getIcon(newSelected));
            appearNormal(getIcon(selectedIcon));
            selectedIcon = newSelected;
        }

        private void LauncherWindow_KeyUp(object sender, KeyEventArgs e)
        {
            MessageBox.Show("KEYPRESS");
            if (e.KeyCode == Keys.Left)
            {
                if (getIcon(selectedIcon - 1) != null) updateSelectedIcon(selectedIcon - 1);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (getIcon(selectedIcon + 1) != null) updateSelectedIcon(selectedIcon + 1);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
            }
            else e.Handled = false;
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
        }
    }
}
