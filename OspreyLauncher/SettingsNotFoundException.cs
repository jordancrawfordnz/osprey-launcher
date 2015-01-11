using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OspreyLauncher
{
    public class SettingsNotFoundException : Exception
    {
        public SettingsNotFoundException()
        {
        }

        public SettingsNotFoundException(string message)
            : base(message)
        {
        }

        public SettingsNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
