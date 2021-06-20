using Clinic.Core.CustomEntities;
using Clinic.Core.Entities;
using Clinic.Core.Interfaces.InfrastructureServices;
using Clinic.Core.QueryFilters;
using System;

namespace Clinic.Infrastructure.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetEmployeePaginationUri(EmployeeQueryFilter filter, PagedList<Employee> pagedList, string actionUrl, bool IsNextPage = true)
        {
            string uri = $"{_baseUri}{actionUrl}";

            uri += "?";

            if (filter.Identification.HasValue)
            {
                uri += $"{nameof(filter.Identification)}={filter.Identification.Value}&";
            }

            if (filter.HireDate.HasValue)
            {
                uri += $"{nameof(filter.HireDate)}={filter.HireDate.Value}&";
            }

            if (filter.EmployeeStatus.HasValue)
            {
                uri += $"{nameof(filter.EmployeeStatus)}={filter.EmployeeStatus.Value}&";
            }

            if (filter.EmployeeRole.HasValue)
            {
                uri += $"{nameof(filter.EmployeeRole)}={filter.EmployeeRole.Value}&";
            }

            uri += $"{nameof(filter.PageSize)}={filter.PageSize.Value}&";

            if (IsNextPage)
                uri += $"{nameof(filter.PageNumber)}={pagedList.NextPageNumber}";
            else
                uri += $"{nameof(filter.PageNumber)}={pagedList.PreviousPageNumber}";

            return new Uri(uri);
        }
    }
}
