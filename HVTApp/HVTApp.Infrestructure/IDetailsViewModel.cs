using System;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;

namespace HVTApp.Infrastructure
{
    public interface IDetailsViewModel<TWrapper, TEntity> : ILoadable<TEntity>, IViewModelWithEntity<TEntity>, ISavable, IDialogRequestClose
        where TEntity : class, IBaseEntity
        where TWrapper : class, IWrapper<TEntity>
    {
        TWrapper Item { get; }
    }

    public interface IViewModelWithEntity<out TEntity>
    where TEntity : class, IBaseEntity
    {
        TEntity Entity { get; }
    }


    public interface ISavable
    {
        ICommand SaveCommand { get; }
        ICommand OkCommand { get; }
    }

    public interface ILoadable<TEntity>
        where TEntity : class, IBaseEntity
    {
        void Load(Guid id);
        void Load(TEntity entity);
        void Load(TEntity entity, IUnitOfWork unitOfWork);
        //Task LoadAsync(Guid id);
        //Task LoadAsync(TEntity entity);
        //Task LoadAsync(TEntity entity, IUnitOfWork unitOfWork);
    }
}