using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Clinic.Core.Interfaces.InfrastructureServices
{
    public interface IAzureBlobFileService
    {
        Task<string> CreateBlobAsync(IFormFile file);
        Task<(Stream, string)> GetBlobAsync(string fileName);
        Task<bool> DeleteBlobAsync(string fileName);
    }
}
