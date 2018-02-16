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

        Task<bool> UpdateDetails<TEntity>(Guid? id = null)
            where TEntity : class, IBaseEntity;
    }
}
