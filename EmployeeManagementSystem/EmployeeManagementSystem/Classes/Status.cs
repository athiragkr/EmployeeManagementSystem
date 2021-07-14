using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Classes
{
    public class Status
    {
        public string EStatus { get; set; }
        public Status(string _status)
        {
            EStatus = _status;
        }
    }
}
