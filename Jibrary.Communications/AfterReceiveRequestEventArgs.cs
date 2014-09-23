using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Jibrary.Communications
{
    public class AfterReceiveRequestEventArgs : EventArgs
    {
        public virtual Message Request { get; set; }
        public virtual IClientChannel Channel { get; set; }
        public virtual InstanceContext InstanceContext { get; set; }

    }
}
