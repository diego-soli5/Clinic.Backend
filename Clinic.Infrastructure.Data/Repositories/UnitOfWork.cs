﻿using Clinic.Core.Entities;
using Clinic.Core.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMedicRepository _medicRepository;
        private readonly IGenericRepository<MedicalSpecialty> _medicalSpecialty;
        private readonly IGenericRepository<ConsultingRoom> _consultingRoom;


        public UnitOfWork(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public IEmployeeRepository Employee => _employeeRepository ?? new EmployeeRepository(_context, _configuration);
        public IPersonRepository Person => _personRepository ?? new PersonRepository(_context, _configuration);
        public IAppUserRepository AppUser => _appUserRepository ?? new AppUserRepository(_context, _configuration);
        public IMedicRepository Medic => _medicRepository ?? new MedicRepository(_context, _configuration);
        public IGenericRepository<MedicalSpecialty> MedicalSpecialty => _medicalSpecialty ?? new GenericRepository<MedicalSpecialty>(_context, _configuration);
        public IGenericRepository<ConsultingRoom> ConsultingRoom => _consultingRoom ?? new GenericRepository<ConsultingRoom>(_context, _configuration);

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
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
