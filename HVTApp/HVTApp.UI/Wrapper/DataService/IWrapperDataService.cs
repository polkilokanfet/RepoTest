using System;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
    public interface IWrapperDataService : IUnitOfWork, IDisposable
    {
        IWrapperRepository<TModel, TWrapper> GetRepository<TModel, TWrapper>() 
            where TModel : class, IBaseEntity 
            where TWrapper : class, IWrapper<TModel>;
    }
}