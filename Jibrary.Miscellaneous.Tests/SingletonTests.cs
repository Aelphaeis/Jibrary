using System;
using Jibrary.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jibrary.Miscellaneous.Tests.Resources;
namespace Jibrary.Miscellaneous.Tests
{
    [TestClass]
    public class SingletonTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(0, Singleton<Int32>.Instance);

            Assert.AreEqual(String.Empty, Singleton<String>.Instantiate(String.Empty));
            Assert.AreEqual(String.Empty, Singleton<String>.Instance);

            Assert.AreNotEqual(null, Singleton<TimeTask>.Instance);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestMethod2()
        {
            Assert.AreEqual(String.Empty, Singleton<String>.Instantiate(String.Empty));
            Assert.AreEqual(String.Empty, Singleton<String>.Instantiate("Test Value"));
        }

        [TestMethod]
        public void TestMethod4()
        {
            Person p = new Person() { Name = "Joseph" };
            Singleton<Person>.Instantiate(p);
            Assert.AreEqual(p, Singleton<Person>.Instance);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestMethod5()
        {
            Person p = new Person() { Name = "Joseph" };
            Assert.AreNotEqual(p, Singleton<Person>.Instance);
            Singleton<Person>.Instantiate(p);
        }
    }
}
