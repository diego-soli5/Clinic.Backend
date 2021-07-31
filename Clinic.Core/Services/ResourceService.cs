using Clinic.Core.CustomExceptions;
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

        public ResourceService(IAzureBlobFileService fileService
                              ,IUnitOfWork unitOfWork)
        {
            _fileService = fileService;
            _unitOfWork = unitOfWork;
        }

        public async Task<(Stream, string)> GetEntityImageAsync(string name)
        {
            return await _fileService.GetBlobAsync(name);
        }

        public async Task<(Stream, string)> GetEntityImageById(int id)
        {
            var entity = await _unitOfWork.Person.GetByIdAsync(id);

            if (entity == null)
                throw new NotFoundException();

            return await _fileService.GetBlobAsync(entity.ImageName);
        }
    }
}
