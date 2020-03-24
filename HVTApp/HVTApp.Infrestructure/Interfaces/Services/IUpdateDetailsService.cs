using System;
using System.Windows.Controls;

namespace HVTApp.Infrastructure.Interfaces.Services
{
    public interface IUpdateDetailsService
    {
        void Register<TEntity, TDetailsView>()
            where TEntity : class, IBaseEntity
            where TDetailsView : Control;

        /// <summary>
        /// Перерегистрация сущности к новому виду.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDetailsView"></typeparam>
        void ReRegister<TEntity, TDetailsView>()
            where TEntity : class, IBaseEntity
            where TDetailsView : Control;

        bool UpdateDetails<TEntity>(Guid id)
            where TEntity : class, IBaseEntity;

        bool UpdateDetails<TEntity>(TEntity entity)
            where TEntity : class, IBaseEntity;

        TEntity UpdateDetailsWithoutSaving<TEntity>(TEntity entity)
            where TEntity : class, IBaseEntity;
    }
}
