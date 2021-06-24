using Clinic.Core.Entities;
using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IGenericRepository<Employee> _employeeRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            _employeeRepository = new GenericRepository<Employee>(_context);
        }

        public IGenericRepository<Employee> Employee => _employeeRepository;

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
