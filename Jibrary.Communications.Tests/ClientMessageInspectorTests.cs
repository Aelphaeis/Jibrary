using System;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jibrary.Communications.Tests.Resources;
using System.Threading;
using Jibrary.Communications;
namespace Jibrary.Communications.Tests
{
    [TestClass]
    public class ClientMessageInspectorTests
    {
        /// <summary>
        /// Test to ensure that Client and Server can connect with each other
        /// </summary>
        [TestMethod]
        [Timeout(20000)]
        public void ClientMessageInspectorTest1()
        {
            //The host location
            var hostLocation = new EndpointAddress("http://localhost:24543/TestService");
            
            //Create a client factory and a service host
            using(var clientFactory = new ChannelFactory<ITestService>(new BasicHttpBinding(), hostLocation))
            using(var testServiceHost = new ServiceHost(typeof(TestService)))
            {
                //Expose endpoint and open the service
                testServiceHost.AddServiceEndpoint(typeof(ITestService), new BasicHttpBinding(), hostLocation.Uri);
                testServiceHost.BeginOpen(ar => testServiceHost.EndOpen(ar), null);

                //wait until the service is open
                while (testServiceHost.State != CommunicationState.Opened)
                    Thread.Yield();

                //Create a channel (a.k.a.) client.
                var client = clientFactory.CreateChannel();
                Assert.AreEqual(String.Format("You entered: {0}", 5), client.GetData(5));
            }
        }

        [TestMethod]
        [Timeout(20000)]
        public void ClientMessageInspectorTest2()
        {
            //The host location
            var hostLocation = new EndpointAddress("http://localhost:24543/TestService");

            //Create a client factory and a service host
            using (var clientFactory = new ChannelFactory<ITestService>(new BasicHttpBinding(), hostLocation))
            using (var testServiceHost = new ServiceHost(typeof(TestService)))
            {
                //Expose endpoint and open the service
                testServiceHost.AddServiceEndpoint(typeof(ITestService), new BasicHttpBinding(), hostLocation.Uri);
                testServiceHost.BeginOpen(ar => testServiceHost.EndOpen(ar), null);

                //wait until the service is open
                while (testServiceHost.State != CommunicationState.Opened)
                    Thread.Yield();

                //Create an inspector and subscribe to it, we know it is hit
                //when count is not equal to 0;
                int count = 0;
                var inspector = new ClientInspector();
                inspector.BeforeSendRequestEvent += (sender, args) => count++;
                inspector.BeforeSendRequestEvent += (sender, args) => Console.WriteLine(args.Request.ToString());
                inspector.AfterReceiveReplyEvent += (sender, args) => Console.WriteLine(args.Reply.ToString());

                //Add the inspection mechanism to the clientFactory
                inspector.ApplyBehaviorToChannelFactory(clientFactory);

                //Create a channel (a.k.a.) client.
                var client = clientFactory.CreateChannel();

                //Make sure you're getting the correct output
                Assert.AreEqual(String.Format("You entered: {0}", 5), client.GetData(5));
                //If the inspector.AfterReceiveReplyEvent was called then we will see count != 0 here
                Assert.AreNotEqual(0, count);
            }
        }
    }
}
