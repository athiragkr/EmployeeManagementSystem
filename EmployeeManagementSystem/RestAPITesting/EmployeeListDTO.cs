using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPITesting
{
    public partial class EmployeeListDTO
    {
        public long Code { get; set; }
        public Meta Meta { get; set; }
        public List<Datum> Data { get; set; }
    }

    public partial class Datum
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public Status Status { get; set; }
    }

    public partial class Meta
    {
        public Pagination Pagination { get; set; }
    }

    public partial class Pagination
    {
        public long Total { get; set; }
        public long Pages { get; set; }
        public long Page { get; set; }
        public long Limit { get; set; }
    }

    public enum Gender { Female, Male };

    public enum Status { Active, Inactive };
}

