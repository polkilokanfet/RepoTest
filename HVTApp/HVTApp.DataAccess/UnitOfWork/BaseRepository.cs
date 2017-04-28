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

        private readonly Dictionary<IBaseEntity, object> _repository;
        public BaseRepository(DbContext context, Dictionary<IBaseEntity, object> repository)
        {
            Context = context;
            _repository = repository;
        }

        public virtual List<TWrapper> GetAll()
        {
            Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            List<TWrapper> result = new List<TWrapper>();

            var models = Context.Set<TModel>();
            foreach (var model in models)
                if (_repository.ContainsKey(model))
                    result.Add((TWrapper)_repository[model]);
                else
                    result.Add((TWrapper)Activator.CreateInstance(typeof(TWrapper), model, _repository));

            return result;
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