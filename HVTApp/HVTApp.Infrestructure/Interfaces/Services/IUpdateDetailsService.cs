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

        Task<bool> UpdateDetails<TEntity>(Guid id)
            where TEntity : class, IBaseEntity;
        Task<bool> UpdateDetails<TEntity>(TEntity entity)
            where TEntity : class, IBaseEntity;
        Task<bool> UpdateDetails<TEntity,TWrapper>(TWrapper wrapper, IUnitOfWork unitOfWork)
            where TEntity : class, IBaseEntity
            where TWrapper : class, IWrapper<TEntity>;
    }
}
