using Clinic.Core.CustomEntities;
using Clinic.Core.Entities;
using Clinic.Core.QueryFilters;
using System;

namespace Clinic.Core.Interfaces.InfrastructureServices
{
    public interface IUriService
    {
        Uri GetPaginationUri<TEntity, TQuery>(TQuery filter, PagedList<TEntity> pagedList, string actionUrl, bool IsNextPage = true)
            where TEntity : BaseEntity where TQuery : BaseQueryFilter;
    }
}
