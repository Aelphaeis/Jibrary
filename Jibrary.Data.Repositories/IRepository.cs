using System;
using System.Linq;
using System.Collections.Generic;

namespace Jibrary.Data.Repositories
{
    public interface IRepository<T> 
    {
        event EventHandler<RepositoryPreOperationEventArgs> BeforeInsertEvent;
        event EventHandler<RepositoryPostOperationEventArgs> AfterInsertEvent;

        event EventHandler<RepositoryPreOperationEventArgs> BeforeDeleteEvent;
        event EventHandler<RepositoryPostOperationEventArgs> AfterDeleteEvent;

        //Create
        T Add(T entry);
        IEnumerable<T> AddRange(IEnumerable<T> entries);

        //Read
        T Find(Predicate<T> condition);
        IEnumerable<T> FindAll(Predicate<T> condition);

        //Delete
        void Remove(Predicate<T> condition);
        void RemoveAll(Predicate<T> condition);

        //Miscellenous Operations
        int Count();
        bool Exists(Predicate<T> predicate);
        IQueryable<T> AsQueryable();
    }
}
