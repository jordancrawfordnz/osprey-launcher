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

        public Webpage(string url)
        {
            this.url = url;
        }

        public void Close()
        {
            ForceClose();
        }

        public void ForceClose()
        {
            LauncherWindow.GetInstance().killBrowser(browser);
        }

        public void Select()
        {
            LauncherController.GetInstance().Launch(this);
        }
        
        public void Launch()
        {
            browser = LauncherWindow.GetInstance().showBrowser(url);
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
