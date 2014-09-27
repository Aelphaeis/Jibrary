using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
namespace Jibrary.Data.Tests.Resources
{
    [ServiceContract]
    public interface ITestService
    {
        [OperationContract]
        IQueryable QueryList();

        [OperationContract]
        Object Query(IQueryable queryable);

        [OperationContract]
        Boolean DoWork();

    }
}
