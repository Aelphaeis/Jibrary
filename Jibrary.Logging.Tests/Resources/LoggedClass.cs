using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jibrary.Logging;

namespace Jibrary.Logging.Tests.Resources
{
    public class LoggedClass
    {
        [Logged]
        public string DoWork(){
            return "Hello World";
        }
    }
}
