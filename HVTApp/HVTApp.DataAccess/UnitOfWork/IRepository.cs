using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HVTApp.Infrastructure;

namespace HVTApp.DataAccess
{
    public interface IRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        List<TEntity> GetAll();
        //Task<List<TEntity>> GetAllAsync();
        //Task<TEntity> GetByIdAsync(Guid id);
        TEntity GetById(Guid id);
        IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
    }
}
