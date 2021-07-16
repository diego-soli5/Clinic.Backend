using Clinic.Core.Enumerations;
using System;

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
