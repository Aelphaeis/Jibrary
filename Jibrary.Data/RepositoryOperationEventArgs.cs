using System;

namespace Jibrary.Data
{
    public class RepositoryOperationEventArgs : EventArgs
    {
        public virtual IRepositoryEntry Entry { get; set; }
    }
}
