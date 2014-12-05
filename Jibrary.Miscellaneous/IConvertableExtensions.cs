using System;
using System.Windows.Forms;

namespace Jibrary.Miscellaneous
{
    public static class IConvertableExtensions
    {
        public static Int32 ToInt32 (this IConvertible value)
        {
            return Convert.ToInt32(value);
        }

        public static void ToClipboard (this IConvertible value)
        {
            Clipboard.SetText(Convert.ToString(value));
        }
    }
}
