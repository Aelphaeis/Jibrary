using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jibrary.Diagnostics.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            TimeAnalyst ta = new TimeAnalyst();
            ta.CreateTask("Joseph");
            ta.CreateTask("May");
            Console.WriteLine(ta);
        }
    }
}
