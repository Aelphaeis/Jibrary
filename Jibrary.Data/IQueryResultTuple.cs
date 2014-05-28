using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jibrary.Data
{
    public interface IQueryResultTuple 
    {
        IQueryResult ResultSet { get; set; }
        Object[] Values { get; set; }
    }
}
