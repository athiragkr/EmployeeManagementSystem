using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RestAPITesting;

namespace UnitTestEMSApi
{
    [TestClass]
    public class RegressionTests
    {
        [TestMethod]
        public void VerifyListofEmployees()
        {
            var empDetails = new EmpDetails();
            var response = empDetails.GetEmployees();
            Assert.AreEqual(1, response.Result.Meta.Pagination.Page);
            Assert.AreEqual("Peter Lau", response.Result.Data[0].Name);
        }

        [TestMethod]
        public void CreateNewEmployee()
        {
            var empDetails = new EmpDetails();
            var response = empDetails.CreateNewEmployee();
            Assert.AreEqual("riya", response.Result.data.name);
            Assert.AreEqual("riya@gmail.com", response.Result.data.email);
            Assert.AreEqual("active", response.Result.data.status);
            Assert.AreEqual("female", response.Result.data.gender);
        }
    }
}
