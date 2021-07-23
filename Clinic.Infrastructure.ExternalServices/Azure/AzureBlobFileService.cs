using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Clinic.Core.Interfaces.ExternalServices;
using Clinic.Core.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.ExternalServices.Azure
{
    public class AzureBlobFileService : IAzureBlobFileService
    {
        private readonly BlobServiceClient _blobService;
        private readonly AzureBlobServiceOptions _options;
        private readonly BlobContainerClient _container;

        public AzureBlobFileService(BlobServiceClient blobService, IOptions<AzureBlobServiceOptions> options)
        {
            _blobService = blobService;
            _options = options.Value;
            _container = _blobService.GetBlobContainerClient(_options.ContainerName);
        }

        public async Task<(Stream, string)> GetBlobAsync(string fileName)
        {
            var blobClient = _container.GetBlobClient(fileName);

            if (await blobClient.ExistsAsync())
            {
                var blobDownloadInfo = await blobClient.DownloadAsync();

                return (blobDownloadInfo.Value.Content, blobDownloadInfo.Value.ContentType);
            }

            return (null, null);
        }

        public async Task<string> CreateBlobAsync(IFormFile file)
        {
            string newFileName = $"Emp_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            var blobClient = _container.GetBlobClient(newFileName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders
                {
                    ContentType = file.ContentType
                });
            }

            return newFileName;
        }

        public async Task<bool> DeleteBlobAsync(string fileName)
        {
            var blobClient = _container.GetBlobClient(fileName ?? "");

            return await blobClient.DeleteIfExistsAsync();
        }
    }
}
