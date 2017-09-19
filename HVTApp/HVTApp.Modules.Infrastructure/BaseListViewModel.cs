using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Infrastructure
{
    public interface IBaseListViewModel<TWrapper> : INotifyPropertyChanged
        where TWrapper : class, IWrapper<IBaseEntity>
    {
        ICollection<TWrapper> Items { get; }
        TWrapper SelectedItem { get; set; }

        ICommand NewItemCommand { get; }
        ICommand EditItemCommand { get; }
        ICommand RemoveItemCommand { get; }
        ICommand SelectItemCommand { get; }

        event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }

    public class BaseListViewModel<TWrapper, TDelailsViewModel> : EditableSelectableBindableBase<TWrapper>, IBaseListViewModel<TWrapper> where TWrapper : class, IWrapper<IBaseEntity>
        where TDelailsViewModel : class, IDetailsViewModel<TWrapper> 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnityContainer _container;
        private readonly IDialogService _dialogService;

        public BaseListViewModel(IUnitOfWork unitOfWork, IUnityContainer container, IDialogService dialogService)
        {
            _unitOfWork = unitOfWork;
            _container = container;
            _dialogService = dialogService;
        }

        protected override void NewItemCommand_Execute()
        {
            TWrapper item = _unitOfWork.GetWrapper<TWrapper>();

            TDelailsViewModel delailsViewModel = _container.Resolve<TDelailsViewModel>(new ParameterOverride("item", item));
            bool? dialogResult = _dialogService.ShowDialog(delailsViewModel);
            if (!dialogResult.HasValue || !dialogResult.Value) return;

            _unitOfWork.AddItem(delailsViewModel.Item);
            _unitOfWork.Complete();

            Items.Add(delailsViewModel.Item);
            SelectedItem = delailsViewModel.Item;
        }

        protected override void EditItemCommand_Execute()
        {
            TDelailsViewModel delailsViewModel = _container.Resolve<TDelailsViewModel>(new ParameterOverride("item", SelectedItem));

            var dialogResult = _dialogService.ShowDialog(delailsViewModel);
            if (dialogResult.HasValue && dialogResult.Value)
            {
                SelectedItem.AcceptChanges();
                _unitOfWork.Complete();
            }
            else
            {
                if (SelectedItem.IsChanged)
                    SelectedItem.RejectChanges();
            }
        }

        protected override void RemoveItemCommand_Execute()
        {
            throw new System.NotImplementedException();
        }
    }








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