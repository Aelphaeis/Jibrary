using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jibrary.Miscellenous.Tests
{
    [TestClass]
    public class IConvertableExtensionsTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            String value = "235232";
            Assert.AreEqual(235232, value.ToInt32());
        }
    }
}
