using System;
using System.Threading.Tasks;
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

        Task<bool> UpdateDetails<TEntity>(Guid id)
            where TEntity : class, IBaseEntity;

        Task<bool> UpdateDetails<TEntity>(TEntity entity)
            where TEntity : class, IBaseEntity;

        Task<TEntity> UpdateDetailsWithoutSaving<TEntity>(TEntity entity)
            where TEntity : class, IBaseEntity;
    }
}
