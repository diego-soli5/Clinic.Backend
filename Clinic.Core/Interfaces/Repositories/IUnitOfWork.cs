using Clinic.Core.Entities;
using System;
using System.Threading.Tasks;

namespace Clinic.Core.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public IEmployeeRepository Employee { get; }
        public IGenericRepository<Person> Person { get; }
        public IGenericRepository<AppUser> AppUser { get; }
        Task BeginTransactionAsync();
        Task RollBackAsync();
        Task<bool> Save();
    }
}
