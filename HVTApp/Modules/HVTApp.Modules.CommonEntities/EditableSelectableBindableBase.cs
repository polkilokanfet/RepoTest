using System.Windows.Input;
using HVTApp.Modules.CommonEntities.ViewModels;
using Prism.Commands;

namespace HVTApp.Modules.CommonEntities
{
    public abstract class EditableSelectableBindableBase<T> : SelectableBindableBase<T>
        where T : class
    {
        protected EditableSelectableBindableBase()
        {
            NewItemCommand = new DelegateCommand(NewItemCommand_Execute, NewItemCommand_CanExecute);
            EditItemCommand = new DelegateCommand(EditItemCommand_Execute, EditItemCommand_CanExecute);
            RemoveItemCommand = new DelegateCommand(RemoveItemCommand_Execute, RemoveItemCommand_CanExecute);
        }

        protected virtual bool RemoveItemCommand_CanExecute()
        {
            return false; 
        }

        protected abstract void RemoveItemCommand_Execute();

        protected virtual bool EditItemCommand_CanExecute()
        {
            return SelectedItem != null;
        }

        protected abstract void EditItemCommand_Execute();

        protected virtual bool NewItemCommand_CanExecute()
        {
            return true;
        }

        protected abstract void NewItemCommand_Execute();

        private T _selectedItem;
        public override T SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                InvalidateCommands();
            }
        }

        private void InvalidateCommands()
        {
            ((DelegateCommand)NewItemCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)EditItemCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)RemoveItemCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)SelectItemCommand).RaiseCanExecuteChanged();
        }

        public override ICommand NewItemCommand { get; }
        public ICommand EditItemCommand { get; }
        public ICommand RemoveItemCommand { get; }

    }
}