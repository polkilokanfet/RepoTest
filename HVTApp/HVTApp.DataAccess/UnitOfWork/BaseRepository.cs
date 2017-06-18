using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.Factory;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class BaseRepository<TModel, TWrapper> : IRepository<TModel,TWrapper>
        where TModel : class, IBaseEntity
        where TWrapper : class, IWrapper<TModel> 
    {
        protected readonly DbContext Context;

        public BaseRepository(DbContext context)
        {
            Context = context;
        }

        public virtual TWrapper GetWrapper()
        {
            return WrappersFactory.GetWrapper<TModel, TWrapper>();
        }

        public TWrapper GetWrapper(TModel model)
        {
            return WrappersFactory.GetWrapper<TModel, TWrapper>(model);
        }

        public virtual List<TWrapper> GetAll()
        {
            Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            return Context.Set<TModel>().ToList().Select(WrappersFactory.GetWrapper<TModel, TWrapper>).ToList();
        }

        public IEnumerable<TWrapper> Find(Func<TWrapper, bool> predicate)
        {
            return GetAll().Where(predicate);
        }


        public void Add(TWrapper entity)
        {
            Context.Set<TModel>().Add(entity.Model);
        }

        public void AddRange(IEnumerable<TWrapper> entities)
        {
            Context.Set<TModel>().AddRange(entities.Select(x => x.Model));
        }


        public void Delete(TWrapper entity)
        {
            Context.Set<TModel>().Remove(entity.Model);
            WrappersFactory.RemoveWrapper(entity.Model);
        }

        public void DeleteRange(IEnumerable<TWrapper> entities)
        {
            Context.Set<TModel>().RemoveRange(entities.Select(x => x.Model));
            foreach (var wrapper in entities)
                WrappersFactory.RemoveWrapper(wrapper.Model);
        }

    }
}