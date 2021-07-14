using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementSystem.Helper;
using Newtonsoft.Json;

namespace RestAPITesting
{
    public class EmpDetails
    {
        RestHelper restHelper = new RestHelper();
        public async Task<EmployeeListDTO> GetEmployees()
        {
            var response = await restHelper.GetAllEmployees();
            var empDetails = JsonConvert.DeserializeObject<EmployeeListDTO>(response);
            return empDetails;
        }

        public async Task<CreateEmployeeDTO> CreateNewEmployee()
        {
            var inputData = new Dictionary<string, string>
            {
                { "name", "Riya"},
                { "email", "riya@gmail.com"},
                { "gender", "female"},
                { "status", "active"}
            };
            var response = await restHelper.Post(inputData);
            CreateEmployeeDTO content = restHelper.GetContent<CreateEmployeeDTO>(response);
            return content;
        }
    }
}
