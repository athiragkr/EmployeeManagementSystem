using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPITesting
{
    public partial class CreateEmployeeDTO
    {
        public long code { get; set; }
        public object meta { get; set; }
        public Data data { get; set; }
    }

    public partial class Data
    {
        public long id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string status { get; set; }
    }
}