using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jibrary.Miscellenous
{
    public static class IConvertableExtensions
    {
        public static Int32 ToInt32 (this IConvertible value)
        {
            return Convert.ToInt32(value);
        }
    }
}
