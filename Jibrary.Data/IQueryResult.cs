using System;
using System.Collections.Generic;

namespace Jibrary.Data
{
    public interface IQueryResult
    {
        Object this[Int32 ColumnIndex, Int32 RowIndex] { get; }
        Object this[String ColumnName, Int32 RowIndex] { get; }
        IEnumerable<Object> this[Int32 ColumnIndex] { get; }
        IEnumerable<Object> this[String ColumnName] { get; }
    }

    public interface IQueryResult<C,T> : IQueryResult
        where T: IQueryResultTuple
        where C: IQueryResultColumn 
    {

        List<C> Columns { get; set; }
        List<T> Tuples { get; set; }
        
    }
}
