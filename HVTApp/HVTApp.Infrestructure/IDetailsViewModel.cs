using System;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;

namespace HVTApp.Infrastructure
{
    public interface IDetailsViewModel<out TWrapper, TEntity> : ILoadable, ISavable, IDialogRequestClose
        where TEntity : class, IBaseEntity
        where TWrapper : class, IWrapper<TEntity>
    {
        TWrapper Item { get; }
    }

    public interface ISavable
    {
        ICommand SaveCommand { get; }
    }

    public interface ILoadable
    {
        Task LoadAsync(Guid id);
    }
}