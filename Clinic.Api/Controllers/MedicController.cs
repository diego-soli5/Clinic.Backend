using AutoMapper;
using Clinic.Core.CustomEntities;
using Clinic.Core.DTOs.Medic;
using Clinic.Core.Interfaces.BusisnessServices;
using Clinic.Core.Interfaces.InfrastructureServices;
using Clinic.Core.QueryFilters;
using Clinic.Infrastructure.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MedicController : ControllerBase
    {
        private readonly IMedicService _medicService;
        private readonly IUriService _uriService;
        private readonly IMapper _mapper;

        public MedicController(IMedicService medicService,
                               IUriService uriService,
                               IMapper mapper)
        {
            _medicService = medicService;
            _uriService = uriService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllForList([FromQuery] MedicQueryFilter filters)
        {
            var pagedListMedics = await _medicService.GetAllAsync(filters);

            var metadata = Metadata.Create(filters, pagedListMedics, Url.RouteUrl(nameof(GetAllForList)), _uriService);

            var response = new OkResponse
            {
                Data = pagedListMedics.Select(med => _mapper.Map<MedicListDTO>(med)).ToList()
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("Specialties")]
        public IActionResult GetAllSpecialties()
        {
            var listMedSpecialties = _medicService.GetAllMedicalSpecialties();

            var response = new OkResponse
            {
                Data = listMedSpecialties.Select(ms => _mapper.Map<MedicalSpecialtyListDTO>(ms)).ToList()
            };

            return Ok(response);
        }
    }
}
