using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Jibrary.Logging.Test
{
    [TestClass]
    public class LogManagerTest
    {
        [TestMethod]
        [Timeout(20)]
        public void ManagerConstructorTest()
        {
            LogManager manager = new LogManager();
            Assert.IsTrue(true);
        }

        [TestMethod]
        [Timeout(20)]
        public void ManagerAddOverload1()
        {
            string entryText = "This is some entry texts"; 

            LogManager manager = new LogManager();
            int token = 0;

            LogManagerEventHandler callback = (s, e) => token++;
            manager.LogAdded += callback;

            manager.Add(entryText);

            Assert.AreEqual(token, 1);
            Assert.AreEqual(token, manager.GetLogs().Count());
        }

        [TestMethod]
        [Timeout(20)]
        public void ManagerAddOverload1Concurrent()
        {
            string entryText = "This is some entry texts";

            LogManager manager = new LogManager();
            int token = 0;

            LogManagerEventHandler callback = (s, e) => token++;
            manager.LogAdded += callback;

            manager.Add(entryText);
            manager.Add(entryText);

            Assert.AreEqual(token, 2);
            Assert.AreEqual(token, manager.GetLogs().Count());
        }

        [TestMethod]
        [Timeout(20)]
        public void ManagerAddOverload2()
        {
            string entryText = "This is some entry texts";
            LogPriority priority = LogPriority.High;

            LogManager manager = new LogManager();
            int token = 0;

            LogManagerEventHandler callback = (s, e) => token++;
            manager.LogAdded += callback;

            manager.Add(entryText, priority);
            Assert.AreEqual(token, 1);
            Assert.AreEqual(token, manager.GetLogs().Count());
        }

        [TestMethod]
        [Timeout(20)]
        public void ManagerAddOverload3()
        {
            string entryText = "This is some entry texts";
            LogPriority priority = LogPriority.High;

            LogManager manager = new LogManager();
            int token = 0;

            LogManagerEventHandler callback = (s, e) => token++;
            manager.LogAdded += callback;

            manager.Add(entryText, priority, false);

            Assert.AreEqual(token, 1);
            Assert.AreEqual(token, manager.GetLogs().Count());
        }

        [TestMethod]
        [Timeout(20)]
        public void ManagerAddOverload4()
        {
            string entryText = "This is some entry texts";
            LogPriority priority = LogPriority.High;

            LogManager manager = new LogManager();
            int token = 0;

            LogManagerEventHandler callback = (s, e) => token++;
            manager.LogAdded += callback;

            manager.Add(entryText, priority, false, DateTime.MinValue);

            Assert.AreEqual(token, 1);
            Assert.AreEqual(token, manager.GetLogs().Count());
        }

        [TestMethod]
        [Timeout(20)]
        public void ManagerAddOverload5()
        {
            Exception exception = new Exception();
            LogManager manager = new LogManager();

            int token = 0;
            LogManagerEventHandler callback = (s, e) => token++;
            manager.LogAdded += callback;

            manager.Add(exception);

            Assert.AreEqual(token, 1);
            Assert.AreEqual(token, manager.GetLogs().Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [Timeout(20)]
        public void ManagerAddOverload6Fail()
        {
            Exception exception = new Exception();
            LogManager manager = new LogManager();

            int token = 0;
            LogManagerEventHandler callback = (s, e) => token++;
            manager.LogAdded += callback;

            manager.Add(new Log());
        }

        [TestMethod]
        [Timeout(20)]
        public void ManagerAddOverload6Success()
        {
            string entryText = "This is some entry texts";
            Exception exception = new Exception();
            LogManager manager = new LogManager();

            int token = 0;
            LogManagerEventHandler callback = (s, e) => token++;
            manager.LogAdded += callback;

            manager.Add(new Log(entryText));


            Assert.AreEqual(token, 1);
            Assert.AreEqual(token, manager.GetLogs().Count());
        }
        
        [TestMethod]
        [Timeout(20)]
        public void ManagerAddOverload7()
        {
            LogManager manager = new LogManager();
            var l1 = new Log();
            var l2 = new Log();

            string entryText = "This is some entry texts";
            l1.Entry = entryText;
            l2.Entry = entryText;

            manager.Add(l1);
            manager.Add(l2);
        }

        [TestMethod]
        [Timeout(20)]
        public void ManagerCreateLogCache()
        {
            LogManager manager = new LogManager();
            var cache = manager.CreateLogCache();

            manager.Add("test");
            Assert.AreEqual(manager.GetLogs().Count(), 0);
            
            manager.DisposeAllCaches();
            Assert.AreEqual(manager.GetLogs().Count(), 1);
        }

        [TestMethod]
        [Timeout(20)]
        public void ManagerCreateLogCache2()
        {
            LogManager manager = new LogManager();
            var cache = manager.CreateLogCache();

            manager.Add("test");
            Assert.AreEqual(manager.GetLogs().Count(), 0);

            cache.Dispose();
            Assert.AreEqual(manager.GetLogs().Count(), 1);
        }

        [TestMethod]
        [Timeout(20)]
        public void ManagerCreateLogDuplicateDataIntegrityCheck1()
        {
            LogManager manager = new LogManager();

            var l1 = new Log();
            var l2 = new Log();

            string entryText = "This is some entry texts";
            l1.Entry = entryText;
            l2.Entry = entryText;

            manager.Add(l1);
            manager.Add(l2);

            var logArrays = manager.GetLogs().ToArray();

            Assert.AreEqual(l1.Date, logArrays[0].Date);
            Assert.AreEqual(l1.Entry, logArrays[0].Entry);
            Assert.AreEqual(l1.IsError, logArrays[0].IsError);
            Assert.AreEqual(l1.Priority, logArrays[0].Priority);
        }

        [TestMethod]
        [Timeout(20)]
        public void ManagerCreateLogDuplicateDataIntegrityCheck2()
        {
            LogManager manager = new LogManager();

            var l1 = new Log();
            var l2 = new Log();

            string entryText = "This is some entry texts";
            l1.Entry = entryText;
            l2.Entry = entryText;

            manager.Add(l1);
            manager.Add(l2);

            var logArrays = manager.GetLogs().ToArray();

            Assert.AreEqual(l2.Date, logArrays[1].Date);
            Assert.AreEqual(l2.Entry, logArrays[1].Entry);
            Assert.AreEqual(l2.IsError, logArrays[1].IsError);
            Assert.AreEqual(l2.Priority, logArrays[1].Priority);
        }

        [TestMethod]
        [Timeout(20)]
        public void ManagerCreateLogDuplicateDataIntegrityCheck3()
        {
            LogManager manager = new LogManager();

            var l1 = new Log();
            var l2 = new Log();

            string entryText = "This is some entry texts";
            l1.Entry = entryText;
            l2.Entry = entryText;

            manager.Add(l1);
            manager.Add(l2);

            var logArrays = manager.GetLogs().ToArray();

            Assert.AreEqual(logArrays[0].Date, logArrays[1].Date);
            Assert.AreEqual(logArrays[0].Entry, logArrays[1].Entry);
            Assert.AreEqual(logArrays[0].IsError, logArrays[1].IsError);
            Assert.AreEqual(logArrays[0].Priority, logArrays[1].Priority);
        }


        [TestMethod]
        [Timeout(20)]
        public void ManagerCreateLogDuplicateDataIntegrityCheck4()
        {
            LogManager manager = new LogManager();

            var l1 = new Log();
            var l2 = new Log();

            string entryText = "This is some entry texts";
            l1.Entry = entryText;
            l2.Entry = entryText;

            manager.Add(l1);
            manager.Add(l2);

            var logArrays = manager.GetLogs().ToArray();

            Assert.AreEqual(l1.Date, l2.Date);
            Assert.AreEqual(l1.Entry, l2.Entry);
            Assert.AreEqual(l1.IsError, l2.IsError);
            Assert.AreEqual(l1.Priority, l2.Priority);
        }

    }
}
