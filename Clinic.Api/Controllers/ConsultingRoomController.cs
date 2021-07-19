using AutoMapper;
using Clinic.Core.DTOs.ConsultingRoom;
using Clinic.Core.Interfaces.BusisnessServices;
using Clinic.Infrastructure.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Clinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConsultingRoomController : ControllerBase
    {
        private readonly IConsultingRoomService _service;
        private readonly IMapper _mapper;

        public ConsultingRoomController(IConsultingRoomService service,
                                          IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetAllConsultingRooms))]
        public IActionResult GetAllConsultingRooms()
        {
            var listConsultingRoom = _service.GetAll();

            var response = new OkResponse
            {
                Data = listConsultingRoom.Select(room => _mapper.Map<ConsultingRoomDTO>(room)).ToList()
            };

            return Ok(response);
        }
    }
}
