using Clinic.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Core.QueryFilters
{
    public class EmployeeQueryFilter : BaseQueryFilter
    {
        public int? Identification { get; set; }
        public DateTime? HireDate { get; set; }
        public EmployeeRole? EmployeeRole { get; set; }
        public EmployeeStatus? EmployeeStatus { get; set; }
    }
}
