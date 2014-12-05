using System;
using System.Collections.Generic;
using System.Linq;

namespace Jibrary.Data
{
    public class InMemoryRepository<T> : IRepository<T>
    {

        public event EventHandler<RepositoryPreOperationEventArgs> BeforeInsertEvent;

        public event EventHandler<RepositoryPostOperationEventArgs> AfterInsertEvent;

        public event EventHandler<RepositoryPreOperationEventArgs> BeforeDeleteEvent;

        public event EventHandler<RepositoryPostOperationEventArgs> AfterDeleteEvent;

        /// <summary>
        /// Internal mechanism by which this Repository stores information
        /// </summary>
        List<T> data;

        public InMemoryRepository()
        {
            //Instantiate the internal list 
            data = new List<T>();
        }

        public T Add(T entry)
        {
            //Throw an event that lets others know that an entry is about to be added
            var preOpArgs = new RepositoryPreOperationEventArgs { Cancel = false, Entry = entry };
            if (BeforeInsertEvent != null)
                BeforeInsertEvent(this, preOpArgs);

            //If one of the subscribers to the BeforeInsert Event changes the Cancel Argument to true 
            //Then do not add and return default(T)
            // null for all classes and default constructor for structures
            if (preOpArgs.Cancel)
                return default(T);

            data.Add(entry);

            //Let everyone know the result of the data that was added to the repository
            if (AfterInsertEvent != null)
                AfterInsertEvent(this, new RepositoryPostOperationEventArgs { Entry = entry });

            return entry;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entries)
        {
            //Allows you to add multiple entries to repository.
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
        /// <summary>
        /// Removes the first element T from the Repository that meets the condition
        /// </summary>
        /// <param name="condition"></param>
        public void Remove(Predicate<T> condition)
        {
            //Find the Entry you're about to remove.
            var entry = Find(condition);

            //Call event to let others know the entry is being deleted
            var preOpArgs = new RepositoryPreOperationEventArgs { Cancel = false, Entry = entry };
            if (BeforeDeleteEvent != null)
                BeforeDeleteEvent(this, preOpArgs);

            //If one of the subscribes changes changes the Cancel variable we stop the deletion.
            if (preOpArgs.Cancel)
                return;

            //The deleting has commenced
            data.Remove(entry);
            
            //After the deletion lets let people know what element was deleted.
            if (AfterDeleteEvent != null)
                AfterDeleteEvent(this, new RepositoryPostOperationEventArgs { Entry = entry });
        }

        /// <summary>
        /// Removes all Elements T from the Repository that meet the condition.
        /// </summary>
        /// <param name="condition"></param>
        public void RemoveAll(Predicate<T> condition)
        {
            //find all entries that meet the condition
            var entries = FindAll(condition);

            //remove all of them
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
            return data.AsQueryable();
        }
    }
}
