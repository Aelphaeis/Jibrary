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
    public class ClientMessageInspectorBaseAttribute : Attribute, IClientMessageInspector
    {
        public event EventHandler<AfterReceiveReplyEventArgs> AfterReceiveReplyEvent;
        public event EventHandler<BeforeSendRequestEventArgs> BeforeSendRequestEvent;

        public virtual void AfterReceiveReply(ref Message reply, object correlationState)
        {
            if (AfterReceiveReplyEvent != null)
                AfterReceiveReplyEvent(this, new AfterReceiveReplyEventArgs { CorrelationState = correlationState, Reply = reply });
        }

        public virtual object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            if (BeforeSendRequestEvent != null)
                BeforeSendRequestEvent(this, new BeforeSendRequestEventArgs { Request = request, Channel = channel });
            return null;
        }
    }
}
