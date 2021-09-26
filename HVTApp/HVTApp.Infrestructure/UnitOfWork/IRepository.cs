using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HVTApp.Infrastructure
{
    public interface IRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        //Task<List<TEntity>> GetAllAsync();
        List<TEntity> GetAll();

        List<TEntity> GetAllAsNoTracking();
        //Task<List<TEntity>> GetAllAsNoTrackingAsync();

        TEntity GetById(Guid id);
        //Task<TEntity> GetByIdAsync(Guid id);

        List<TEntity> Find(Func<TEntity, bool> predicate);
        List<TEntity> FindAsNoTracking(Func<TEntity, bool> predicate);

        UnitOfWorkOperationResult Add(TEntity entity);
        UnitOfWorkOperationResult AddRange(IEnumerable<TEntity> entities);

        UnitOfWorkOperationResult Delete(TEntity entity);
        UnitOfWorkOperationResult DeleteRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Событие провала операции
        /// </summary>
        event Action<UnitOfWorkOperationResult> OperationFailedEvent;

        void Reload(TEntity entity);
    }
}
