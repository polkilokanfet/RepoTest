using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.DataAccess
{
    public interface IRepository<TModel, TWrapper>
        where TModel : class, IBaseEntity
        where TWrapper : class, IWrapper<TModel>
    {
        List<TWrapper> GetAll();
        IEnumerable<TWrapper> Find(Func<TWrapper, bool> predicate);

        //TWrapper GetWrapper();
        //TWrapper GetWrapper(TModel model);

        void Add(TWrapper entity);
        void AddRange(IEnumerable<TWrapper> entities);

        void Delete(TWrapper entity);
        void DeleteRange(IEnumerable<TWrapper> entities);
    }
}
