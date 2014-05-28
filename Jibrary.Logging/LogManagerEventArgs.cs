using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jibrary.Logging
{
    public delegate void LogManagerEventHandler(object sender, LogManagerEventArgs e);
    public class LogManagerEventArgs : EventArgs
    {
        public Log Log
        {
            get;
            set;
        }
    }
}
