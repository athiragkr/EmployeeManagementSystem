using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Helper
{
    interface IRestHelper
    {
        Task<string> GetAllEmployees();
        Task<string> Post(Dictionary<string, string> empData);
        Task<string> Delete(Datum empData);
        Task<string> Put(Datum empData);
        Task<string> GetByPageNo(int pageNo);
    }
}
