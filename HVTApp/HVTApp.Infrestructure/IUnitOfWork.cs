using System;
using System.Threading.Tasks;

namespace HVTApp.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
        IRepository<T> GetRepository<T>() where T : class, IBaseEntity;
    }
}
