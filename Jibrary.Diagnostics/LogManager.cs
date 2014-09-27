using System;
using System.Linq;
using System.Collections.Generic;


namespace Jibrary.Diagnostics
{
    public class LogManager
    {
        /// <summary>
        /// Contains a global instance which stores all logs.
        /// </summary>
        internal List<Log> Logs;

        /// <summary>
        /// An Event called when a Log is added. 
        /// </summary>
        public event EventHandler<LogAddedEventArgs> LogAddedEvent;

        public LogManager()
        {
            Logs = new List<Log>();
        }

        /// <summary>
        /// Adds a log to the master log file. The log will not be added immediately if there are any live caches. To destory all caches use DisposeAllCaches().
        /// </summary>
        /// <param name="Log">The Log to add to the master file</param>
        public virtual void Add(Log Log) 
        {
            if (String.IsNullOrEmpty(Log.Entry))
                throw new ArgumentException("Log has no entry. All Logs should have entries");
            Logs.Add(Log);
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
        /// Creates a cache and registers it with the Log Manager. Logs can be stored and this cache and all of the logs can either be discarded or added to the Log Manager. 
        /// 
        /// Note that  logs added to the log manager while a cache is still alive will not be visible until all caches are closed.
        /// </summary>
        /// <example>
        /// using (var Cache = Logs.CreateLogCache())
        /// {
        ///     Cache.Add(new Log("A Log Was Created"));
        ///     Cache.CommitCache();
        /// }
        /// </example>
        /// <returns>Created Instance the Log Cache</returns>
        public LogCache CreateLogCache()
        {
            var cache = new LogCache(this);
            cache.LogAddedEvent += (sender, args) => Logs.Add(args.Log);
            return cache;
        }

        /// <summary>
        /// Get all logs currently stored.
        /// </summary>
        /// <returns>An Enumerable of Logs</returns>
        public IEnumerable<Log> GetLogs() 
        {
            return Logs.Where(p => !p.Hidden);
        }
    }
}
