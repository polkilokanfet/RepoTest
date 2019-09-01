using System;
using System.Threading.Tasks;

namespace HVTApp.Infrastructure
{
    public interface IUnitOfWork : IUnitOfWorkDisplay
    {
        /// <summary>
        /// Сохранить все изменения
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
    }

    /// <summary>
    /// Для создание синглтона. Чтобы не порождать ненужные UOW при работе с выводом.
    /// </summary>
    public interface IUnitOfWorkDisplay : IDisposable
    {
        /// <summary>
        /// Вернуть репозиторий
        /// </summary>
        /// <typeparam name="T">Тип сущности из репозитория</typeparam>
        /// <returns> Репозиторий </returns>
        IRepository<T> Repository<T>() where T : class, IBaseEntity;
    }
}
