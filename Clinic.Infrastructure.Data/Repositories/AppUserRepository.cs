using Clinic.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Clinic.Core.Interfaces.Repositories;

namespace Clinic.Infrastructure.Data.Repositories
{
    public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(AppDbContext context)
            : base(context)
        {

        }

        public async Task<string> GetSMTokenByIdAsync(int id)
        {
            return (await _dbEntity.FirstOrDefaultAsync(a => a.Id == id)).SMToken;
        }
    }
}
