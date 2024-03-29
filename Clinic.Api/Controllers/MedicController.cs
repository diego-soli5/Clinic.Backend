﻿using AutoMapper;
using Clinic.Core.CustomEntities;
using Clinic.Core.DTOs.Medic;
using Clinic.Core.Entities;
using Clinic.Core.Interfaces.BusisnessServices;
using Clinic.Core.Interfaces.InfrastructureServices;
using Clinic.Core.QueryFilters;
using Clinic.Infrastructure.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public IActionResult GetAllForList([FromQuery] MedicQueryFilter filters)
        {
            OkResponse response;

            if (filters.PendingUpdate)
            {
                var medPendingList = _medicService.GetAllPendingForUpdate();

                response = new OkResponse
                {
                    Data = medPendingList.Select(med => _mapper.Map<MedicDisplayPendingForUpdateDTO>(med))
                };

                return Ok(response);
            }

            var pagedListMedics = _medicService.GetAllForList(filters);

            var metadata = Metadata.Create(filters, pagedListMedics, Url.RouteUrl(nameof(GetAllForList)), _uriService);

            response = new OkResponse
            {
                Data = pagedListMedics.Select(med => _mapper.Map<MedicListDTO>(med)).ToList()
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicForEdit(int id)
        {
            var med = await _medicService.GetMedicForEdit(id);

            var response = new OkResponse()
            {
                Data = _mapper.Map<MedicUpdateDTO>(med)
            };

            return Ok(response);
        }

        [HttpPatch("Edit/{id}")]
        public async Task<IActionResult> EditMedic([FromBody] MedicUpdateDTO model, int id)
        {
            Medic entity = new Medic
            {
                Id = id,
                IdConsultingRoom = model.IdConsultingRoom,
                IdMedicalSpecialty = model.IdMedicalSpecialty
            };

            await _medicService.Edit(entity);

            return NoContent();
        }

        [HttpGet("Pending/{idEmployee}")]
        public async Task<IActionResult> GetMedicPendingForUpdate(int idEmployee)
        {
            var emp = await _medicService.GetMedicPendingForUpdate(idEmployee);

            var response = new OkResponse()
            {
                Data = _mapper.Map<MedicPendingUpdateDTO>(emp)
            };

            return Ok(response);
        }

        [HttpPatch("{idEmployee}")]
        public async Task<IActionResult> UpdatePendingMedic([FromBody] MedicPendingUpdateDTO model, int idEmployee)
        {
            var medic = new Medic
            {
                IdEmployee = idEmployee,
                IdConsultingRoom = model.IdConsultingRoom,
                IdMedicalSpecialty = model.IdMedicalSpecialty
            };

            await _medicService.UpdatePendingMedic(medic);

            return NoContent();
        }
    }
}
