using System;
using System.Threading.Tasks;

namespace HVTApp.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Вернуть репозиторий
        /// </summary>
        /// <typeparam name="T">Тип сущности из репозитория</typeparam>
        /// <returns> Репозиторий </returns>
        IRepository<T> Repository<T>() where T : class, IBaseEntity;
        /// <summary>
        /// Сохранить все изменения
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

        void SaveChanges();
    }

    ///// <summary>
    ///// Для создание синглтона. Чтобы не порождать ненужные UOW при работе с выводом.
    ///// </summary>
    //public interface IUnitOfWorkDisplay : IDisposable
    //{
    //}
}
