using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
    public abstract class WrapperRepository<TModel, TWrapper> : IWrapperRepository<TModel, TWrapper> 
        where TModel : class, IBaseEntity 
        where TWrapper : class, IWrapper<TModel>
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected WrapperRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public virtual async Task<IEnumerable<TWrapper>> GetAllAsync()
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

        public async Task<TWrapper> GetByIdAsync(Guid id)
        {
            return GenerateWrapper(await UnitOfWork.GetRepository<TModel>().GetByIdAsync(id));
        }

        protected virtual TWrapper GenerateWrapper(TModel model)
        {
            return Activator.CreateInstance(typeof(TWrapper), model) as TWrapper;
        }

        public void Delete(TWrapper wrapper)
        {
            UnitOfWork.GetRepository<TModel>().Delete(wrapper.Model);
        }

        public void Add(TWrapper wrapper)
        {
            UnitOfWork.GetRepository<TModel>().Add(wrapper.Model);
        }



        public void Dispose()
        {
            UnitOfWork?.Dispose();
        }
    }
}