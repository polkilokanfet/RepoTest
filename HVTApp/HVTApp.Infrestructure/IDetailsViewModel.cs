using System;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;

namespace HVTApp.Infrastructure
{
    public interface IDetailsViewModel<TWrapper, in TEntity> : ILoadable, ISavable, IDialogRequestClose
        where TEntity : class, IBaseEntity
        where TWrapper : class, IWrapper<TEntity>
    {
        TWrapper Item { get; }
        Task LoadAsync(TEntity entity);
        Task LoadAsync(TWrapper wrapper, IUnitOfWork unitOfWork);
    }

    public interface ISavable
    {
        ICommand SaveCommand { get; }
    }

    public interface ILoadable
    {
        Task LoadAsync(Guid? id = null);
    }
}