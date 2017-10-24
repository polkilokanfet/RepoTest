using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.DataAccess.Infrastructure
{
    public interface IRepository<TModel>
        where TModel : class, IBaseEntity
    {
        List<TModel> GetAll();
        TModel GetById(Guid Id);
        IEnumerable<TModel> Find(Func<TModel, bool> predicate);

        void Add(TModel entity);
        void AddRange(IEnumerable<TModel> entities);

        void Delete(TModel entity);
        void DeleteRange(IEnumerable<TModel> entities);
    }
}
