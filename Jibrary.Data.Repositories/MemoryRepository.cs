using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jibrary.Data.Repositories
{
    public class InMemoryRepository<T> : IRepository<T>
    {

        public event EventHandler<RepositoryPreOperationEventArgs> BeforeInsertEvent;

        public event EventHandler<RepositoryPostOperationEventArgs> AfterInsertEvent;

        public event EventHandler<RepositoryPreOperationEventArgs> BeforeDeleteEvent;

        public event EventHandler<RepositoryPostOperationEventArgs> AfterDeleteEvent;

        List<T> data;

        public InMemoryRepository()
        {
            data = new List<T>();
        }

        public T Add(T entry)
        {
            var preOpArgs = new RepositoryPreOperationEventArgs { Cancel = false, Entry = entry };
            if (BeforeInsertEvent != null)
                BeforeInsertEvent(this, preOpArgs);

            if (preOpArgs.Cancel)
                return default(T);

            data.Add(entry);

            if (AfterInsertEvent != null)
                AfterInsertEvent(this, new RepositoryPostOperationEventArgs { Entry = entry });

            return entry;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entries)
        {
            foreach (var entry in entries)
                Add(entry);
            return entries;
        }

        public T Find(Predicate<T> condition)
        {
            return data.Find(condition);
        }

        public IEnumerable<T> FindAll(Predicate<T> condition)
        {
            return data.FindAll(condition);
        }

        public void Remove(Predicate<T> condition)
        {
            var entry = Find(condition);

            var preOpArgs = new RepositoryPreOperationEventArgs { Cancel = false, Entry = entry };
            if (BeforeDeleteEvent != null)
                BeforeDeleteEvent(this, preOpArgs);

            if (preOpArgs.Cancel)
                return;


            data.Remove(entry);
            
            if (AfterDeleteEvent != null)
                AfterDeleteEvent(this, new RepositoryPostOperationEventArgs { Entry = entry });
        }

        public void RemoveAll(Predicate<T> condition)
        {
            var entries = FindAll(condition);

            foreach (var entry in entries)
                Remove(p => p.Equals(entry));
        }

        public int Count()
        {
            return data.Count;
        }

        public bool Exists(Predicate<T> predicate)
        {
            return data.Exists(predicate);
        }

        public IQueryable<T> AsQueryable()
        {
           return new RepositoryQuery<T>(new RepositoryQueryProvider(), data);
        }
    }
}
