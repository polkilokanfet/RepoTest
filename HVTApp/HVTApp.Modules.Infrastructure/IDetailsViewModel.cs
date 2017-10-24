using System;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;

namespace HVTApp.Modules.Infrastructure
{
    public interface IDetailsViewModel<out TWrapper> : IDialogRequestClose
        where TWrapper : IWrapper<IBaseEntity>
    {
        TWrapper Item { get; }
        ICommand SaveCommand { get; }
        Task LoadAsync();
        Task LoadAsync(Guid id);
    }
}