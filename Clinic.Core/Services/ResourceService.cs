using Clinic.Core.CustomExceptions;
using Clinic.Core.Entities;
using Clinic.Core.Interfaces.BusisnessServices;
using Clinic.Core.Interfaces.ExternalServices;
using Clinic.Core.Interfaces.Repositories;
using System.IO;
using System.Threading.Tasks;

namespace Clinic.Core.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IAzureBlobFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;

        public ResourceService(IAzureBlobFileService fileService,
                               IUnitOfWork unitOfWork)
        {
            _fileService = fileService;
            _unitOfWork = unitOfWork;
        }

        public async Task<(Stream, string)> GetEntityImageAsync(int entityId)
        {
            var oEntity = await _unitOfWork.Person.GetByIdAsync(entityId);

            return await _fileService.GetBlobAsync(oEntity?.ImageName);
        }
    }
}
