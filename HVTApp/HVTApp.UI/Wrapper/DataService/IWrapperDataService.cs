using System;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
    public interface IWrapperDataService : IUnitOfWorkWrapper, IUnitOfWork
    {
    }

    public interface IUnitOfWorkWrapper
    {
        IWrapperRepository<TModel, TWrapper> GetWrapperRepository<TModel, TWrapper>()
            where TModel : class, IBaseEntity
            where TWrapper : class, IWrapper<TModel>;
    }
}