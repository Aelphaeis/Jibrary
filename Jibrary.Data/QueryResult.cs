using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace Jibrary.Data
{
    
    public class QueryResult : IQueryResult<QueryResultColumn, QueryResultTuple>
    {
        public Object this[Int32 ColumnIndex, Int32 RowIndex]
        {
            get
            {
                return Tuples[RowIndex].Values[ColumnIndex];
            }
        }
        public Object this[String ColumnName, Int32 RowIndex]
        {
            get
            {
                return this[GetColumnIndex(ColumnName), RowIndex];
            }
        }
        public IEnumerable<Object> this[Int32 ColumnIndex]
        {
            get
            {
                foreach (var row in Tuples)
                    yield return row.Values[ColumnIndex];
            }
        }
        public IEnumerable<Object> this[String ColumnName]
        {
            get
            {
                return this[GetColumnIndex(ColumnName)];
            }
        }
        public virtual List<QueryResultColumn> Columns { get { return columns; } set { columns = value; } }
        public virtual List<QueryResultTuple> Tuples { get { return tuples; } set { tuples = value; } }

        List<QueryResultColumn> columns;
        List<QueryResultTuple> tuples;

        #region Constructors
        public QueryResult()
        {
            this.columns = new List<QueryResultColumn>();
            this.tuples = new List<QueryResultTuple>();
        }

        public QueryResult(IDataReader r) : this()
        {
            var schemaTable = r.GetSchemaTable();
            foreach (var column in GetColumnData(schemaTable))
                columns.Add(column);

            for (var Curr = new QueryResultTuple() { parent = this }; r.Read(); tuples.Add(Curr), Curr = new QueryResultTuple() { parent = this })
                for (int i = 0; i < columns.Count; i++)
                    Curr.Values.Add((r[columns[i].ColumnName] != DBNull.Value) ? r[columns[i].ColumnName] : null);
        }
        #endregion

        public int GetColumnIndex(String ColumnName)
        {
            //This can be optimized but was done in the interest of fast coding and readability
            return Columns.IndexOf(Columns.First((p) => p.ColumnName.Equals(ColumnName, StringComparison.OrdinalIgnoreCase)));
        }

        IEnumerable<QueryResultColumn> GetColumnData(DataTable dataTable)
        {
            for (int i = 0; i < dataTable.Rows.Count; i++)
                yield return new QueryResultColumn((DataRow)dataTable.Rows[i]);
        }
    }
}
