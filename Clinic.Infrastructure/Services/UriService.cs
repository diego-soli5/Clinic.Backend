using Clinic.Core.CustomEntities;
using Clinic.Core.Entities;
using Clinic.Core.Interfaces.InfrastructureServices;
using Clinic.Core.QueryFilters;
using System;
using System.Reflection;
using System.Linq;

namespace Clinic.Infrastructure.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetPaginationUri<TEntity, TQueryFilter>(TQueryFilter filter, PagedList<TEntity> pagedList, string actionUrl, bool IsNextPage = true)
            where TEntity : BaseEntity where TQueryFilter : BaseQueryFilter
        {
            if (IsNextPage)
            {
                if (!pagedList.HasNextPage)
                    return null;
            }
            else
            {
                if (!pagedList.HasPreviousPage)
                    return null;
            }

            string uri = $"{_baseUri}{actionUrl}?";

            PropertyInfo[] properties = filter.GetType().GetProperties();

            properties.ToList().ForEach(prop =>
            {
                if (prop.Name != nameof(filter.PageNumber))
                {
                    var value = prop.GetValue(filter);

                    if (value != null)
                        uri += $"{prop.Name}={value}&";
                }
            });

            if (IsNextPage)
                uri += $"{nameof(filter.PageNumber)}={pagedList.NextPageNumber}";
            else
                uri += $"{nameof(filter.PageNumber)}={pagedList.PreviousPageNumber}";

            return new Uri(uri);
        }
    }
}
