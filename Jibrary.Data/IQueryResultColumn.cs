using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jibrary.Data
{
    interface IQueryResultColumn
    {
        public String ColumnName { get; set; }
        public String DataType { get; set; }
    }
}
