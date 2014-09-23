using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jibrary.Logging
{
    public class LoggedAttribute : Attribute
    {
        public LoggedAttribute()
        {
            Console.WriteLine("The Ctor has been called");
        }

    }
}
