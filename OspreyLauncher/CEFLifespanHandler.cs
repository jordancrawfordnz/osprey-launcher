using System;
using CefSharp;
using CefSharp.WinForms;
using CefSharp.Internals;

namespace OspreyLauncher
{
    public class CEFLifespanHandler : ILifeSpanHandler
    {
        public CEFLifespanHandler()
        {

        }

        public bool DoClose(IWebBrowser browserControl, IBrowser browser)
        {
            return true;
        }

        public void OnAfterCreated(IWebBrowser browserControl, IBrowser browser)
        {
        }

        public void OnBeforeClose(IWebBrowser browserControl, IBrowser browser)
        {
        }

        bool ILifeSpanHandler.OnBeforePopup(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IWindowInfo windowInfo, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            newBrowser = browserControl;
            return true;
        }

        // Close popups automatically.
        bool OnBeforePopup(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IWindowInfo windowInfo, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            // Preserve new windows to be opened and load all popup urls in the same browser view
            browserControl.Load(targetUrl);

            newBrowser = browserControl;

            return true;
        }
    }
}
