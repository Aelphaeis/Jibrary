using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Jibrary.Data
{
    public class QueryResultTuple : IQueryResultTuple
    {
        internal QueryResult parent;
         
        public virtual Object this[int i]
        {
            get
            {
                return Values[i];
            }
            set 
            {
                Values[i] = value;
            }
        }


        public virtual List<Object> Values { get { return values; } set { values = value; } }
        public List<Object> values;

        public QueryResultTuple()
        {
            parent = null;
            values = new List<Object>();
        }
    }
}
