﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
namespace Jibrary.Data.Tests.Resources
{
    [ServiceBehavior]
    public class TestService : ITestService
    {
        List<Int32> MyList { get; set; }

        public TestService()
        {
            MyList = new List<Int32> { 1, 2, 3, 4, 5, 6 };
        }

        public IQueryable QueryList()
        {
            return MyList.AsQueryable();
        }

        public bool DoWork()
        {
            return true;
        }


        public object Query(IQueryable queryable)
        {
            return null;
        }
    }
}
