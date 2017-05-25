using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using Prism.Commands;
using Prism.Mvvm;

namespace HVTApp.Modules.CommonEntities
{
    public abstract class SelectableBindableBase<T> : BindableBase, ISelectViewModel<T>
        where T : class 
    {
        public ICollection<T> Items { get; }
        public abstract T SelectedItem { get; set; }
        public abstract ICommand NewItemCommand { get; }
        public ICommand SelectItemCommand { get; }

        protected SelectableBindableBase()
        {
            Items = new ObservableCollection<T>();
            SelectItemCommand = new DelegateCommand(SelectItemCommand_Execute, SelectItemCommand_CanExecute);
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;

        private bool SelectItemCommand_CanExecute()
        {
            return SelectedItem != null;
        }

        private void SelectItemCommand_Execute()
        {
            CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
        }

    }
}