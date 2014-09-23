using System;
using System.Linq;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

namespace Jibrary.Data
{
    public class RepositoryEntryBase
    {
        public static RepositoryEntryBase FromPlexQueryResultTuple(RepositoryEntryBase reb, QueryResultTuple plexTuple)
        {
            if (plexTuple.parent == null)
                throw new NotSupportedException("This Operation is Not supported by this PlexTuple.");

            Type type = reb.GetType();
            var pInfo = type.GetProperties();
            QueryResult result = plexTuple.parent;

            foreach (var p in pInfo)
            {
                int index = result.Tuples.IndexOf(plexTuple);

                if (result[p.Name, index] == null)
                    continue;

                var conversationType = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;
                object value = Convert.ChangeType(result[p.Name, index], (result[p.Name, index] != null) ? conversationType : p.PropertyType);
                p.SetValue(reb, value);
            }
            return reb;
        }

        public static Boolean AreEqual(RepositoryEntryBase a, RepositoryEntryBase b)
        {
            var TypeA = a.GetType();
            if (TypeA != b.GetType())
                return false;

            var keys = a.GetPrimaryKeys();
            foreach (var property in TypeA.GetProperties().Where(p => keys.Any(q => p.Name == q)))
                if (!property.GetValue(a).Equals(property.GetValue(b)))
                    return false;
            return true;
        }

        protected IList<String> primaryKeys;

        public RepositoryEntryBase()
        {
            primaryKeys = new List<String>();
        }
        public RepositoryEntryBase(QueryResultTuple plexTuple)
            : this()
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }

        public IList<String> GetPrimaryKeys()
        {
            return primaryKeys;
        }
    }
}
