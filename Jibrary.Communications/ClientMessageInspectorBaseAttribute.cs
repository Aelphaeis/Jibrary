using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel;
namespace Jibrary.Communications
{
    public class ClientMessageInspectorBase: IClientMessageInspector, IEndpointBehavior
    {
        public event EventHandler<AfterReceiveReplyEventArgs> AfterReceiveReplyEvent;
        public event EventHandler<BeforeSendRequestEventArgs> BeforeSendRequestEvent;

        /// <summary>
        /// Calls the AfterRecievedReplyEvent and passes its parameters as members in the AfterRecievedReplyEventArgs
        ///
        /// Note that the Message object is read only and if you wish to manipulate it you must overload this method.
        /// Also note that it is your responsibilty if you consume the message to repackage the message so it can be 
        /// reused.
        /// 
        /// Also Ensure that if you overload this method you call base.AfterRecieveReply with the appropriate parameters
        /// 
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="correlationState"></param>
        public virtual void AfterReceiveReply(ref Message reply, object correlationState)
        {
            if (AfterReceiveReplyEvent != null)
                AfterReceiveReplyEvent(this, new AfterReceiveReplyEventArgs { CorrelationState = correlationState, Reply = reply });
        }

        /// <summary>
        /// Calls the BeforeSendRequestEvent and passes its parameters as members in the BeforeSendRequestEventArgs
        /// 
        /// Note that the Message object is read only and if you wish to manipulate it you must overload this method.
        /// Also note that it is your responsibilty if you consume the message to repackage the message so it can be 
        /// reused.
        /// 
        /// Also Ensure that if you overload this method you call base.BeforeSendRequest with the appropriate parameters
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public virtual object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            if (BeforeSendRequestEvent != null)
                BeforeSendRequestEvent(this, new BeforeSendRequestEventArgs { Request = request, Channel = channel });
            return null;
        }


        public virtual void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            //There is no need to implement this at this time
            return;
        }

        public virtual void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(this);
            return;
        }

        public virtual void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            //There is no need to implement this at this time
            return;
        }

        public virtual void Validate(ServiceEndpoint endpoint)
        {
            //There is no need to implement this at this time
            return;
        }
    }
}
