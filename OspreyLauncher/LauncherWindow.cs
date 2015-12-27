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
        private readonly ChromiumWebBrowser frontendBrowser;

        static LauncherWindow instance = null;

        public static LauncherWindow GetInstance()
        {
            if (instance == null)
                instance = new LauncherWindow();
            return instance;
        }

        private LauncherWindow()
        {
            if (!UserSettings.GetInstance().isInDebugMode())
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                changeCursor(false);
                Taskbar.Hide();
            }
            InitializeComponent();
            frontendBrowser = showBrowser(UserSettings.GetInstance().getFrontendUrl());
            frontendBrowser.RegisterJsObject("backend", FrontendBridge.GetInstance());

            // the label seems to stretch, so must be hidden before anything can be seen.

            // running hide seems to stop something loading properly. Will have to see how this actually works.
            // maybe make a switch method to switch between two browser instances

        }

        List<ChromiumWebBrowser> browsers = new List<ChromiumWebBrowser>();
        ChromiumWebBrowser currentlyShownBrowser = null;

        public void changeCursor(bool enabled)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    changeCursor(enabled);
                });
                return;
            }
            if (enabled)
                Cursor.Show();
            else
                Cursor.Hide();
        }

        public ChromiumWebBrowser showBrowser(string url, ChromiumWebBrowser pendingInit = null)
        {
            ChromiumWebBrowser browser;
            if (pendingInit == null)
                browser = new ChromiumWebBrowser(url);
            else
                browser = pendingInit;

            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    showBrowser(url,browser);
                });
                return browser;
            }
            browser.BrowserSettings = new BrowserSettings();

            browser.Dock = DockStyle.Fill;
            loadingLabel.Show();
            this.Controls.Add(browser);
            browser.FrameLoadEnd += new EventHandler<FrameLoadEndEventArgs>(browser_FrameLoadEnd);
            if (currentlyShownBrowser != null)
            {
                currentlyShownBrowser.Hide();
                currentlyShownBrowser.SetFocus(false);
            }
            currentlyShownBrowser = browser;
            browsers.Add(browser);
            return browser;
        }

        public void showBrowser(ChromiumWebBrowser browser)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    showBrowser(browser);
                });
                return;
            }
            if (!browsers.Contains(browser)) return; // browser must be setup properly.
            if (currentlyShownBrowser == browser) return;
            if (currentlyShownBrowser != null)
            {
                currentlyShownBrowser.Hide();
            }
            browser.Show();
            browser.SetFocus(true);
            currentlyShownBrowser = browser;
        }
                
        public void killBrowser(ChromiumWebBrowser browser)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    killBrowser(browser);
                });
                return;
            }
            if (!browsers.Contains(browser)) return; // browser must be setup properly.
            this.Controls.Remove(browser);
            browser.SetFocus(false);
            browsers.Remove(browser);
            if (browser == currentlyShownBrowser)
            {
                currentlyShownBrowser = null;
            }
        }

        public ChromiumWebBrowser getFrontendBrowser()
        {
            return frontendBrowser;
        }

        void browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    loadingLabel.Hide();
                    WindowManagement.SwitchToLauncher();
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
            else if (keyData == Keys.Back || keyData == Keys.Escape)
            {
                FrontendBridge.GetInstance().Reset();
            }
            else if (keyData == Keys.C)
            {
                FrontendBridge.GetInstance().Context();
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
    }
}
