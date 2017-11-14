using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
    public interface IEntityWrapperDataService<TModel, TWrapper> : IDisposable
        where TModel : class, IBaseEntity
        where TWrapper : IWrapper<TModel>
    {
        Task<IEnumerable<TWrapper>> GetAllAsync();
        Task<TWrapper> GetByIdAsync(Guid id);
    }
}
