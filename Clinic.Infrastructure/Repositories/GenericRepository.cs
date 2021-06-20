using Clinic.Core.Entities;
using Clinic.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicSys.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbSet<TEntity> _dbEntity;

        public GenericRepository(DbContext context)
        {
            _dbEntity = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbEntity.AsEnumerable();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _dbEntity.FindAsync(id);
        }

        public void Create(TEntity entity)
        {
            _dbEntity.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbEntity.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _dbEntity.Update(entity);
        }

        public async Task Delete(int id)
        {
            Delete(await GetById(id));
        } 
    }
}
