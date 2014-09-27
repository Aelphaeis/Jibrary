using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jibrary.Communications;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.Collections.ObjectModel;
namespace Jibrary.Data.Tests.Resources
{
    /// <summary>
    /// This is a test class for Jibrary.Data.Tests which allows for inspection of ServiceHost and IClientChannel;
    /// </summary>
    public class MessageInspector : IClientMessageInspector, IDispatchMessageInspector, IEndpointBehavior, IServiceBehavior
    {
        //This is the order in which things generally happen
        public event EventHandler<BeforeSendRequestEventArgs> BeforeSendRequestEvent;
        public event EventHandler<AfterReceiveRequestEventArgs> AfterReceiveRequestEvent;
        public event EventHandler<BeforeSendReplyEventArgs> BeforeSendReplyEvent;
        public event EventHandler<AfterReceiveReplyEventArgs> AfterReceiveReplyEvent;

        /* C# does not allow for multiple inheritence so we declare implement
         * IClientMessageInspector and IDispatchMesageInspecter and create internal 
         * classes that inherit from abstract implementations of those interfaces
         *
         * Lastly, we do make versions of those instances internally to manage functionality
         * that we will later use
         */

        /// <summary>
        /// Allows us to listen in on the server
        /// </summary>
        DispatcherInspector di;

        /// <summary>
        /// Allows us to listen in on the client 
        /// </summary>
        ClientInspector ci;

        public MessageInspector()
        {
            di = new DispatcherInspector();
            ci = new ClientInspector();

            //Pass the events from the internal classes on
            di.AfterReceiveRequestEvent += di_AfterReceiveRequestEvent;
            di.BeforeSendReplyEvent += di_BeforeSendReplyEvent;
            ci.AfterReceiveReplyEvent += ci_AfterReceiveReplyEvent;
            ci.BeforeSendRequestEvent += ci_BeforeSendRequestEvent;
        }

        public virtual void ApplyToServiceHost(ServiceHost host)
        {
            host.Description.Behaviors.Add(this);
            return;
        }

        public virtual void ApplyToChannelFactory(ChannelFactory factory)
        {
            factory.Endpoint.EndpointBehaviors.Add(this);
            return;
        }

        public virtual void AfterReceiveReply(ref Message reply, object correlationState)
        {
            ci.AfterReceiveReply(ref reply, correlationState);
            return;
        }

        public virtual object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            return ci.BeforeSendRequest(ref request, channel);
        }

        public virtual object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            return di.AfterReceiveRequest(ref request, channel, instanceContext);
        }

        public virtual void BeforeSendReply(ref Message reply, object correlationState)
        {
            di.BeforeSendReply(ref reply, correlationState);
            return;
        }

        public virtual void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            ci.AddBindingParameters(endpoint, bindingParameters);
            return;
        }

        public virtual void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            ci.ApplyClientBehavior(endpoint, clientRuntime);
            return;
        }

        public virtual void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            ci.ApplyDispatchBehavior(endpoint, endpointDispatcher);
            return;
        }

        public virtual void Validate(ServiceEndpoint endpoint)
        {
            ci.Validate(endpoint);
            return;
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
            di.AddBindingParameters(serviceDescription, serviceHostBase, endpoints, bindingParameters);
            return;
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            di.ApplyDispatchBehavior(serviceDescription, serviceHostBase);
            return;
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            di.Validate(serviceDescription, serviceHostBase);
            return;
        }

        #region Event Handlers
        void ci_BeforeSendRequestEvent(object sender, BeforeSendRequestEventArgs e)
        {
            if (BeforeSendRequestEvent != null)
                BeforeSendRequestEvent(sender, e);
        }
        void ci_AfterReceiveReplyEvent(object sender, AfterReceiveReplyEventArgs e)
        {
            if (AfterReceiveReplyEvent != null)
                AfterReceiveReplyEvent(sender, e);
        }
        void di_BeforeSendReplyEvent(object sender, BeforeSendReplyEventArgs e)
        {
            if (BeforeSendReplyEvent != null)
                BeforeSendReplyEvent(sender, e);
        }
        void di_AfterReceiveRequestEvent(object sender, AfterReceiveRequestEventArgs e)
        {
            if (AfterReceiveRequestEvent != null)
                AfterReceiveRequestEvent(sender, e);
        }
        #endregion
        #region Internal Classes
        class ClientInspector : ClientMessageInspectorBase {  }
        class DispatcherInspector : DispatcherMessageInspectorBaseAttribute { }
        #endregion

    }
}
