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
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace HVTApp.UI.ViewModels
{
    public abstract class BaseWrapperListViewModel<TWrapper, TEntity, TDelailsViewModel, TAfterSaveEntityEvent> :
        BindableBase, IBaseWrapperListViewModel<TEntity, TWrapper>, ISelectViewModel<TWrapper>, IDisposable
        where TEntity : class, IBaseEntity
        where TWrapper : class, IWrapper<TEntity>
        where TDelailsViewModel : IDetailsViewModel<TWrapper, TEntity>
        where TAfterSaveEntityEvent : PubSubEvent<TEntity>, new()
    {
        protected readonly IUnityContainer Container;
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IEntityWrapperDataService<TEntity, TWrapper> WrapperDataService;
        protected readonly IEventAggregator EventAggregator;
        protected readonly IDialogService DialogService;
        protected readonly IMessageService MessageService;

        public IEnumerable<TWrapper> Items { get; }

        protected BaseWrapperListViewModel(IUnityContainer container, IEntityWrapperDataService<TEntity, TWrapper> wrapperDataService)
        {
            Container = container;
            WrapperDataService = wrapperDataService;
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            EventAggregator = Container.Resolve<IEventAggregator>();
            DialogService = Container.Resolve<IDialogService>();
            MessageService = Container.Resolve<IMessageService>();

            Items = new ObservableCollection<TWrapper>();

            NewItemCommand = new DelegateCommand(NewItemCommand_Execute, NewItemCommand_CanExecute);
            EditItemCommand = new DelegateCommand(EditItemCommand_Execute, EditItemCommand_CanExecute);
            RemoveItemCommand = new DelegateCommand(RemoveItemCommand_ExecuteAsync, RemoveItemCommand_CanExecute);

            SelectItemCommand = new DelegateCommand(SelectItemCommand_Execute, SelectItemCommand_CanExecute);

            EventAggregator.GetEvent<TAfterSaveEntityEvent>().Subscribe(OnAfterSaveEntity);
        }

        protected virtual async Task<IEnumerable<TWrapper>> GetItems()
        {
            return (await UnitOfWork.GetRepository<TEntity>().GetAllAsNoTrackingAsync())
                .Select(x => (TWrapper) Activator.CreateInstance(typeof(TWrapper), x));
        }

        public virtual async Task LoadAsync()
        {
            var items = Items as ICollection<TWrapper>;
            items.Clear();
            var wrappers = await GetItems();
            wrappers.ForEach(items.Add);
        }

        private TWrapper _selectedItem;
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

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;

        #region Commands

        public ICommand NewItemCommand { get; }
        public ICommand EditItemCommand { get; }
        public ICommand RemoveItemCommand { get; }
        public ICommand SelectItemCommand { get; }



        protected void NewItemCommand_Execute()
        {
            //var model = Activator.CreateInstance<TEntity>();
            //var wrapper = (TWrapper) Activator.CreateInstance(typeof(TWrapper), model);
            Container.Resolve<IUpdateDetailsService>().UpdateDetails<TEntity>(Guid.Empty);

            //добавляется сущность в реакции на событие сохранения OnAfterSaveEntity
        }

        protected virtual bool NewItemCommand_CanExecute()
        {
            return true;
        }


        protected void EditItemCommand_Execute()
        {
            Container.Resolve<IUpdateDetailsService>().UpdateDetails<TEntity>(SelectedItem.Model.Id);
        }

        protected virtual bool EditItemCommand_CanExecute()
        {
            return SelectedItem != null;
        }

        protected async void RemoveItemCommand_ExecuteAsync()
        {
            if (MessageService.ShowYesNoMessageDialog("Удалить", $"Вы действительно хотите удалить '{SelectedItem.DisplayMember}'?") != MessageDialogResult.Yes)
                return;

            var repo = UnitOfWork.GetRepository<TEntity>();
            var entityToRemove = await repo.GetByIdAsync(SelectedItem.Model.Id);
            if (entityToRemove != null)
            {
                UnitOfWork.GetRepository<TEntity>().Delete(entityToRemove);
                UnitOfWork.Complete();
            }
            (Items as ICollection<TWrapper>).Remove(SelectedItem);
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


        private void OnAfterSaveEntity(TEntity entity)
        {
            var wrapper = Items.SingleOrDefault(x => Equals(x.Model.Id, entity.Id));
            if(wrapper == null)
                (Items as ICollection<TWrapper>).Add((TWrapper)Activator.CreateInstance(typeof(TWrapper), entity));
            else
                wrapper.Refresh();
        }

        public void Dispose()
        {
            UnitOfWork?.Dispose();
        }
    }
}