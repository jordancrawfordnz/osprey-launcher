using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CefSharp.WinForms;
using CefSharp;

namespace OspreyLauncher
{
    public class Webpage : Launchable
    {
        string url;
        ChromiumWebBrowser browser;
        bool showCursor;

        public Webpage(string url, bool showCursor = false)
        {
            this.url = url;
            this.showCursor = showCursor;
        }

        public void Close()
        {
            ForceClose();
        }

        public void ForceClose()
        {
            LauncherWindow.GetInstance().killBrowser(browser);
            if (showCursor) LauncherWindow.GetInstance().changeCursor(false);
        }

        public void Select()
        {
            LauncherController.GetInstance().Launch(this);
        }
        
        public void Launch()
        {
            browser = LauncherWindow.GetInstance().showBrowser(url);
            if (showCursor) LauncherWindow.GetInstance().changeCursor(true);
        }

        public void Show()
        { }

        public void Hide()
        { }

        public List<Launchable> GetRestrictedLaunchables()
        {
            return new List<Launchable>(); 
        }

        public void AddRestrictedLaunchable(Launchable newRestricted)
        { }
    }
}
