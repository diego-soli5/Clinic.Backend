using Clinic.Core.CustomEntities;
using Clinic.Core.Entities;
using Clinic.Core.QueryFilters;
using System;

namespace Clinic.Core.Interfaces.InfrastructureServices
{
    public interface IUriService
    {
        Uri GetEmployeePaginationUri(EmployeeQueryFilter filter, PagedList<Employee> pagedList, string actionUrl, bool IsNextPage = true);
    }
}
