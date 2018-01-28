using System;
using System.Windows.Controls;

namespace HVTApp.Infrastructure.Interfaces.Services
{
    public interface IUpdateDetailsService
    {
        void Register<TEntity, TDetailsView>()
            where TEntity : class, IBaseEntity
            where TDetailsView : Control;

        bool UpdateDetails<TEntity>(Guid entityId) 
            where TEntity : class, IBaseEntity;
    }
}
