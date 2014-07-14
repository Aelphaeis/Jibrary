using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace Jibrary.Data
{
    
    public class JibraryQueryResult : IJibraryQueryResult<JibraryQueryResultColumn, JibraryQueryResultTuple>
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
        public virtual List<JibraryQueryResultColumn> Columns { get; set; }
        public virtual List<JibraryQueryResultTuple> Tuples { get; set; }

        #region Constructors
        public JibraryQueryResult()
        {
            this.Columns = new List<JibraryQueryResultColumn>();
            this.Tuples = new List<JibraryQueryResultTuple>();
        }

        public JibraryQueryResult(IDataReader r) : this()
        {
            var schemaTable = r.GetSchemaTable();
            foreach (var column in GetColumnData(schemaTable))
                Columns.Add(column);

            for (var Curr = new JibraryQueryResultTuple() { parent = this }; r.Read(); Tuples.Add(Curr), Curr = new JibraryQueryResultTuple() { parent = this })
                for (int i = 0; i < Columns.Count; i++)
                    Curr.Values.Add((r[Columns[i].ColumnName] != DBNull.Value) ? r[Columns[i].ColumnName] : null);
        }
        #endregion

        public int GetColumnIndex(String ColumnName)
        {
            //This can be optimized but was done in the interest of fast coding and readability
            return Columns.IndexOf(Columns.First((p) => p.ColumnName.Equals(ColumnName, StringComparison.OrdinalIgnoreCase)));
        }

        IEnumerable<JibraryQueryResultColumn> GetColumnData(DataTable dataTable)
        {
            for (int i = 0; i < dataTable.Rows.Count; i++)
                yield return new JibraryQueryResultColumn((DataRow)dataTable.Rows[i]);
        }
    }
}
