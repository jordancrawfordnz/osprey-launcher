using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OspreyLauncher
{
    public interface ProcessClosureNotifiable
    {
        void notifyProcessClosure(ApplicationInstance instanceClosed);
    }
}
