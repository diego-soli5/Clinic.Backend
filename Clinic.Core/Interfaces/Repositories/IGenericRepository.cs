﻿using Clinic.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.Core.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetById(int id, string includeProperties = null);
        IEnumerable<TEntity> GetAll(string includeProperties = null);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task Delete(int id);
    }
}
