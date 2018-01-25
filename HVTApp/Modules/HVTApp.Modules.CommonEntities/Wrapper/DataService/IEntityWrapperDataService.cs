using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
    public interface IEntityWrapperDataService<TModel, out TWrapper> : IDisposable
        where TModel : class, IBaseEntity
        where TWrapper : IWrapper<TModel>
    {
        IEnumerable<TWrapper> GetAll();
        TWrapper GetById(Guid id);
        //Task<IEnumerable<TWrapper>> GetAll();
        //Task<TWrapper> GetById(Guid id);
    }
}
