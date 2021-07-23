using Clinic.Core.Entities;
using System;
using System.Threading.Tasks;

namespace Clinic.Core.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public IEmployeeRepository Employee { get; }
        public IPersonRepository Person { get; }
        public IAppUserRepository AppUser { get; }
        public IMedicRepository Medic { get; }
        public IGenericRepository<MedicalSpecialty> MedicalSpecialty { get; }
        public IGenericRepository<ConsultingRoom> ConsultingRoom { get; }

        Task BeginTransactionAsync();
        Task RollBackAsync();
        Task CommitTransactionAsync();
        Task<bool> Save();
    }
}
