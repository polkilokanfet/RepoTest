using System;
using System.Threading.Tasks;

namespace HVTApp.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Сохранить все изменения
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Вернуть репозиторий
        /// </summary>
        /// <typeparam name="T">Тип сущности из репозитория</typeparam>
        /// <returns></returns>
        IRepository<T> GetRepository<T>() where T : class, IBaseEntity;
    }
}
