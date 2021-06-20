using Clinic.Core.Entities;
using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Repositories.Interfaces;
using Clinic.Infrastructure.Data;
using ClinicSys.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Repositories
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
