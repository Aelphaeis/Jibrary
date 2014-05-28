using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jibrary.Data
{
    public interface IQueryResult
    {
        public IQueryResultColumn[] Columns { get; set; }
        public IQueryResultTuple[] Tuples { get; set; }
    }
}
