using System;
using System.Collections.Generic;

namespace Jibrary.Data
{
    public interface IQueryResultTuple  
    {
        Object this[int i] { get; set; }

        List<Object> Values { get; set; }

    }
}
