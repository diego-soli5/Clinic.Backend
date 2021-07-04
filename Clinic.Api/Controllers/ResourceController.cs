using Clinic.Core.Interfaces.ExternalServices;
using Clinic.Infrastructure.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Clinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IAzureBlobFileService _fileService;

        public ResourceController(IAzureBlobFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFile(string name)
        {
            if (name == null)
                return NotFound(new NotFoundResponse());

            var file = await _fileService.GetBlobAsync(name);

            return File(file.Item1, file.Item2);
        }
    }
}
