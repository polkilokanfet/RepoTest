using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;

namespace HVTApp.Infrastructure
{
    public interface IDetailsViewModel<TWrapper, TEntity> : IDialogRequestClose
        where TEntity : class, IBaseEntity
        where TWrapper : class, IWrapper<TEntity>
    {
        TWrapper Item { get; }
        ICommand SaveCommand { get; }
    }
}