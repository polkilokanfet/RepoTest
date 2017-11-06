using System.Windows.Controls;

namespace HVTApp.Infrastructure.Interfaces.Services
{
    public interface IUpdateDetailsService
    {
        void Register<TEntity, TWrapper, TDetailsViewModel, TDetailsView>()
            where TEntity : class, IBaseEntity
            where TWrapper : class, IWrapper<TEntity>
            where TDetailsViewModel : class, IDetailsViewModel<TWrapper, TEntity>
            where TDetailsView : Control;

        void UpdateDetails<TEntity, TWrapper>(TWrapper wrapper)
            where TEntity : class, IBaseEntity
            where TWrapper : class, IWrapper<TEntity>;
    }
}
