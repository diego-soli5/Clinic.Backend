using Clinic.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Core.Interfaces.Repositories
{
    public interface IMedicRepository : IGenericRepository<Medic>
    {
        Task<IEnumerable<Medic>> GetAllForListAsync(int? medicalSpecialtyId, int? identification);
    }
}
