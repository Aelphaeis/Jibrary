using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jibrary.Miscellaneous.Tests.Resources
{
    public class Person
    {
        public String Name { get; set; }
        public Int32 Age { get; set; }

        public Person()
        {
            Name = String.Empty;
            Age = 0;
        }

        public Person(String n, Int32 a)
        {
            Name = n;
            Age = a;

        }
    }
}
