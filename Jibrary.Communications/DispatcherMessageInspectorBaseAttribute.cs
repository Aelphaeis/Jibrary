using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Jibrary.Communications
{
    public abstract class DispatcherMessageInspectorBaseAttribute : Attribute, IDispatchMessageInspector, IServiceBehavior 
    {
        public event EventHandler<AfterReceiveRequestEventArgs> AfterReceiveRequestEvent;
        public event EventHandler<BeforeSendReplyEventArgs> BeforeSendReplyEvent;

        #region Implementation of IServiceBehavior
        public virtual void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
            return;
        }

        public virtual void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher cDispatcher in serviceHostBase.ChannelDispatchers)
                foreach (EndpointDispatcher eDispatcher in cDispatcher.Endpoints)
                    eDispatcher.DispatchRuntime.MessageInspectors.Add(this);
        }

        public virtual void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            return;
        }
        #endregion
        #region Implementation of IDispatchMessageInspector
        public virtual object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            if (AfterReceiveRequestEvent != null)
                AfterReceiveRequestEvent(this, new AfterReceiveRequestEventArgs { Request = request, Channel = channel, InstanceContext = instanceContext });
            return null;
        }

        public virtual void BeforeSendReply(ref Message reply, object correlationState)
        {
            if (BeforeSendReplyEvent != null)
                BeforeSendReplyEvent(this, new BeforeSendReplyEventArgs { Reply = reply, CorrelationState = correlationState });
        }
        #endregion
    }
}
