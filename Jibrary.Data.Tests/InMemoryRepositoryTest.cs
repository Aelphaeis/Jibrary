using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jibrary.Data.Tests
{
    [TestClass]
    public class InMemoryRepositoryTest
    {
        /// <summary>
        /// Test the Add Method
        /// </summary>
        [TestMethod]
        public void InMemoryRepositoryTest1()
        {
            InMemoryRepository<Int32> repo = new InMemoryRepository<Int32>();
            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(i, repo.Count());
                repo.Add(i);
            }
        }

        /// <summary>
        /// Test the Add Method Pre Operation Cancel Argument.
        /// </summary>
        [TestMethod]
        public void InMemoryRepositoryTest2()
        {
            InMemoryRepository<Int32> repo = new InMemoryRepository<Int32>();
            repo.BeforeInsertEvent += (sender, args) => args.Cancel = true;
            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(0, repo.Count());
                repo.Add(i);
            }
        }

        [TestMethod]
        /// <summary>
        /// Test the Add Method Pre Operation Cancel Argument.
        /// </summary>
        public void InMemoryRepositoryTest4()
        {
            int count = 0;
            InMemoryRepository<Int32> repo = new InMemoryRepository<Int32>();
            repo.BeforeInsertEvent += (sender, args) => args.Cancel = (Convert.ToInt32(args.Entry) < 5) ? true : false;
            repo.AfterInsertEvent += (sender, args) => count++;
            for (int i = 0; i < 10; i++)
                repo.Add(i);

            Assert.AreEqual(5, repo.Count());
            Assert.AreEqual(repo.Count(), count);
        }
        
        /// <summary>
        /// Test the Add Method Post Operation Event
        /// </summary>
        [TestMethod]
        public void InMemoryRepositoryTest3()
        {
            int i, count = 0;
            InMemoryRepository<Int32> repo = new InMemoryRepository<Int32>();
            repo.AfterInsertEvent += (sender, args) => count++;
            for (i = 0; i < 10; i++)
                repo.Add(i);

            Assert.AreEqual(10, i);
        }
        [TestMethod]
        /// <summary>
        /// Test the AddRange Method Pre Operation Cancel Argument.
        /// </summary>
        public void InMemoryRepositoryTest5()
        {
            int count = 0;
            InMemoryRepository<Int32> repo = new InMemoryRepository<Int32>();
            
            repo.AfterInsertEvent += (sender, args) => count++;
            repo.BeforeInsertEvent += (sender, args) => args.Cancel = (Convert.ToInt32(args.Entry) < 5) ? true : false;

            repo.AddRange(new List<Int32> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
 
            Assert.AreEqual(5, repo.Count());
            Assert.AreEqual(repo.Count(), count);
        }
    }
}
