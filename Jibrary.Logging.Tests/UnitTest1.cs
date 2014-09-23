using System;
using Jibrary.Logging;
using Jibrary.Logging.Tests.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jibrary.Logging.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            LoggedClass lc = new LoggedClass();
            lc.DoWork();
        }
    }
}
