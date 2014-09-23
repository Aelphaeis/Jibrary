using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Channels;
using System.ServiceModel;
namespace Jibrary.Communications
{
    public class BeforeSendRequestEventArgs : EventArgs
    {
        public virtual Message Request { get; set; }
        public virtual IClientChannel Channel { get; set; }
    }
}
