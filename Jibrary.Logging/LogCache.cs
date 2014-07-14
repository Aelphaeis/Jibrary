using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jibrary.Logging
{
    public class LogCache : IDisposable
    {
        bool IsDisposed;
        LogManager logManager;
        SortedList<DateTime, Log> logs = new SortedList<DateTime, Log>();

        internal LogCache(LogManager manager)
        {
            logManager = manager;
            IsDisposed = false;
        }

        private void Add(Log Log, DateTime Key)
        {
            try 
            { 
                logs.Add(Key, Log);
            }
            catch
            {
                Add(Log, Key.Add(TimeSpan.FromTicks(1L)));
            }
        }

        public void Add(Log Log)
        {
            ValidateState();

            if ((Log.Entry ?? String.Empty) == String.Empty)
                throw new ArgumentException("Log has no entry. All Logs should have entries");

            try
            {
                logs.Add(Log.Date, Log);
            }
            catch (ArgumentException)
            {
                Add(Log, Log.Date.Add(TimeSpan.FromTicks(1L)));
            }
        }
        public void Add(String Entry)
        {
            ValidateState();
            Add(new Log(Entry));
        }
        public void Add(String Entry, LogPriority Priority)
        {
            ValidateState();
            Add(new Log(Entry, Priority));
        }
        public void Add(String Entry, LogPriority Priority, bool IsError)
        {
            ValidateState();
            Add(new Log(Entry, Priority, IsError));

        }
        public void Add(String Entry, LogPriority Priority, bool IsError, DateTime LogTime)
        {
            ValidateState();
            Add(new Log(Entry, Priority, IsError, LogTime));

        }
        public void Add(Exception e, LogPriority priority = LogPriority.Highest)
        {
            Add(new Log(e.ToString(), priority, true, DateTime.Now));
        }

        public IEnumerable<Log> GetLogs()
        {
            return logs.Values;
        }

        public void CommitCache()
        {
            ValidateState();
            foreach (var log in logs)
                logManager.Add(log.Value);
            logs.Clear();
        }

        public void RollbackCache()
        {
            ValidateState();
            logs = new SortedList<DateTime, Log>();
        }
        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
                logManager.DisposeLogCache(this);

            IsDisposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void ValidateState()
        {
            if (IsDisposed)
                throw new ObjectDisposedException("LogCache", "This object has been disposed and no longer in a valid state");
        }
    }
}
