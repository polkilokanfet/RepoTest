using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
    public abstract class WrapperRepository<TModel, TWrapper> : IWrapperRepository<TModel, TWrapper> 
        where TModel : class, IBaseEntity 
        where TWrapper : class, IWrapper<TModel>
    {
        protected readonly IUnitOfWorkWrapper UnitOfWorkWrapper;
        private readonly IUnitOfWork _unitOfWork;

        protected WrapperRepository(IWrapperDataService unitOfWork)
        {
            _unitOfWork = unitOfWork;
            UnitOfWorkWrapper = unitOfWork;
        }

        public virtual async Task<IEnumerable<TWrapper>> GetAllAsync()
        {
            var models = await _unitOfWork.GetRepository<TModel>().GetAllAsync();
            var result = new List<TWrapper>();
            foreach (var model in models)
                result.Add(await GenerateWrapper(model));
            return result;
        }

        public async Task<List<TWrapper>> FindAsync(Func<TWrapper, bool> predicate)
        {
            var wrappers = await GetAllAsync();
            return wrappers.Where(predicate).ToList();
        }

        public async Task<TWrapper> GetByIdAsync(Guid id)
        {
            var model = await _unitOfWork.GetRepository<TModel>().GetByIdAsync(id);
            return await GenerateWrapper(model);
        }

        protected virtual async Task<TWrapper> GenerateWrapper(TModel model)
        {
            return await Task.Factory.StartNew(() => Activator.CreateInstance(typeof(TWrapper), model) as TWrapper);
        }

        public void Delete(TWrapper wrapper)
        {
            _unitOfWork.GetRepository<TModel>().Delete(wrapper.Model);
        }

        public void Add(TWrapper wrapper)
        {
            _unitOfWork.GetRepository<TModel>().Add(wrapper.Model);
        }



        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}