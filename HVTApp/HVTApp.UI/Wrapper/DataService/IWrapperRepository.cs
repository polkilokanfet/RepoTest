using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
    public interface IWrapperRepository<TModel, TWrapper> : IDisposable
        where TModel : class, IBaseEntity
        where TWrapper : IWrapper<TModel>
    {
        Task<IEnumerable<TWrapper>> GetAllAsync();
        Task<TWrapper> GetByIdAsync(Guid id);
        void Delete(TWrapper wrapper);
        void Add(TWrapper wrapper);
    }
}
