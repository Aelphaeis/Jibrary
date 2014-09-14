using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jibrary.Miscellaneous
{
    public sealed class Singleton<T> where T : new()
    {
        public static T Instance { get { return instance; } }
        static T instance = new T();

        internal Singleton() { }
    }
}
