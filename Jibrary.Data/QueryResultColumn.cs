using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Jibrary.Data
{
    public class QueryResultColumn : IQueryResultColumn
    {
        public virtual string ColumnName { get; set; }
        public virtual int? ColumnSequence { get; set; }
        public virtual string DataType { get; set; }
        public virtual int? DataLength { get; set; }
        public virtual int? DataPrecision { get; set; }
        public virtual int? DataScale { get; set; }
        public virtual bool? AllowDbNull { get; set; }
        public virtual bool? IsReadOnly { get; set; }
        public virtual bool? IsLong { get; set; }
        public virtual bool? IsKey { get; set; }
        public virtual string KeyType { get; set; }
        public virtual bool? IsUnique { get; set; }
        public virtual string Description { get; set; }

        public QueryResultColumn() { }
        public QueryResultColumn(DataRow dataRow)
        {
            ColumnName = (dataRow[0] != DBNull.Value) ? Convert.ToString(dataRow[0]) : string.Empty;
            IsUnique = (dataRow[5] != DBNull.Value) ? Convert.ToBoolean(dataRow[5]) : (bool?)null;

            IsKey = (dataRow[6] != DBNull.Value) ? Convert.ToBoolean(dataRow[6]) : (bool?)null;
            DataType = (dataRow[11] != DBNull.Value) ? dataRow[11].ToString() : string.Empty;
            IsReadOnly = (dataRow[18] != DBNull.Value) ? Convert.ToBoolean(dataRow[18]) : (bool?)null;
            IsLong = (dataRow[19] != DBNull.Value) ? Convert.ToBoolean(dataRow[19]) : (bool?)null;
        }
    }
}
