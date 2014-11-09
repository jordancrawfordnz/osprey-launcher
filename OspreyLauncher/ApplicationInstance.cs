﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace OspreyLauncher
{
    public abstract class ApplicationInstance
    {
        public abstract void Launch();
        public abstract void Close();
        public abstract void ForceClose();

        protected LaunchableApplication application;

        protected ApplicationInstance(LaunchableApplication application)
        {
            this.application = application;   
        }
    }
}
