using System;
using Jibrary.Miscellaneous;
using Jibrary.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jibrary.Miscellaneous.Tests.Resources;

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
        [TestMethod]
        public void TestMethod2()
        {
            Console.WriteLine(StringSerializer.DataContractSerialize(new Person { Name = "Joseph", Age = 22 }));
        }
    }
}
