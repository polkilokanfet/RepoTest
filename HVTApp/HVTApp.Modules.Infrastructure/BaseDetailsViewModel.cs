using System;
using System.ComponentModel;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.Wrappers;
using Prism.Commands;

namespace HVTApp.Modules.Infrastructure
{
    public abstract class BaseDetailsViewModel<TWrapper, TModel> : IItemDetailsViewModel<TWrapper, TModel> 
        where TWrapper : WrapperBase<TModel>
        where TModel : class, IBaseEntity 
    {
        protected BaseDetailsViewModel(TWrapper item)
        {
            Item = item;
            Item.PropertyChanged += ItemOnPropertyChanged;

            OkCommand = new DelegateCommand(OkCommand_Execute, OkCommand_CanExecute);
        }

        private void ItemOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand)OkCommand).RaiseCanExecuteChanged();
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
        public TWrapper Item { get; }
        public ICommand OkCommand { get; }

        private void OkCommand_Execute()
        {
            CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
        }

        private bool OkCommand_CanExecute()
        {
            return Item.IsChanged && Item.IsValid;
        }
    }

    public interface IItemDetailsViewModel<TWrapper, TModel> : IDialogRequestClose
    where TModel : class, IBaseEntity
    where TWrapper : IWrapper<TModel>
    {
        TWrapper Item { get; }
        ICommand OkCommand { get; }
    }

}