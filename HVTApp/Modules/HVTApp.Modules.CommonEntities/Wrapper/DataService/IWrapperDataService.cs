using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
    public interface IWrapperDataService<TModel, TWrapper>
        where TModel : class, IBaseEntity
        where TWrapper : WrapperBase<TModel>
    {
        Task<IEnumerable<TWrapper>> GetAllWrappersAsync();
    }

    public abstract class WrapperDataService<TModel, TWrapper> : IWrapperDataService<TModel, TWrapper> 
        where TModel : class, IBaseEntity 
        where TWrapper : WrapperBase<TModel>
    {
        protected readonly Func<HvtAppContext> ContextCreator;

        protected WrapperDataService(Func<HvtAppContext> contextCreator)
        {
            ContextCreator = contextCreator;
        }

        public virtual async Task<IEnumerable<TWrapper>> GetAllWrappersAsync()
        {
            using (var context = ContextCreator())
            {
                var models = await context.Set<TModel>().AsNoTracking().ToListAsync();
                //return models.Select(x => Activator.CreateInstance(typeof(TWrapper), x)).Cast<TWrapper>(); - не канает, т.к. не подгружает свойства

                var result = new List<TWrapper>();
                foreach (var model in models)
                {
                    var wrapper = GenerateWrapper(model);
                    result.Add(wrapper);
                }
                return result;
            }
        }

        protected virtual TWrapper GenerateWrapper(TModel model)
        {
            return Activator.CreateInstance(typeof(TWrapper), model) as TWrapper;
        }
    }
}
