using Clinic.Core.Entities;
using Clinic.Core.Interfaces.InfrastructureServices;
using Clinic.Core.QueryFilters;

namespace Clinic.Core.CustomEntities
{
    public class Metadata
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public int? NextPageNumber { get; set; }
        public int? PreviousPageNumber { get; set; }
        public string NextPageUrl { get; set; }
        public string PreviousPageUrl { get; set; }

        public static Metadata Create<TEntity>(BaseQueryFilter filters, PagedList<TEntity> pagedList, string actionUrl, IUriService uriService)
            where TEntity : BaseEntity
        {
            return new Metadata
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNextPage = pagedList.HasNextPage,
                HasPreviousPage = pagedList.HasPreviousPage,
                NextPageNumber = pagedList.NextPageNumber,
                PreviousPageNumber = pagedList.PreviousPageNumber,
                NextPageUrl = uriService.GetPaginationUri(filters, pagedList, actionUrl, true)?.ToString(),
                PreviousPageUrl = uriService.GetPaginationUri(filters, pagedList, actionUrl, false)?.ToString()
            };
        }
    }
}
