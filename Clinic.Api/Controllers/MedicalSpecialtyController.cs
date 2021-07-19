using AutoMapper;
using Clinic.Core.DTOs.Medic;
using Clinic.Core.Interfaces.BusisnessServices;
using Clinic.Infrastructure.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MedicalSpecialtyController : ControllerBase
    {
        private readonly IMedicalSpecialtyService _service;
        private readonly IMapper _mapper;

        public MedicalSpecialtyController(IMedicalSpecialtyService service,
                                          IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetAllMedicalSpecialties))]
        public IActionResult GetAllMedicalSpecialties()
        {
            var listMedSpecialties = _service.GetAll();

            var response = new OkResponse
            {
                Data = listMedSpecialties.Select(ms => _mapper.Map<MedicalSpecialtyListDTO>(ms)).ToList()
            };

            return Ok(response);
        }
    }
}
