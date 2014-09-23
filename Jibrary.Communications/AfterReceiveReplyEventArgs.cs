using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Channels;

namespace Jibrary.Communications
{
    public class AfterReceiveReplyEventArgs : EventArgs
    {
        public virtual Message Reply { get; set; }
        public virtual Object CorrelationState { get; set; }
    }
}
