using System;
using Jibrary.Miscellaneous;
using Jibrary.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jibrary.Miscellaneous.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Singleton<TimeAnalyst>.Instance.CreateTask("Hello World");
            Console.WriteLine(Singleton<TimeAnalyst>.Instance);
            Singleton<TimeAnalyst>.Instance.RemoveTask("Hello World");

        }
    }
}
