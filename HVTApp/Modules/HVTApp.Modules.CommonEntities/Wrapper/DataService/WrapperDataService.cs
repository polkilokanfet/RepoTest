using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
    public abstract class WrapperDataService<TModel, TWrapper> : IWrapperDataService<TModel, TWrapper> 
        where TModel : class, IBaseEntity 
        where TWrapper : class, IWrapper<TModel>
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected WrapperDataService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public virtual async Task<IEnumerable<TWrapper>> GetAllWrappersAsync()
        {
            var models = await UnitOfWork.GetRepository<TModel>().GetAllAsync();
            var result = new List<TWrapper>();
            foreach (var model in models)
            {
                var wrapper = GenerateWrapper(model);
                result.Add(wrapper);
            }
            return result;
        }

        //public virtual async Task<IEnumerable<TWrapper>> GetAllWrappersAsync()
        //{
        //    var models = await Context.Set<TModel>().AsNoTracking().ToListAsync();
        //    //return models.Select(x => Activator.CreateInstance(typeof(TWrapper), x)).Cast<TWrapper>(); - не канает, т.к. не подгружает свойства

        //    var result = new List<TWrapper>();
        //    foreach (var model in models)
        //    {
        //        var wrapper = GenerateWrapper(model);
        //        result.Add(wrapper);
        //    }
        //    return result;
        //}

        protected virtual TWrapper GenerateWrapper(TModel model)
        {
            return Activator.CreateInstance(typeof(TWrapper), model) as TWrapper;
        }

        public void Dispose()
        {
            UnitOfWork?.Dispose();
        }
    }
}