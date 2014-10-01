using System;

namespace Jibrary.Data.Repositories
{
    public class RepositoryPostOperationEventArgs : EventArgs
    {
        public virtual Object Entry { get; set; }
    }
}
