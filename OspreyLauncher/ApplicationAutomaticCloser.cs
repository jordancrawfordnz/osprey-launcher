using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OspreyLauncher
{
    public class ApplicationAutomaticCloser
    {
        Thread waiterThread;
        LaunchableApplication toClose;
        int minutesToWait;
        bool timeUp = false;

        public ApplicationAutomaticCloser(LaunchableApplication toClose,int minutesToWait)
        {
            this.minutesToWait = minutesToWait;
            this.toClose = toClose;
            this.waiterThread = null;
        }

        public void Begin()
        {
            waiterThread = new Thread(new ThreadStart(WaitUntilTime));
            timeUp = false;
            waiterThread.Start(); // Start the thread
        }

        public void Cancel()
        {
            if(waiterThread != null && !timeUp) // if timer has started and time isn't up.
                waiterThread.Abort();
        }

        private void WaitUntilTime()
        {
            Thread.Sleep(TimeSpan.FromMinutes(minutesToWait));
            timeUp = true;
            LauncherController.GetInstance().Close(toClose);
        }
    }
}
