using System;

namespace Jibrary.Data
{
    public class RepositoryPostOperationEventArgs : EventArgs
    {
        public virtual Object Entry { get; set; }
    }
}
