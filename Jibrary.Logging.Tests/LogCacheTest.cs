using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Jibrary.Logging.Tests
{
    [TestClass]
    public class LogCacheTest
    {
        [TestMethod]
        [Timeout(20)]
        public void LogCacheConstructorTest()
        {
            LogManager manager = new LogManager();
            using (var cache = manager.CreateLogCache())
                Assert.IsTrue(true);
        }

        [TestMethod]
        [Timeout(20)]
        [ExpectedException(typeof(ArgumentException))]
        public void LogCacheAddOverload1Failure()
        {        
            LogManager manager = new LogManager();
            using (var cache = manager.CreateLogCache())
                cache.Add(new Log());
        }


        [TestMethod]
        [Timeout(20)]
        public void LogCacheAddOverlad1Success()
        {
            LogManager manager = new LogManager();
            using(var cache = manager.CreateLogCache())
            {
                cache.Add("test");
                Assert.AreEqual(cache.GetLogs().Count(), 1);

                cache.CommitCache();
                Assert.AreEqual(cache.GetLogs().Count(), 0);
            }
            Assert.AreEqual(manager.GetLogs().Count(), 1);
        }

        [TestMethod]
        [Timeout(20)]
        public void LogCacheAddRollback1Success()
        {
            LogManager manager = new LogManager();
            using (var cache = manager.CreateLogCache())
            {
                cache.Add("test");
                Assert.AreEqual(cache.GetLogs().Count(), 1);

                cache.RollbackCache();
                Assert.AreEqual(cache.GetLogs().Count(), 0);
            }
            Assert.AreEqual(manager.GetLogs().Count(), 0);
        }

        [TestMethod]
        [Timeout(20)]
        public void LogCacheAddOverload1Failure2()
        {
            LogManager manager = new LogManager();
            using (var cache = manager.CreateLogCache())
            {
                var l1 = new Log();
                var l2 = new Log();

                string entryText = "This is some entry texts";
                l1.Entry = entryText;
                l2.Entry = entryText;

                cache.Add(l1);
                cache.Add(l2);

                cache.CommitCache();
            }
        }
    }
}
