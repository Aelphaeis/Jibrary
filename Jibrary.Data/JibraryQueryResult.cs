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
        public virtual List<JibraryQueryResultColumn> Columns { get { return columns; } set { columns = value; } }
        public virtual List<JibraryQueryResultTuple> Tuples { get { return tuples; } set { tuples = value; } }

        List<JibraryQueryResultColumn> columns;
        List<JibraryQueryResultTuple> tuples;

        #region Constructors
        public JibraryQueryResult()
        {
            this.columns = new List<JibraryQueryResultColumn>();
            this.tuples = new List<JibraryQueryResultTuple>();
        }

        public JibraryQueryResult(IDataReader r) : this()
        {
            var schemaTable = r.GetSchemaTable();
            foreach (var column in GetColumnData(schemaTable))
                columns.Add(column);

            for (var Curr = new JibraryQueryResultTuple() { parent = this }; r.Read(); tuples.Add(Curr), Curr = new JibraryQueryResultTuple() { parent = this })
                for (int i = 0; i < columns.Count; i++)
                    Curr.Values.Add((r[columns[i].ColumnName] != DBNull.Value) ? r[columns[i].ColumnName] : null);
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
