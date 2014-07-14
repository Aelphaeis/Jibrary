using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Jibrary.Data
{
    public class JibraryQueryResultTuple : IJibraryQueryResultTuple
    {
        internal JibraryQueryResult parent;
         
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

        
        public virtual List<Object> Values { get; set; }

        public JibraryQueryResultTuple()
        {
            parent = null;
            Values = new List<Object>();
        }
    }
}
