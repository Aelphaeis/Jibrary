using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Jibrary.Communications
{
    public class AfterReceiveRequestEventArgs : EventArgs
    {
        public Message Request { get; set; }
        public IClientChannel Channel { get; set; }
        public InstanceContext InstanceContext { get; set; }

    }
}
