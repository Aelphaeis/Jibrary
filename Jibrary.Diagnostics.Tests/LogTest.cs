using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Jibrary.Diagnostics.Tests
{
    [TestClass]
    public class LogTest
    {
        [TestMethod]
        [Timeout(20)]
        public void ConstructorTest1()
        {
            Log log = new Log();
            Assert.AreEqual(log.Entry, String.Empty);
            Assert.AreEqual(log.IsError, false);
            Assert.AreEqual(log.Priority, LogPriority.Normal);
        }

        [TestMethod]
        [Timeout(20)]
        public void ConstructorTest2()
        {
            string entryText = "This is a test string";
            Log log = new Log(entryText);
            Assert.AreEqual(log.Entry, entryText);
            Assert.AreEqual(log.IsError, false);
            Assert.AreEqual(log.Priority, LogPriority.Normal);
        }

        [TestMethod]
        [Timeout(20)]
        public void ConstructorTest3()
        {
            string entryText = "This is a test string";
            LogPriority priority = LogPriority.High;
            Log log = new Log(entryText, priority);
            Assert.AreEqual(log.Entry, entryText);
            Assert.AreEqual(log.IsError, false);
            Assert.AreEqual(log.Priority, priority);
        }

        [TestMethod]
        [Timeout(20)]
        public void ConstructorTest4()
        {
            string entryText = "this is a test string";
            LogPriority priority = LogPriority.High;
            Log log = new Log(entryText, priority, true);

            Assert.AreEqual(log.Entry, entryText);
            Assert.AreEqual(log.IsError, true);
            Assert.AreEqual(log.Priority, priority);
        }

        [TestMethod]
        [Timeout(20)]
        public void ConstructorTest5()
        {
            string entryText = "this is a test string";
            LogPriority priority = LogPriority.High;
            Log log = new Log(entryText, priority, false, DateTime.MinValue);

            Assert.AreEqual(log.Entry, entryText);
            Assert.AreEqual(log.IsError, false);
            Assert.AreEqual(log.Priority, priority);
            Assert.AreEqual(log.Date, DateTime.MinValue);
        }
    }
}
