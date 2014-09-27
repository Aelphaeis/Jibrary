using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jibrary.Diagnostics
{
    public class LogCache
    {
        List<Log> logs;
        LogManager logManager;

        public event EventHandler<LogAddedEventArgs> LogAddedEvent;

        internal LogCache(LogManager manager)
        {
            logs = new List<Log>();
            logManager = manager;
        }
        /// <summary>
        /// Adds a log to the master log file. The log will not be added immediately if there are any live caches. To destory all caches use DisposeAllCaches().
        /// </summary>
        /// <param name="Log">The Log to add to the master file</param>
        public virtual void Add(Log Log)
        {
            if (String.IsNullOrEmpty(Log.Entry))
                throw new ArgumentException("Log has no entry. All Logs should have entries");
            Log.Hidden = true;
            logs.Add(Log);


            if (LogAddedEvent != null)
                LogAddedEvent(this, new LogAddedEventArgs { Log = Log });
        }
        /// <summary>
        /// Adds a log to the master file based on the text associated with the log you wish to store. The rest of the log will be automatically generated.
        /// </summary>
        /// <param name="Entry">Text you want in the log</param>
        public virtual void Add(String Entry)
        {
            Add(new Log(Entry));
        }

        /// <summary>
        /// Adds a log to the master file based on the text and priority associated with the log you wish to store. The rest of the log will be automatically generated.
        /// </summary>
        /// <param name="Entry">Text you want in the log</param>
        /// <param name="Priority">The priority you wish the log to have</param>
        public virtual void Add(String Entry, LogPriority Priority)
        {
            Add(new Log(Entry, Priority));
        }

        /// <summary>
        /// Adds a log to the master file based on the text and priority associated with the log you wish to store. The rest of the log will be automatically generated.
        /// You may also specify if this log is represents a malfunction.
        /// </summary>
        /// <param name="Entry">Text you want in the log</param>
        /// <param name="Priority">The priority you wish the log to have</param>
        /// <param name="IsError">If the log represents an error or not </param>
        public virtual void Add(String Entry, LogPriority Priority, bool IsError)
        {
            Add(new Log(Entry, Priority, IsError));
        }

        /// <summary>
        /// Adds a log to the master file based on the text and priority associated with the log you wish to store. The rest of the log will be automatically generated.
        /// You may also specify if this log is represents a malfunction. You can also specific the date under which the log occured.
        /// </summary>
        /// <param name="Entry">Text you want in the log</param>
        /// <param name="Priority">The priority you wish the log to have</param>
        /// <param name="IsError">If the log represents an error or not </param>
        /// <param name="LogTime">The time the message is suppose to represent.</param>
        public virtual void Add(String Entry, LogPriority Priority, bool IsError, DateTime LogTime)
        {
            Add(new Log(Entry, Priority, IsError, LogTime));
        }

        /// <summary>
        /// Adds a log to the master file based on an exception. This will set isError to true and the time of the exception to the moment it is added as a log.
        /// </summary>
        /// <param name="e">The exception to log</param>
        /// <param name="priority">The priority to log the exception with, the default is Highest</param>
        public virtual void Add(Exception e, LogPriority priority = LogPriority.Highest)
        {
            Add(new Log(e.ToString(), priority, true, DateTime.Now));
        }

        /// <summary>
        /// Get all logs currently stored.
        /// </summary>
        /// <returns>An Enumerable of Logs</returns>
        public IEnumerable<Log> GetLogs()
        {
            return logs.ToList();
        }

        public void CommitCache()
        {
            logs.ForEach(p => p.Hidden = false);
            logs.Clear();
        }

        public void RollbackCache()
        {
            logs.ForEach(p => logManager.Logs.Remove(p));
            logs.Clear();
        }
    }
}
