using System;

namespace Jibrary.Data.Repositories
{
    public class RepositoryOperationEventArgs : EventArgs
    {
        public virtual IRepositoryEntry Entry { get; set; }
    }
}
