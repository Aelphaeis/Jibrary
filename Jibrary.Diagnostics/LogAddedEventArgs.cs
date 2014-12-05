using System;

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
