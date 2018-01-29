using System;
using System.Threading.Tasks;
using HVTApp.Infrastructure;

namespace HVTApp.DataAccess
{
    public partial interface IUnitOfWork : IDisposable
    {
        Task<int> CompleteAsync();
        IRepository<T> GetRepository<T>() where T : class, IBaseEntity;
    }
}
