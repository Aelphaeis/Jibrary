using System;
using System.Linq;
using System.Collections.Generic;


namespace Jibrary.Logging
{
    public sealed class LogManager
    {
        /// <summary>
        /// Contains a global instance which stores all logs.
        /// </summary>
        SortedList<DateTime, Log> Logs;
        SortedList<DateTime, Log> intermediate;
        List<LogCache> caches;

        /// <summary>
        /// An Event called when a Log is added. 
        /// </summary>
        public event LogManagerEventHandler LogAdded;
        event EventHandler CacheDiscard;
        /// <summary>
        /// Fall back add function to handle duplicate key's in the lists
        /// </summary>
        /// <param name="log">The Log to add to the master file</param>
        /// <param name="key">The key to to pretend it has</param>
        void Add(Log log, DateTime key)
        {
            try
            {
                if(caches.Count == 0)
                { 
                    Logs.Add(key, log);
                    if (LogAdded != null)
                        LogAdded(this, new LogManagerEventArgs() { Log = log });
                }
                else
                {
                    intermediate.Add(key, log);

                }
            }
            catch (ArgumentException)
            {
                Add(log, key.Add(TimeSpan.FromTicks(1L)));
            }
        }

        /// <summary>
        /// Adds a log to the master log file. The log will not be added immediately if there are any live caches. To destory all caches use DisposeAllCaches().
        /// </summary>
        /// <param name="Log">The Log to add to the master file</param>
        public void Add(Log Log) 
        {
            if ((Log.Entry ?? String.Empty) == String.Empty)
                throw new ArgumentException("Log has no entry. All Logs should have entries");

            try 
            {
                if (caches.Count == 0) 
                { 
                    Logs.Add(Log.Date, Log);
                    if(LogAdded != null )
                        LogAdded(this, new LogManagerEventArgs() { Log = Log });
                }
                else 
                { 
                    intermediate.Add(Log.Date, Log);
                }

            }
            catch (ArgumentException)
            {
                Add(Log, Log.Date.Add(TimeSpan.FromTicks(1L)));
            }
        }

        /// <summary>
        /// Adds a log to the master file based on the text associated with the log you wish to store. The rest of the log will be automatically generated.
        /// </summary>
        /// <param name="Entry">Text you want in the log</param>
        public void Add(String Entry)
        {
            Add(new Log(Entry));
        }

        /// <summary>
        /// Adds a log to the master file based on the text and priority associated with the log you wish to store. The rest of the log will be automatically generated.
        /// </summary>
        /// <param name="Entry">Text you want in the log</param>
        /// <param name="Priority">The priority you wish the log to have</param>
        public void Add(String Entry, LogPriority Priority)
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
        public void Add(String Entry, LogPriority Priority, bool IsError)
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
        public void Add(String Entry, LogPriority Priority, bool IsError, DateTime LogTime)
        {
            Add(new Log(Entry, Priority, IsError, LogTime));

        }

        /// <summary>
        /// Adds a log to the master file based on an exception. This will set isError to true and the time of the exception to the moment it is added as a log.
        /// </summary>
        /// <param name="e">The exception to log</param>
        /// <param name="priority">The priority to log the exception with, the default is Highest</param>
        public void Add(Exception e, LogPriority priority = LogPriority.Highest)
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
            caches.Add(cache);
            return cache;
        }

        /// <summary>
        /// Destories and invalidates the specified Cache.
        /// </summary>
        /// <param name="cache">The Cache to be invalidated and destoried.</param>
        public void DisposeLogCache(LogCache cache)
        {
            caches.Remove(cache);
            CacheDiscard(this, EventArgs.Empty);
        }

        /// <summary>
        /// This invalidates all Caches 
        /// </summary>
        public void DisposeAllCaches()
        {
            while(caches.Count > 0)
                DisposeLogCache(caches.First());
        }

        /// <summary>
        /// Get all logs currently stored.
        /// </summary>
        /// <returns>An Enumerable of Logs</returns>
        public IEnumerable<Log> GetLogs() 
        {
            return Logs.Values.ToList();
        }

        public LogManager()
        {
            Logs = new SortedList<DateTime, Log>();
            intermediate = new SortedList<DateTime, Log>();
            caches = new List<LogCache>();

            CacheDiscard += Merge;
            //LogAdded += InsertLogIntoCollection;
        }

        void Merge(object sender, EventArgs e)
        {
            if (caches.Count == 0) { 
                foreach (var l in intermediate)
                    Add(l.Value);
                intermediate.Clear();
            }
        }

        //void InsertLogIntoCollection(object sender, LogManagerEventArgs e)
        //{
        //    try {
        //        Logs.Add(e.Log.Date, e.Log);
        //    }
        //    catch(ArgumentException) {
        //        e.Log.Date = e.Log.Date.AddTicks(1);
        //        Add(e.Log);
        //    }
        //}
    }
}
