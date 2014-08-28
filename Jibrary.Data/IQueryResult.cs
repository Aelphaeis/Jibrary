using System;
using System.Collections.Generic;

namespace Jibrary.Data
{
    public interface IQueryResult<C,T>
        where T: IQueryResultTuple
        where C: IQueryResultColumn 
    {
        Object this[Int32 ColumnIndex, Int32 RowIndex]{ get; }
        Object this[String ColumnName, Int32 RowIndex]{ get; }
        IEnumerable<Object> this[Int32 ColumnIndex] { get; }
        IEnumerable<Object> this[String ColumnName] { get; }
        List<C> Columns { get; set; }
        List<T> Tuples { get; set; }
        
    }
}
