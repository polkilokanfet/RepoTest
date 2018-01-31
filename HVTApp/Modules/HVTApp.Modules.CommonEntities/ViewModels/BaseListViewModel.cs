using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Services.MessageService;
using HVTApp.UI.Events;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace HVTApp.UI.ViewModels
{
    public abstract class BaseListViewModel<TEntity, TLookup, TAfterSaveEntityEvent, TAfterSelectEntityEvent> :
        BindableBase, IBaseListViewModel<TEntity, TLookup>, ISelectServiceViewModel<TLookup>, IDisposable
        where TEntity : class, IBaseEntity
        where TLookup : class, ILookupItemNavigation<TEntity>
        where TAfterSaveEntityEvent : PubSubEvent<TEntity>, new()
        where TAfterSelectEntityEvent : PubSubEvent<PubSubEventArgs<TEntity>>, new()
    {
        protected readonly IUnityContainer Container;
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IDialogService DialogService;
        protected readonly IMessageService MessageService;

        private TLookup _selectedLookup;

        public IEnumerable<TLookup> Lookups { get; }

        protected BaseListViewModel(IUnityContainer container)
        {
            Container = container;
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            DialogService = Container.Resolve<IDialogService>();
            MessageService = Container.Resolve<IMessageService>();

            Lookups = new ObservableCollection<TLookup>();

            NewItemCommand = new DelegateCommand(NewItemCommand_Execute, NewItemCommand_CanExecute);
            EditItemCommand = new DelegateCommand(EditItemCommand_Execute, EditItemCommand_CanExecute);
            RemoveItemCommand = new DelegateCommand(RemoveItemCommand_ExecuteAsync, RemoveItemCommand_CanExecute);

            SelectItemCommand = new DelegateCommand(SelectItemCommand_Execute, SelectItemCommand_CanExecute);

            Container.Resolve<IEventAggregator>().GetEvent<TAfterSaveEntityEvent>().Subscribe(OnAfterSaveEntity);
        }

        protected virtual async Task<IEnumerable<TEntity>> GetItems()
        {
            return await UnitOfWork.GetRepository<TEntity>().GetAllAsNoTrackingAsync();
        }

        public virtual async Task LoadAsync()
        {
            var lookups = Lookups as ICollection<TLookup>;
            lookups?.Clear();

            if (_lookups != null)
            {
                _lookups.ForEach(lookups.Add);
                return;
            }

            var items = _entities?? await GetItems();
            foreach (var item in items)
            {
                lookups?.Add((TLookup)Activator.CreateInstance(typeof(TLookup), item));
            }
        }

        private IEnumerable<TLookup> _lookups;
        public async Task InjectItems(IEnumerable<TLookup> entities)
        {
            _lookups = new List<TLookup>(entities);
            await LoadAsync();
        }

        private IEnumerable<TEntity> _entities;
        public async Task InjectItems(IEnumerable<TEntity> entities)
        {
            _entities = new List<TEntity>(entities);
            await LoadAsync();
        }

        public TLookup SelectedLookup
        {
            get { return _selectedLookup; }
            set
            {
                if (Equals(_selectedLookup, value)) return;
                _selectedLookup = value;
                OnPropertyChanged();
                InvalidateCommands();
                Container.Resolve<IEventAggregator>().GetEvent<TAfterSelectEntityEvent>().Publish(new PubSubEventArgs<TEntity>(this, value.Entity));
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
        }

        protected virtual bool NewItemCommand_CanExecute()
        {
            return true;
        }


        protected void EditItemCommand_Execute()
        {
            Container.Resolve<IUpdateDetailsService>().UpdateDetails<TEntity>(SelectedLookup.Id);
        }

        protected virtual bool EditItemCommand_CanExecute()
        {
            return !Equals(SelectedLookup, default(TLookup));
        }

        protected async void RemoveItemCommand_ExecuteAsync()
        {
            if (MessageService.ShowYesNoMessageDialog("Удалить",
                    $"Вы действительно хотите удалить '{SelectedLookup.DisplayMember}'?") != MessageDialogResult.Yes)
                return;

            var entityToRemove = await UnitOfWork.GetRepository<TEntity>().GetByIdAsync(SelectedLookup.Id);
            if (entityToRemove == null) return;

            try
            {
                UnitOfWork.GetRepository<TEntity>().Delete(entityToRemove);
                await UnitOfWork.SaveChangesAsync();
                (Lookups as ICollection<TLookup>)?.Remove(SelectedLookup);
            }
            catch (DbUpdateException e)
            {
                MessageService.ShowYesNoMessageDialog("DbUpdateException", e.Message);
            }
        }

        protected virtual bool RemoveItemCommand_CanExecute()
        {
            return !Equals(SelectedLookup, default(TLookup));
        }

        private void SelectItemCommand_Execute()
        {
            CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
        }

        private bool SelectItemCommand_CanExecute()
        {
            return !Equals(SelectedLookup, default(TLookup));
        }


        protected virtual void InvalidateCommands()
        {
            ((DelegateCommand)NewItemCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)EditItemCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)RemoveItemCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)SelectItemCommand).RaiseCanExecuteChanged();
        }
        #endregion

        //реакция на корректировку айтема или на создание нового
        protected virtual async void OnAfterSaveEntity(TEntity entity)
        {
            //обновление существующего айтема
            var lookup = Lookups.SingleOrDefault(x => Equals(x.Id, entity.Id));
            if (lookup != null)
            {
                lookup.Refresh(entity);
                return;
            }

            //добавление несуществующего айтема
            var eee = await UnitOfWork.GetRepository<TEntity>().GetByIdAsync(entity.Id);
            lookup = (TLookup)Activator.CreateInstance(typeof(TLookup), eee);
            lookup.Refresh(eee);
            ((ICollection<TLookup>)Lookups).Add(lookup);

            //выбор добавленного айтема
            SelectedLookup = lookup;
        }

        public void Dispose()
        {
            UnitOfWork?.Dispose();
        }
    }
}