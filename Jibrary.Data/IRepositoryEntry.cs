using System;
using System.Collections.Generic;

namespace Jibrary.Data
{
    public interface IRepositoryEntry
    {
        IList<String> GetPrimaryKeys();
    }
}
