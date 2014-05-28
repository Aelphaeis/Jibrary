using System;
namespace Jibrary.Logging
{
    public class Log 
    {
        public DateTime Date
        {
            get;
            set;
        }

        public virtual String Entry
        {
            get;
            set;
        }

        public virtual Boolean IsError
        {
            get;
            set;
        }

        public virtual LogPriority Priority
        {
            get;
            set;
        }

        public Log()
        {
            Date = DateTime.Now;
            Entry = String.Empty;
            Priority = LogPriority.Normal;
        }

        public Log(String Entry) 
            : this()
        {
            this.Entry = Entry;
        }

        public Log(String Entry, LogPriority Priority) 
            : this(Entry)
        {
            this.Priority = Priority;
        }

        public Log(String Entry, LogPriority Priority, Boolean IsError)
            : this(Entry, Priority)
        {
            this.IsError = IsError;
        }

        public Log(String Entry, LogPriority Priority, Boolean IsError, DateTime LogTime)
            : this(Entry, Priority, IsError)
        {
            this.Date = LogTime;
        }

        public Log(Exception e, LogPriority priority = LogPriority.Highest) 
            : this(e.ToString(), priority, true, DateTime.Now)
        {
        }

    }
}
