using System;
using System.Threading;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jibrary.Diagnostics.Tests
{
    [TestClass]
    public class TimeAnalystTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            TimeAnalyst ta = new TimeAnalyst();
            ta.StartTask("task1");
            Thread.Sleep(1000);
            ta.StartTask("Jordan");
            Thread.Sleep(1000);
            ta.StartTask("Joseph");
            Thread.Sleep(1000);

            Console.WriteLine(ta);
        }
    }
}
