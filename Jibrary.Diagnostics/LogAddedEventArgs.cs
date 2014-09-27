using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jibrary.Diagnostics
{
    public class LogAddedEventArgs : EventArgs
    {
        public Log Log
        {
            get;
            set;
        }
    }
}
