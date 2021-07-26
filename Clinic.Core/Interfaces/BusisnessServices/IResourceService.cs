using System.IO;
using System.Threading.Tasks;

namespace Clinic.Core.Interfaces.BusisnessServices
{
    public interface IResourceService
    {
        Task<(Stream, string)> GetEntityImageAsync(string name);
    }
}
