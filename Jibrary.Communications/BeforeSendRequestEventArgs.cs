using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
namespace Jibrary.Communications
{
    public class BeforeSendRequestEventArgs : EventArgs
    {
        public virtual Message Request { get; set; }
        public virtual IClientChannel Channel { get; set; }
    }
}
