using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HVTApp.Infrastructure
{
    public interface IRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllAsNoTrackingAsync();
        Task<TEntity> GetByIdAsync(Guid id);
        IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
    }
}
