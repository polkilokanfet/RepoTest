using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model.Wrapper;

namespace HVTApp.DataAccess
{
    public class BaseRepository<TModel, TWrapper> : IRepository<TWrapper>
        where TWrapper : class, IWrapper<TModel> 
        where TModel : class, IBaseEntity
    {
        protected readonly DbContext Context;

        public BaseRepository(DbContext context)
        {
            Context = context;
        }

        public virtual List<TWrapper> GetAll()
        {
            Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            var models = Context.Set<TModel>();
            List<TWrapper> result = new List<TWrapper>();
            foreach (var model in models)
                result.Add((TWrapper)Activator.CreateInstance(typeof(TWrapper), model));
            return result;
            //return Context.Set<TModel>().Select(x => (TWrapper)Activator.CreateInstance(typeof(TWrapper), x)).ToList();
        }

        public IEnumerable<TWrapper> Find(Func<TWrapper, bool> predicate)
        {
            return GetAll().Where(predicate);
            //return Context.Set<TModel>().Select(x => (TWrapper)Activator.CreateInstance(typeof(TWrapper), x)).Where(predicate);
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
        }

        public void DeleteRange(IEnumerable<TWrapper> entities)
        {
            Context.Set<TModel>().RemoveRange(entities.Select(x => x.Model));
        }
    }
}