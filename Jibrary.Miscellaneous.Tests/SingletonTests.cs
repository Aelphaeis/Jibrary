using System;
using Jibrary.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestMethod3()
        {
            Assert.AreNotEqual(String.Empty, Singleton<String>.Instance);
        }


    }
}
