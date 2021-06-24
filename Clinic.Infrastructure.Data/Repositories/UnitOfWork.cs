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
        private readonly IGenericRepository<Person> _personRepository;
        private readonly IGenericRepository<AppUser> _appUserRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            _employeeRepository = new GenericRepository<Employee>(_context);
            _personRepository = new GenericRepository<Person>(_context);
            _appUserRepository = new GenericRepository<AppUser>(_context);
        }

        public IGenericRepository<Employee> Employee => _employeeRepository;
        public IGenericRepository<Person> Person => _personRepository;
        public IGenericRepository<AppUser> AppUser => _appUserRepository;

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task RollBackAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
