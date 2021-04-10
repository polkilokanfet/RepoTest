using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HVTApp.Infrastructure
{
    public interface IRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        //Task<List<TEntity>> GetAllAsync();
        TEntity[] GetAll();

        TEntity[] GetAllAsNoTracking();
        //Task<List<TEntity>> GetAllAsNoTrackingAsync();

        TEntity GetById(Guid id);
        //Task<TEntity> GetByIdAsync(Guid id);

        TEntity[] Find(Func<TEntity, bool> predicate);
        TEntity[] FindAsNoTracking(Func<TEntity, bool> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);

        void Reload(TEntity entity);
    }
}
