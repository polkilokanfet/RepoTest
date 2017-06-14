using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class BaseRepository<TModel, TWrapper> : IRepository<TWrapper>
        where TModel : class, IBaseEntity
        where TWrapper : class, IWrapper<TModel> 
    {
        protected readonly DbContext Context;

        protected readonly Dictionary<IBaseEntity, IWrapper<IBaseEntity>> WrappersRepository;

        public BaseRepository(DbContext context, Dictionary<IBaseEntity, IWrapper<IBaseEntity>> wrappersRepository)
        {
            if (context == null || wrappersRepository == null) throw new ArgumentNullException();

            Context = context;
            WrappersRepository = wrappersRepository;
        }

        public virtual List<TWrapper> GetAll()
        {
            Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            List<TWrapper> result = new List<TWrapper>();

            var models = Context.Set<TModel>();
            foreach (var model in models)
                if (WrappersRepository.ContainsKey(model))
                    result.Add((TWrapper)WrappersRepository[model]);
                else
                    result.Add((TWrapper)Activator.CreateInstance(typeof(TWrapper), model, WrappersRepository));

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
            WrappersRepository.Remove(entity.Model);
        }

        public void DeleteRange(IEnumerable<TWrapper> entities)
        {
            Context.Set<TModel>().RemoveRange(entities.Select(x => x.Model));
            foreach (var wrapper in entities)
                WrappersRepository.Remove(wrapper.Model);
        }
    }
}