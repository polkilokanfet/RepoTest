using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.Services.MessageService;
using HVTApp.UI.Events;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.ViewModels
{
    public abstract class BaseListViewModel<TEntity, TLookup, TAfterSaveEntityEvent, TAfterSelectEntityEvent, TAfterRemoveEntityEvent> :
        BindableBaseCanExportToExcel, IBaseListViewModel<TEntity, TLookup>, ISelectServiceViewModel<TEntity>, IDisposable
        where TEntity : class, IBaseEntity
        where TLookup : class, ILookupItemNavigation<TEntity>
        where TAfterSaveEntityEvent : PubSubEvent<TEntity>, new()
        where TAfterRemoveEntityEvent : PubSubEvent<TEntity>, new()
        where TAfterSelectEntityEvent : PubSubEvent<PubSubEventArgs<TEntity>>, new()
    {
        protected readonly IUnityContainer Container;
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IDialogService DialogService;
        protected readonly IMessageService MessageService;
        protected readonly IEventAggregator EventAggregator;

        private TLookup _selectedLookup;
        private TEntity _selectedItem;

        protected BaseListViewModel(IUnityContainer container) : base(container)
        {
            Container = container;
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            DialogService = Container.Resolve<IDialogService>();
            MessageService = Container.Resolve<IMessageService>();
            EventAggregator = Container.Resolve<IEventAggregator>();

            Lookups = new ObservableCollection<TLookup>();

            NewItemCommand = new DelegateCommand(NewItemCommand_Execute, NewItemCommand_CanExecute);
            EditItemCommand = new DelegateCommand(EditItemCommand_Execute, EditItemCommand_CanExecute);
            RemoveItemCommand = new DelegateCommand(RemoveItemCommand_ExecuteAsync, RemoveItemCommand_CanExecute);

            SelectItemCommand = new DelegateCommand(SelectItemCommand_Execute, SelectItemCommand_CanExecute);

            EventAggregator.GetEvent<TAfterSaveEntityEvent>().Subscribe(OnAfterSaveEntity);
            EventAggregator.GetEvent<TAfterRemoveEntityEvent>().Subscribe(OnAfterRemoveEntity);

            SubscribesToEvents();
        }

        /// <summary>
        /// Подписка на события. Запуск в конце конструктора.
        /// </summary>
        protected virtual void SubscribesToEvents()
        {
        }


        public TLookup SelectedLookup
        {
            get { return _selectedLookup; }
            set
            {
                if (Equals(_selectedLookup, value)) return;
                _selectedLookup = value;
                SelectedItem = _selectedLookup?.Entity;
                OnPropertyChanged();
                SelectedLookupChanged?.Invoke(_selectedLookup);
                EventAggregator.GetEvent<TAfterSelectEntityEvent>().Publish(new PubSubEventArgs<TEntity>(this, value?.Entity));
                InvalidateCommands();
            }
        }


        public TEntity SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (Equals(_selectedItem, value)) return;
                _selectedItem = value;
                SelectedLookup = _selectedLookup != null ? Lookups.Single(x => x.Entity.Id == _selectedItem.Id) : null;
                OnPropertyChanged();
            }
        }

        public IEnumerable<TLookup> Lookups { get; }
        private ICollection<TLookup> LookupsCollection => (ICollection<TLookup>)Lookups;

        private static TLookup GetLookup(TEntity entity)
        {
            return (TLookup) Activator.CreateInstance(typeof(TLookup), entity);
        }

        public virtual async Task LoadAsync()
        {
            LookupsCollection.Clear();
            SelectedLookup = null;
            var lookups = (await GetItems()).Select(GetLookup).OrderBy(x => x);
            lookups.ForEach(LookupsCollection.Add);
            Loaded?.Invoke();
        }

        public virtual void Load(IEnumerable<TEntity> entities)
        {
            Load(entities.Select(GetLookup));
        }

        public virtual void Load(IEnumerable<TLookup> lookups)
        {
            LookupsCollection.Clear();
            SelectedLookup = null;
            lookups.OrderBy(x => x).ForEach(LookupsCollection.Add);
            Loaded?.Invoke();
        }



        public event Action Loaded;

        public event Action<TLookup> SelectedLookupChanged; 

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;



        protected virtual async Task<IEnumerable<TEntity>> GetItems()
        {
            return await UnitOfWork.GetRepository<TEntity>().GetAllAsNoTrackingAsync();
        }

        #region Commands

        public ICommand NewItemCommand { get; }
        public ICommand EditItemCommand { get; }
        public ICommand RemoveItemCommand { get; }
        public ICommand SelectItemCommand { get; }


        protected async void NewItemCommand_Execute()
        {
            var entity = Activator.CreateInstance<TEntity>();
            await Container.Resolve<IUpdateDetailsService>().UpdateDetails(entity);
        }

        protected virtual bool NewItemCommand_CanExecute()
        {
            var attribute = this.GetType().GetCustomAttribute<RoleToUpdateAttribute>();
            return attribute == null || !attribute.Roles.Contains(Role.Admin);
        }


        protected async void EditItemCommand_Execute()
        {
            await Container.Resolve<IUpdateDetailsService>().UpdateDetails<TEntity>(SelectedLookup.Id);
        }

        protected virtual bool EditItemCommand_CanExecute()
        {
            return !Equals(SelectedLookup, default(TLookup));
        }

        protected async void RemoveItemCommand_ExecuteAsync()
        {
            var dr = MessageService.ShowYesNoMessageDialog("Удалить", $"Вы действительно хотите удалить '{SelectedLookup.DisplayMember}'?");
            if (dr != MessageDialogResult.Yes) return;

            var entityToRemove = await UnitOfWork.GetRepository<TEntity>().GetByIdAsync(SelectedLookup.Id);
            if (entityToRemove == null) return;

            try
            {
                UnitOfWork.GetRepository<TEntity>().Delete(entityToRemove);
                await UnitOfWork.SaveChangesAsync();
                LookupsCollection.Remove(SelectedLookup);
            }
            catch (DbUpdateException e)
            {
                Exception ex = e;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                MessageService.ShowYesNoMessageDialog("DbUpdateException", ex.Message);
            }

            EventAggregator.GetEvent<TAfterRemoveEntityEvent>().Publish(entityToRemove);
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

        /// <summary>
        /// реакция на корректировку айтема или на создание нового
        /// </summary>
        /// <param name="entity"></param>
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
            var newEntity = await UnitOfWork.GetRepository<TEntity>().GetByIdAsync(entity.Id);
            lookup = GetLookup(newEntity);
            lookup.Refresh(newEntity);
            LookupsCollection.Add(lookup);

            //выбор добавленного айтема
            SelectedLookup = lookup;
        }

        private void OnAfterRemoveEntity(TEntity entity)
        {
            var lookup = Lookups.SingleOrDefault(x => x.Id == entity.Id);
            if (lookup != null)
            {
                LookupsCollection.Remove(lookup);
                SelectedLookup = null;
            }
        }

        public void Dispose()
        {
            UnitOfWork?.Dispose();
        }
    }
}