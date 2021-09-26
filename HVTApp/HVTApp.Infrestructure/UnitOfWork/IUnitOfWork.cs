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
        /// <returns>Репозиторий сущностей</returns>
        IRepository<T> Repository<T>() where T : class, IBaseEntity;

        ///// <summary>
        ///// Сохранить все изменения
        ///// </summary>
        ///// <returns></returns>
        //Task<int> SaveChangesAsync();

        /// <summary>
        /// Сохранить все изменения
        /// </summary>
        /// <returns></returns>
        UnitOfWorkOperationResult SaveChanges();

        /// <summary>
        /// Сохранение сущности
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">Сущность</param>
        /// <returns></returns>
        UnitOfWorkOperationResult SaveEntity<T>(T entity) where T : class, IBaseEntity;


        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">Сущность</param>
        /// <returns></returns>
        UnitOfWorkOperationResult RemoveEntity<T>(T entity) where T : class, IBaseEntity;
    }

    ///// <summary>
    ///// Для создание синглтона. Чтобы не порождать ненужные UOW при работе с выводом.
    ///// </summary>
    //public interface IUnitOfWorkDisplay : IDisposable
    //{
    //}
}
