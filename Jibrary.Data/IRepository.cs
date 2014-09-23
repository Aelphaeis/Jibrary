using System;
using System.Collections.Generic;

namespace Jibrary.Data
{
    /// <summary>
    /// This interface represents a repository which can store all types that Inherit from IRepository that implements the default constructor
    /// </summary>
    /// <typeparam name="T">Any Type Inheriting IRepositoryEntry implementing a default constructor</typeparam
    public interface IRepository<T> where T : IRepositoryEntry, new()
    {
 
        /// <summary>
        /// This event is included in the interface with the intent that it is called when an object is inserted into the repository.
        /// </summary>
        /// 
        event EventHandler<RepositoryOperationEventArgs> InsertEvent;

        /// <summary>
        /// This event is included in the interface with the intent that it is called when an object is modified in the repostiory
        /// </summary>
        event EventHandler<RepositoryOperationEventArgs> UpdateEvent;

        /// <summary>
        /// This event is included in the interface with the intent that it is called when an object is removed from the repository.
        /// </summary>
        event EventHandler<RepositoryOperationEventArgs> DeleteEvent;

        /// <summary>
        /// This is the unique identifier with which the IRepositoryEntry can be identified
        /// </summary>
        IList<String> PrimaryKeys { get; }

        /// <summary>
        /// This is used to insert an IRepostioryEntry into the Repository
        /// </summary>
        /// <param name="Entry">The Typed IRepositoryEntry to add to the repository.</param>
        void Insert(T Entry);

        /// <summary>
        /// This is used to modifiy an IRepositoryEntry already existing within the Repository.
        /// </summary>
        /// <param name="Entry"></param>
        void Update(T Entry);

        /// <summary>
        /// This is a condition used to determine if an entry from the repository should be deleted. All IRepositoryEntries that furfill this condition will be deleted
        /// </summary>
        /// <param name="predicate">The condition to be checked.</param>
        void Delete(Predicate<T> predicate);

        /// <summary>
        /// This is a condition used to determine if any entry from the repository meets the criteria specified in the predicate
        /// </summary>
        /// <param name="predicate">the condition to be checked</param>
        /// <returns>True if there exist any IRepositoryEntries meeting the criteria, othrewise false.</returns>
        bool Exists(Predicate<T> predicate);

        /// <summary>
        /// Retrieves the first entry which meets the condition
        /// </summary>
        /// <param name="predicate">returns an IRespositoryEntry meeting the condition specified in the predicate</param>
        /// <returns></returns>
        T Retrieve(Predicate<T> predicate);

        /// <summary>
        /// Retrieve All IRepositoryEntries stored in the repository.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> RetrieveAll();
    }
}
