using AutoMapper;
using Clinic.Core.CustomEntities;
using Clinic.Core.DTOs.Employee;
using Clinic.Core.Interfaces.BusisnessServices;
using Clinic.Core.Interfaces.InfrastructureServices;
using Clinic.Core.QueryFilters;
using Clinic.Infrastructure.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace Clinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public EmployeeController(IEmployeeService employeeService, IUriService uriService, IMapper mapper)
        {
            _employeeService = employeeService;
            _uriService = uriService;
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetAll))]
        public IActionResult GetAll([FromQuery] EmployeeQueryFilter filters)
        {
            var pagedList = _employeeService.GetAll(filters);

            var metadata = new Metadata
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNextPage = pagedList.HasNextPage,
                HasPreviousPage = pagedList.HasPreviousPage,
                NextPageNumber = pagedList.NextPageNumber,
                PreviousPageNumber = pagedList.PreviousPageNumber,
                NextPageUrl = pagedList.HasNextPage ? _uriService.GetEmployeePaginationUri(filters, pagedList, Url.RouteUrl(nameof(GetAll)), true).ToString() : null,
                PreviousPageUrl = pagedList.HasPreviousPage ? _uriService.GetEmployeePaginationUri(filters, pagedList, Url.RouteUrl(nameof(GetAll)), false).ToString() : null
            };

            var response = new OkResponse
            {
                Data = pagedList.Select(x => _mapper.Map<EmployeeDTO>(x)).ToList()
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(new { metadata, response });
        }
    }
}
