using AutoMapper;
using Clinic.Core.CustomEntities;
using Clinic.Core.DTOs.Employee;
using Clinic.Core.Entities;
using Clinic.Core.Interfaces.BusisnessServices;
using Clinic.Core.Interfaces.InfrastructureServices;
using Clinic.Core.QueryFilters;
using Clinic.Infrastructure.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

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

            var metadata = Metadata.Create(filters, pagedList, Url.RouteUrl(nameof(GetAll)), _uriService);

            var response = new OkResponse
            {
                Data = pagedList.Select(x => _mapper.Map<EmployeeListDTO>(x)).ToList()
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}", Name = nameof(GetById))]
        public async Task<IActionResult> GetById(int id)
        {
            var oEmployee = await _employeeService.GetByIdAsync(id);

            if(oEmployee == null)
            {
                return NotFound(new NotFoundResponse());
            }

            OkResponse response = new()
            {
                Data = _mapper.Map<EmployeeDTO>(oEmployee)
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateDTO model)
        {
            var oEmployee = _mapper.Map<Employee>(model);

            await _employeeService.Create(oEmployee);

            return CreatedAtRoute(nameof(GetById), new { oEmployee.Id }, null);
        }
    }
}
