using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
namespace Jibrary.Communications.Tests.Resources
{
    [ServiceBehavior]
    public class TestService : ITestService
    {
        [OperationBehavior]
        public String GetData(Int32 value)
        {
            return String.Format("You entered: {0}", value);
        }
    }

    [ServiceContract]
    public interface ITestService
    {
        [OperationContract]
        string GetData(int value);
    }
}
