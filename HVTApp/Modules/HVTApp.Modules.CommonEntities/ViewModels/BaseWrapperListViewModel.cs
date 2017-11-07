using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Services.MessageService;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace HVTApp.UI.ViewModels
{
    public class BaseWrapperListViewModel<TWrapper, TModel, TDelailsViewModel, TAfterSaveEntityEvent> :
        BindableBase, ISelectViewModel<TWrapper>, IBaseWrapperListViewModel<TModel, TWrapper>
        where TModel : class, IBaseEntity
        where TWrapper : class, IWrapper<TModel>
        where TDelailsViewModel : IDetailsViewModel<TWrapper, TModel>
        where TAfterSaveEntityEvent : PubSubEvent<TModel>, new()
    {
        protected readonly IUnityContainer Container;
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IEntityWrapperDataService<TModel, TWrapper> WrapperDataService;
        protected readonly IEventAggregator EventAggregator;
        protected readonly IDialogService DialogService;
        protected readonly IMessageService MessageService;

        private TWrapper _selectedItem;

        public BaseWrapperListViewModel(IUnityContainer container, IEntityWrapperDataService<TModel, TWrapper> wrapperDataService)
        {
            Container = container;
            WrapperDataService = wrapperDataService;
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            EventAggregator = Container.Resolve<IEventAggregator>();
            DialogService = Container.Resolve<IDialogService>();
            MessageService = Container.Resolve<IMessageService>();

            Items = new ObservableCollection<TWrapper>();

            NewItemCommand = new DelegateCommand(NewItemCommand_ExecuteAsync, NewItemCommand_CanExecute);
            EditItemCommand = new DelegateCommand(EditItemCommand_ExecuteAsync, EditItemCommand_CanExecute);
            RemoveItemCommand = new DelegateCommand(RemoveItemCommand_Execute, RemoveItemCommand_CanExecute);
            SelectItemCommand = new DelegateCommand(SelectItemCommand_Execute, SelectItemCommand_CanExecute);

            LoadedCommand = new DelegateCommand(async () =>
            {
                if (AutoLoadItems) await LoadAsync();
                AutoLoadItems = false;
            });

            EventAggregator.GetEvent<TAfterSaveEntityEvent>().Subscribe(OnAfterSaveEntity);
        }

        public ICollection<TWrapper> Items { get; }

        public TWrapper SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (Equals(_selectedItem, value)) return;
                _selectedItem = value;
                OnPropertyChanged();
                InvalidateCommands();
            }
        }

        public bool AutoLoadItems { get; set; } = true;

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;

        public virtual async Task LoadAsync()
        {
            var wrappers = (await WrapperDataService.GetAllAsync()).ToList();
            Items.Clear();
            wrappers.ForEach(Items.Add);
        }

        #region Commands
        public ICommand NewItemCommand { get; }
        public ICommand EditItemCommand { get; }
        public ICommand RemoveItemCommand { get; }
        public ICommand SelectItemCommand { get; }
        public DelegateCommand LoadedCommand { get; set; }



        protected void NewItemCommand_ExecuteAsync()
        {
            var model = Activator.CreateInstance<TModel>();
            var wrapper = (TWrapper) Activator.CreateInstance(typeof(TWrapper), model);
            Container.Resolve<IUpdateDetailsService>().UpdateDetails<TModel, TWrapper>(wrapper);
        }

        protected virtual bool NewItemCommand_CanExecute()
        {
            return true;
        }


        protected void EditItemCommand_ExecuteAsync()
        {
            Container.Resolve<IUpdateDetailsService>().UpdateDetails<TModel, TWrapper>(SelectedItem);
        }

        protected virtual bool EditItemCommand_CanExecute()
        {
            return SelectedItem != null;
        }

        protected async void RemoveItemCommand_Execute()
        {
            if (MessageService.ShowYesNoMessageDialog("Удалить", $"Вы действительно хотите удалить '{SelectedItem.DisplayMember}'?") 
                != MessageDialogResult.Yes)
                return;

            var repo = UnitOfWork.GetRepository<TModel>();
            var entityToRemove = await repo.GetByIdAsync(SelectedItem.Model.Id);
            if (entityToRemove != null)
            {
                UnitOfWork.GetRepository<TModel>().Delete(entityToRemove);
                UnitOfWork.Complete();
            }
            Items.Remove(SelectedItem);
        }

        protected virtual bool RemoveItemCommand_CanExecute()
        {
            return SelectedItem != null;
        }

        private void SelectItemCommand_Execute()
        {
            CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
        }

        private bool SelectItemCommand_CanExecute()
        {
            return SelectedItem != null;
        }


        protected virtual void InvalidateCommands()
        {
            ((DelegateCommand)NewItemCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)EditItemCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)RemoveItemCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)SelectItemCommand).RaiseCanExecuteChanged();
        }
        #endregion


        private void OnAfterSaveEntity(TModel entity)
        {
            var wrapper = Items.SingleOrDefault(x => Equals(x.Model.Id, entity.Id));
            if(wrapper == null)
                Items.Add((TWrapper)Activator.CreateInstance(typeof(TWrapper), entity));
            else
                wrapper.Refresh();
        }

    }
}