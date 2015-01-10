using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
                Taskbar.Hide();
                Cursor.Hide();
            }
            InitializeComponent();
            
            browser = new ChromiumWebBrowser("http://192.168.1.106:9000/");
            browser.BrowserSettings = new BrowserSettings();
            browser.BrowserSettings.WebSecurityDisabled = true; // This is a security flaw! Can be turned off if the: "Access-Control-Allow-Origin: *" header is present.
            browser.Dock = DockStyle.Fill;
           
            browser.RegisterJsObject("backend", FrontendBridge.GetInstance());
            browser.FrameLoadEnd += new EventHandler<FrameLoadEndEventArgs>(browser_FrameLoadEnd);
            this.Controls.Add(browser);
        }

        public ChromiumWebBrowser getBrowser()
        {
            return browser;
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
                FrontendBridge.GetInstance().MoveLeft();
                return true;
            }
            else if (keyData == Keys.Right)
            {
                FrontendBridge.GetInstance().MoveRight();
                return true;
            }
            else if (keyData == Keys.Up)
            {
                FrontendBridge.GetInstance().MoveUp();
                return true;
            }
            else if (keyData == Keys.Down)
            {
                FrontendBridge.GetInstance().MoveDown();
                return true;
            }
            else if (keyData == Keys.Enter)
            {
                FrontendBridge.GetInstance().SelectKey();
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

        // == Currently Unused Fade Methods ===
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
