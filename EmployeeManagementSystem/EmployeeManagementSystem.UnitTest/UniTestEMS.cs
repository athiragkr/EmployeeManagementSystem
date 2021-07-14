using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementSystem;

namespace EmployeeManagementSystem.UnitTest
{
    [TestClass]
    public class UniTestEMS
    {
        EmployeeManagementSystem.Helper.RestHelper restHelper = new Helper.RestHelper();
        [TestMethod]
        public void TestGetEmployees()
        {
            restHelper.GetAllEmployees ()
        }

    }
}
