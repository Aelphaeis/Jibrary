using System;
using System.ServiceModel.Channels;

namespace Jibrary.Communications
{
    public class BeforeSendReplyEventArgs : EventArgs
    {
        public virtual Message Reply { get; set; }
        public virtual Object CorrelationState { get; set; }
    }
}
