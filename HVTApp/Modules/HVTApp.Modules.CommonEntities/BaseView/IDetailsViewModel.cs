using System;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;

namespace HVTApp.UI.BaseView
{
    public interface IDetailsViewModel<out TWrapper, TEntity> : IDialogRequestClose
        where TEntity : class, IBaseEntity
        where TWrapper : class, IWrapper<TEntity>
    {
        TWrapper Item { get; }
        ICommand SaveCommand { get; }
        Task LoadAsync();
        Task LoadAsync(Guid id);
    }
}