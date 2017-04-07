using System;
using System.Collections.Generic;

namespace HVTApp.Infrastructure.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class 
    {
        List<TEntity> GetAll();
        IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
    }
}
