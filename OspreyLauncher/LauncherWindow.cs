﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;
using System.Threading;
using CefSharp.WinForms;
using CefSharp;

namespace OspreyLauncher
{
    public partial class LauncherWindow : Form
    {
        private readonly ChromiumWebBrowser browser;

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

            browser = new ChromiumWebBrowser("http://192.168.1.106:9000/");
            browser.Dock = DockStyle.Fill;

            browser.RegisterJsObject("backend", FrontendBridge.GetInstance());
            Cursor.Hide();
            browser.FrameLoadEnd += new EventHandler<FrameLoadEndEventArgs>(browser_FrameLoadEnd);
            browser.GotFocus += new EventHandler(browser_GotFocus);
            this.Controls.Add(browser);
        }

        // stop the browser from getting focus
        void browser_GotFocus(object sender, EventArgs e)
        {
            this.Focus();
        }

        void browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    loadingLabel.Hide();
                    this.Focus(); 
                    
                }); // invokes this method in the UI thread
                return;
            } 
        }

        public void PrepareForClose()
        {
            // this could be run from another thread!
            // http://stackoverflow.com/questions/13698704/execute-a-method-in-main-thread-from-event-handler
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { PrepareForClose(); }); // invokes this method in the UI thread
                return;
            }
            fadeOut();
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Send keys through to JS
            if (keyData == Keys.Left)
            {
                browser.ExecuteScriptAsync("moveLeft()");
                return true;
            }
            else if (keyData == Keys.Right)
            {
                browser.ExecuteScriptAsync("moveRight()");
                return true;
            }
            else if (keyData == Keys.Up)
            {
                browser.ExecuteScriptAsync("moveUp()");
                return true;
            }
            else if (keyData == Keys.Down)
            {
                browser.ExecuteScriptAsync("moveDown()");
                return true;
            }
            else if (keyData == Keys.Enter)
            {
                browser.ExecuteScriptAsync("selectKey()");
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
