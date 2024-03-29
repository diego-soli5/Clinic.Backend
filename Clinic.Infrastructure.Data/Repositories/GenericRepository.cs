﻿using Clinic.Core.Entities;
using Clinic.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Data.Repositories
{
    public class GenericRepository<TEntity> : ADORepository, IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbSet<TEntity> _dbEntity;

        public GenericRepository(DbContext context, IConfiguration configuration)
            : base(configuration)
        {
            _dbEntity = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll(string includeProperties = null)
        {
            IQueryable<TEntity> query = _dbEntity;

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.AsEnumerable();
        }

        public async Task<TEntity> GetByIdAsync(int id, string includeProperties = null)
        {
            IQueryable<TEntity> query = _dbEntity;

            if(includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
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
            Delete(await GetByIdAsync(id));
        } 
    }
}
