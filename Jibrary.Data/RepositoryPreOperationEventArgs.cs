using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jibrary.Data
{
    public class RepositoryPreOperationEventArgs
    {
        public Object Entry { get; set; }
        public Boolean Cancel { get; set; }
    }
}
