using Clinic.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Clinic.Core.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;

namespace Clinic.Infrastructure.Data.Repositories
{
    public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(AppDbContext context, IConfiguration configuration)
            : base(context, configuration)
        {

        }

        public async Task<string> GetSMTokenByIdAsync(int id)
        {
            return (await _dbEntity.FirstOrDefaultAsync(a => a.Id == id)).SMToken;
        }
    }
}
