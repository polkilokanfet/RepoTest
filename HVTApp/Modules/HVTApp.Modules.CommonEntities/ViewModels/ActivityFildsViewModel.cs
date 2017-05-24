using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.Wrappers;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public abstract class EditableSelectableBindableBase<T> : SelectableBindableBase<T>
        where T : class
    {
        protected EditableSelectableBindableBase()
        {
            NewItemCommand = new DelegateCommand(NewItemCommand_Execute, NewItemCommand_CanExecute);
            EditItemCommand = new DelegateCommand(EditItemCommand_Execute, EditItemCommand_CanExecute);
            RemoveItemCommand = new DelegateCommand(RemoveItemCommand_Execute);
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
        }

        public override ICommand NewItemCommand { get; }
        public ICommand EditItemCommand { get; }
        public ICommand RemoveItemCommand { get; }

    }


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

    public class ActivityFildsViewModel : SelectableBindableBase<ActivityFieldWrapper>, ISelectViewModel<ActivityFieldWrapper>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ObservableCollection<ActivityFieldWrapper> ActivityFields { get; }


        public ActivityFildsViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            ActivityFields = new ObservableCollection<ActivityFieldWrapper>(_unitOfWork.ActivityFields.GetAll());

            NewActivityFieldCommand = new DelegateCommand(NewActivityFieldCommand_Execute, NewActivityFieldCommand_CanExecute);
            EditActivityFieldCommand = new DelegateCommand(EditActivityFieldCommand_Execute, EditActivityFieldCommand_CanExecute);
            RemoveActivityFieldCommand = new DelegateCommand(RemoveActivityFieldCommand_Execute, RemoveActivityFieldCommand_CanExecute);
        }

        #region Commands
        public DelegateCommand NewActivityFieldCommand { get; }
        public DelegateCommand EditActivityFieldCommand { get; }
        public DelegateCommand RemoveActivityFieldCommand { get; }

        private bool RemoveActivityFieldCommand_CanExecute()
        {
            throw new NotImplementedException();
        }

        private void RemoveActivityFieldCommand_Execute()
        {
            throw new NotImplementedException();
        }

        private bool EditActivityFieldCommand_CanExecute()
        {
            throw new NotImplementedException();
        }

        private void EditActivityFieldCommand_Execute()
        {
            throw new NotImplementedException();
        }

        private bool NewActivityFieldCommand_CanExecute()
        {
            throw new NotImplementedException();
        }

        private void NewActivityFieldCommand_Execute()
        {
            throw new NotImplementedException();
        }
        

        #endregion

        private ActivityFieldWrapper _selectedActivityField;
        public ActivityFieldWrapper SelectedActivityField
        {
            get { return _selectedActivityField; }
            set
            {
                _selectedActivityField = value;
                OnPropertyChanged();
                InvalidateCommands();
            }
        }

        private void InvalidateCommands()
        {
            ((DelegateCommand)SelectItemCommand).RaiseCanExecuteChanged();
        }

        #region ISelectViewModel

        //public override ICollection<ActivityFieldWrapper> Items => ActivityFields;

        public override ActivityFieldWrapper SelectedItem
        {
            get { return SelectedActivityField; }
            set { SelectedActivityField = value; }
        }

        public override ICommand NewItemCommand => NewActivityFieldCommand;

        #endregion
    }
}
