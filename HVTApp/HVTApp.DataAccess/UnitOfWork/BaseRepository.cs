using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure;

namespace HVTApp.DataAccess
{
    public class BaseRepository<TModel> : IRepository<TModel>
        where TModel : class, IBaseEntity
    {
        protected readonly DbContext Context;

        public BaseRepository(DbContext context)
        {
            Context = context;
        }

        public virtual List<TModel> GetAll()
        {
            Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            return Context.Set<TModel>().ToList();
        }

        public IEnumerable<TModel> Find(Func<TModel, bool> predicate)
        {
            return GetAll().Where(predicate);
        }


        public void Add(TModel entity)
        {
            Context.Set<TModel>().Add(entity);
        }

        public void AddRange(IEnumerable<TModel> entities)
        {
            Context.Set<TModel>().AddRange(entities);
        }


        public void Delete(TModel entity)
        {
            Context.Set<TModel>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<TModel> entities)
        {
            Context.Set<TModel>().RemoveRange(entities);
        }








    }
}