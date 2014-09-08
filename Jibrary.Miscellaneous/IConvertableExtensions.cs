using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jibrary.Miscellaneous
{
    public static class IConvertableExtensions
    {
        public static Int32 ToInt32 (IConvertible value)
        {
            return Convert.ToInt32(value);
        }
    }
}
