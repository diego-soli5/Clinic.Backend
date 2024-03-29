﻿using Clinic.Core.Interfaces.BusisnessServices;
using Clinic.Infrastructure.Responses;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceService _resourceService;

        public ResourceController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEntityImgById(int id)
        {
            var img = await _resourceService.GetEntityImageById(id);

            if (img.Item1 == null)
                return NotFound(new NotFoundResponse());

            return File(img.Item1, img.Item2);
        }

        [HttpGet]
        public async Task<IActionResult> GetResource(string n, string type)
        {
            (Stream, string) file = (null, null);

            if (!ResourceTypes.Any(rt => rt == type))
            {
                return BadRequest(new BadRequestResponse());
            }

            if (type == "img")
            {
                file = await _resourceService.GetEntityImageAsync(n);
            }

            if (file.Item1 == null)
                return NotFound(new NotFoundResponse());

            return File(file.Item1, file.Item2);
        }

        private string[] ResourceTypes
        {
            get
            {
                return new[] {

                    "img",
                };
            }
        }
    }
}
