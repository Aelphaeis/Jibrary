using System;
using System.Linq;
using System.Collections.Generic;

namespace Jibrary.Data.Repositories
{

    public interface IRepository<T> where T : IRepositoryEntry, new()
    {
        event EventHandler<RepositoryOperationEventArgs> InsertEvent;
        event EventHandler<RepositoryOperationEventArgs> DeleteEvent;

        //Create
        T Add(T entry);
        T AddRange(IEnumerable<T> entries);

        //Read
        void Find(Predicate<T> condition);
        void FindAll(Predicate<T> condition);

        //Delete
        void Remove(Predicate<T> condition);
        void RemoveAll(Predicate<T> condition);

        //Miscellenous Operations
        void Count();
        void Exists(Predicate<T> predicate);
        IQueryable<T> AsQueryable();
    }
}
