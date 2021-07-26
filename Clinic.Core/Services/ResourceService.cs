using Clinic.Core.CustomExceptions;
using Clinic.Core.Entities;
using Clinic.Core.Interfaces.BusisnessServices;
using Clinic.Core.Interfaces.ExternalServices;
using System.IO;
using System.Threading.Tasks;

namespace Clinic.Core.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IAzureBlobFileService _fileService;

        public ResourceService(IAzureBlobFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<(Stream, string)> GetEntityImageAsync(string name)
        {
            return await _fileService.GetBlobAsync(name);
        }
    }
}
