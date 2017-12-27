using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
    public abstract class EntityWrapperDataService<TModel, TWrapper> : IEntityWrapperDataService<TModel, TWrapper> 
        where TModel : class, IBaseEntity 
        where TWrapper : class, IWrapper<TModel>
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected readonly List<TWrapper> ExistsWrappers = new List<TWrapper>();

        protected EntityWrapperDataService(IUnitOfWork unitOfWork)
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

        //public virtual async Task<IEnumerable<TWrapper>> GetAllAsync()
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
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            TWrapper wrapper = Activator.CreateInstance(typeof(TWrapper), model) as TWrapper;
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }

        public void Dispose()
        {
            UnitOfWork?.Dispose();
        }

        public async Task<TWrapper> GetByIdAsync(Guid id)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == id))
                return ExistsWrappers.Single(x => x.Model.Id == id);

            TModel model = await UnitOfWork.GetRepository<TModel>().GetByIdAsync(id);
            return GenerateWrapper(model);
        }
    }
}