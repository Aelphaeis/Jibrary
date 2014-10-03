using System;
using System.ServiceModel;
using System.Threading;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jibrary.Data.Tests.Resources;
using Jibrary.Communications;
using System.Linq.Expressions;
using System.Collections.Generic;
using Jibrary.Data.Repositories;
namespace Jibrary.Data.Tests
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// This test is to ensure that a service host can be opened for wcf
        /// </summary>
        [TestMethod]
        public void ServiceHostTest()
        {
            string hostAddress = "http://localhost:24573/JibraryDataTests";
            using (ServiceHost host = new ServiceHost(typeof(TestService)))
            {
                host.AddServiceEndpoint(typeof(ITestService), new BasicHttpBinding(), hostAddress);
                host.BeginOpen(ar => host.EndOpen(ar), null);
                while (host.State != CommunicationState.Opened)
                    Thread.Yield();
                host.Close();
            }
        }
        /// <summary>
        /// This tests ensures that a client can connect to the wcf host.
        /// </summary>
        [TestMethod]
        public void ClientConnectionTest()
        {
            string hostAddress = "http://localhost:24573/JibraryDataTests";
            using (ChannelFactory<ITestService> factory = new ChannelFactory<ITestService>(new BasicHttpBinding(), hostAddress))
            using (ServiceHost host = new ServiceHost(typeof(TestService)))
            {
                host.AddServiceEndpoint(typeof(ITestService), new BasicHttpBinding(), hostAddress);
                host.BeginOpen(ar => host.EndOpen(ar), null);
                while (host.State != CommunicationState.Opened)
                    Thread.Yield();

                var channel = factory.CreateChannel();
                Assert.AreEqual(true, channel.DoWork());

                factory.Close();
                host.Close();
            }
        }

        /// <summary>
        /// Monitors to ensure that the client is Message Inspector's events are being called and display their result
        /// </summary>
        [TestMethod]
        public void ClientHostInspectionTest()
        {
            //init
            List<Int32> counter = new List<Int32>();
            MessageInspector inspector = new MessageInspector();
            String nl = String.Format("{0}{0}{0}", Environment.NewLine);
            String hostAddress = "http://localhost:24573/JibraryDataTests";

            using (ServiceHost host = new ServiceHost(typeof(TestService)))
            using (ChannelFactory<ITestService> factory = new ChannelFactory<ITestService>(new BasicHttpBinding(), hostAddress))
            {
                //Set up the message inspector
                inspector.AfterReceiveRequestEvent += (sender, args) => counter.Add(2);
                inspector.AfterReceiveReplyEvent += (sender, args) => counter.Add(4);
                inspector.BeforeSendRequestEvent += (sender, args) => counter.Add(1);
                inspector.BeforeSendReplyEvent += (sender, args) => counter.Add(3);

                inspector.AfterReceiveRequestEvent += (sender, args) => Console.WriteLine(args.Request.ToString() + nl);
                inspector.AfterReceiveReplyEvent += (sender, args) => Console.WriteLine(args.Reply.ToString() + nl);
                inspector.BeforeSendRequestEvent += (sender, args) => Console.WriteLine(args.Request.ToString() + nl);
                inspector.BeforeSendReplyEvent += (sender, args) => Console.WriteLine(args.Reply.ToString() + nl);

                //Attract inspector to host and cliemt
                inspector.ApplyToChannelFactory(factory);
                inspector.ApplyToServiceHost(host);

                //Configure and open service host
                host.AddServiceEndpoint(typeof(ITestService), new BasicHttpBinding(), hostAddress);
                host.BeginOpen(ar => host.EndOpen(ar), null);
                while (host.State != CommunicationState.Opened)
                    Thread.Yield();

                //Open Client and make call
                var channel = factory.CreateChannel();
                Assert.AreEqual(true, channel.DoWork());

                //Make sure each inspector was called
                //in the order we think it was called
                Assert.AreEqual(4, counter.Count);
                Assert.AreEqual(1, counter[0]);
                Assert.AreEqual(2, counter[1]);
                Assert.AreEqual(3, counter[2]);
                Assert.AreEqual(4, counter[3]);

                //clean up the test.
                factory.Close();
                host.Close();
            }
        }

        [TestMethod]
        public void IQueryableServiceTest()
        {
            //init
            MessageInspector inspector = new MessageInspector();
            String nl = String.Format("{0}{0}{0}", Environment.NewLine);
            String hostAddress = "http://localhost:24573/JibraryDataTests";

            using (ServiceHost host = new ServiceHost(typeof(TestService)))
            using (ChannelFactory<ITestService> factory = new ChannelFactory<ITestService>(new BasicHttpBinding(), hostAddress))
            {
                //Set up the message inspector
                inspector.AfterReceiveReplyEvent += (sender, args) => Console.WriteLine(args.Reply.ToString() + nl);
                inspector.BeforeSendRequestEvent += (sender, args) => Console.WriteLine(args.Request.ToString() + nl);

                //Attract inspector to host and cliemt
                inspector.ApplyToChannelFactory(factory);
                inspector.ApplyToServiceHost(host);

                //Configure and open service host
                host.AddServiceEndpoint(typeof(ITestService), new BasicHttpBinding(), hostAddress);
                host.BeginOpen(ar => host.EndOpen(ar), null);
                while (host.State != CommunicationState.Opened)
                    Thread.Yield();

                //Open Client and make call
                var channel = factory.CreateChannel();

                var result = channel.QueryList()
                    .Cast<Int32>()
                    .Where(p => p > 3)
                    .ToList()
                    .Count();

                Assert.AreEqual(3, result);

                //clean up the test.
                factory.Close();
                host.Close();
            }
        }

        [TestMethod]
        public void IQueryableServiceTest2()
        {
            MessageInspector inspector = new MessageInspector();
            String nl = String.Format("{0}{0}{0}", Environment.NewLine);
            String hostAddress = "http://localhost:24573/JibraryDataTests";

            using (ServiceHost host = new ServiceHost(typeof(TestService)))
            using (ChannelFactory<ITestService> factory = new ChannelFactory<ITestService>(new BasicHttpBinding(), hostAddress))
            {
                //Set up the message inspector
                inspector.AfterReceiveReplyEvent += (sender, args) => Console.WriteLine(args.Reply.ToString() + nl);
                inspector.BeforeSendRequestEvent += (sender, args) => Console.WriteLine(args.Request.ToString() + nl);

                //Attract inspector to host and cliemt
                inspector.ApplyToChannelFactory(factory);
                inspector.ApplyToServiceHost(host);

                //Configure and open service host
                host.AddServiceEndpoint(typeof(ITestService), new BasicHttpBinding(), hostAddress);
                host.BeginOpen(ar => host.EndOpen(ar), null);
                while (host.State != CommunicationState.Opened)
                    Thread.Yield();

                //Open Client and make call
                var channel = factory.CreateChannel();

                List<Int32> list = new List<Int32>();
                IQueryable<Int32> qList = list.AsQueryable();
                qList.Where(p => p > 3);
                var result = channel.QueryList()
                    .Cast<Int32>()
                    .Where(p => p > 3)
                    .ToList()
                    .Count();

                Assert.AreEqual(3, result);

                //clean up the test.
                factory.Close();
                host.Close();
            }
        }

        [TestMethod]
        public void RepoQueryTest1()
        {
            InMemoryRepository<Int32> Repo = new InMemoryRepository<Int32>();
            Repo.AddRange(new Int32[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            var queryable = Repo.AsQueryable();
            //Assert.AreEqual(10, queryable.Count());
            Assert.AreEqual(3, queryable.Where(p => p <= 3).Count());

        }
    }
}
