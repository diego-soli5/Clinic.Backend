using Clinic.Core.Interfaces.BusisnessServices;
using Clinic.Infrastructure.Responses;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> GetImage(int id)
        {
            var file = await _resourceService.GetEntityImageAsync(id);

            if (file.Item1 == null)
                return NotFound(new NotFoundResponse());

            return File(file.Item1, file.Item2);
        }
    }
}
