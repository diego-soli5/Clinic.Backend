using Clinic.Core.Entities;
using Clinic.Core.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace Clinic.Core.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public IGenericRepository<Employee> Employee { get; }

        Task<bool> Save();
    }
}
