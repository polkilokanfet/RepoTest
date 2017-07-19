using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class BaseRepository<TModel, TWrapper> : IRepository<TModel, TWrapper>
        where TModel : class, IBaseEntity
        where TWrapper : class, IWrapper<TModel>
    {
        protected readonly DbContext Context;
        private readonly IGetWrapper _getWrapper;

        public BaseRepository(DbContext context, IGetWrapper getWrapper)
        {
            Context = context;
            _getWrapper = getWrapper;
        }

        //public virtual TWrapper GetWrapper()
        //{
        //    return WrappersFactory.GetWrapper<TWrapper>();
        //}

        //public TWrapper GetWrapper(TModel model)
        //{
        //    return WrappersFactory.GetWrapper<TWrapper>(model);
        //}

        public virtual List<TWrapper> GetAll()
        {
            Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            return Context.Set<TModel>().ToList().Select(_getWrapper.GetWrapper<TWrapper>).ToList();
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
            _getWrapper.RemoveWrapper(entity.Model);
        }

        public void DeleteRange(IEnumerable<TWrapper> entities)
        {
            Context.Set<TModel>().RemoveRange(entities.Select(x => x.Model));
            foreach (var wrapper in entities)
                _getWrapper.RemoveWrapper(wrapper.Model);
        }








    }
}