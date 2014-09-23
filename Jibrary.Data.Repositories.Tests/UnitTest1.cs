using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jibrary.Data.Repositories.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        { 
            List<Int32> list = new List<Int32> { 1, 2, 3, 4, 5 };
            foreach(var v in list.AsQueryable().Where(p => p > 2))
                Console.WriteLine(v);
        }
    }
}
