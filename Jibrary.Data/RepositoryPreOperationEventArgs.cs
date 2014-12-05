using System;

namespace Jibrary.Data
{
    public class RepositoryPreOperationEventArgs
    {
        public Object Entry { get; set; }
        public Boolean Cancel { get; set; }
    }
}
