using System;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;

namespace HVTApp.Infrastructure
{
    public interface IDetailsViewModel<TWrapper, TEntity> : ILoadable<TEntity>, ISavable, IDialogRequestClose
        where TEntity : class, IBaseEntity
        where TWrapper : class, IWrapper<TEntity>
    {
        TWrapper Item { get; }
    }

    public interface ISavable
    {
        ICommand SaveCommand { get; }
    }

    public interface ILoadable<TEntity>
        where TEntity : class, IBaseEntity
    {
        Task LoadAsync(Guid id);
        Task LoadAsync(TEntity entity);
    }
}