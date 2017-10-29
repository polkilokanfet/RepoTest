using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
    public interface IWrapperDataService<TModel, TWrapper> : IDisposable
        where TModel : class, IBaseEntity
        where TWrapper : WrapperBase<TModel>
    {
        Task<IEnumerable<TWrapper>> GetAllWrappersAsync();
    }

    public abstract class WrapperDataService<TModel, TWrapper> : IWrapperDataService<TModel, TWrapper> 
        where TModel : class, IBaseEntity 
        where TWrapper : WrapperBase<TModel>
    {
        protected readonly HvtAppContext Context;

        protected WrapperDataService(HvtAppContext context)
        {
            Context = context;
        }

        public virtual async Task<IEnumerable<TWrapper>> GetAllWrappersAsync()
        {
            var models = await Context.Set<TModel>().AsNoTracking().ToListAsync();
            //return models.Select(x => Activator.CreateInstance(typeof(TWrapper), x)).Cast<TWrapper>(); - не канает, т.к. не подгружает свойства

            var result = new List<TWrapper>();
            foreach (var model in models)
            {
                var wrapper = GenerateWrapper(model);
                result.Add(wrapper);
            }
            return result;
        }

        protected virtual TWrapper GenerateWrapper(TModel model)
        {
            return Activator.CreateInstance(typeof(TWrapper), model) as TWrapper;
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
