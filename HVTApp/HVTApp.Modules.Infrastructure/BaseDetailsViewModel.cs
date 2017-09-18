using System;
using System.ComponentModel;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using Prism.Commands;

namespace HVTApp.Modules.Infrastructure
{
    public abstract class BaseDetailsViewModel<TWrapper> : IDetailsViewModel<TWrapper> 
        where TWrapper : IWrapper<IBaseEntity>
    {
        public TWrapper Item { get; }

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
}