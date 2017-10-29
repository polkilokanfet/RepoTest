using System;
using HVTApp.Infrastructure;

namespace HVTApp.DataAccess
{
    public partial interface IUnitOfWork : IDisposable
    {
        int Complete();
        IRepository<T> GetRepository<T>() where T : class, IBaseEntity;
    }
}
