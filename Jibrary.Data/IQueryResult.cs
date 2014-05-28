using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Jibrary.Data
{
    public interface IQueryResult
    {
        IQueryResultColumn[] Columns { get; set; }
        IQueryResultTuple[] Tuples { get; set; }

        int GetColumnIndex(String columnName);
        int GetRowValue(String columnName, Int32 rowIndex);
        int GetRowValue(Int32 columnIndex, Int32 rowIndex);
        IEnumerable<object> GetColumnValues(String columnName);
        IEnumerable<object> GetColumnValues(Int32 columnIndex);
    }
}
