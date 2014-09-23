using System;
using System.ServiceModel.Channels;

namespace Jibrary.Communications
{
    public class BeforeSendReplyEventArgs : EventArgs
    {
        public Message Reply { get; set; }
        public Object CorrelationState { get; set; }
    }
}
