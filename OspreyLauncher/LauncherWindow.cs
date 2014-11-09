using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OspreyLauncher
{
    public partial class LauncherWindow : Form
    {
        LaunchableApplication application;

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
    }
}
