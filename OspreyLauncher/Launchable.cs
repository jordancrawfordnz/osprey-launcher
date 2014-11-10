using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OspreyLauncher
{
    public interface Launchable : Selectable
    {
        void Close();
        void ForceClose();
        void Launch();
    }
}
