using Clinic.Core.Entities;
using System.Threading.Tasks;

namespace Clinic.Core.Interfaces.Repositories
{
    public interface IAppUserRepository : IGenericRepository<AppUser>
    {
        Task<string> GetSMTokenByIdAsync(int id);
    }
}
