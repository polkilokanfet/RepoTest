using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.Events;
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
        protected readonly IDialogService DialogService;
        protected readonly IMessageService MessageService;
        protected readonly IEventAggregator EventAggregator;

        protected bool AllowEdit => typeof(TEntity).GetAllowEditRoles().Contains(GlobalAppProperties.User.RoleCurrent);

        private TLookup _selectedLookup;
        private TEntity _selectedItem;
        private bool _isLoaded = false;

        protected BaseListViewModel(IUnityContainer container) : base(container)
        {
            DialogService = Container.Resolve<IDialogService>();
            MessageService = Container.Resolve<IMessageService>();
            EventAggregator = Container.Resolve<IEventAggregator>();

            Lookups = new ObservableCollection<TLookup>();

            NewItemCommand = new DelegateCommand(NewItemCommand_Execute, NewItemCommand_CanExecute);
            EditItemCommand = new DelegateCommand(EditItemCommand_Execute, EditItemCommand_CanExecute);
            RemoveItemCommand = new DelegateCommand(RemoveItemCommand_ExecuteAsync, RemoveItemCommand_CanExecute);

            SelectItemCommand = new DelegateCommand(SelectItemCommand_Execute, SelectItemCommand_CanExecute);

            RefreshCommand = new DelegateCommand(RefreshCommand_Execute);

            EventAggregator.GetEvent<TAfterSaveEntityEvent>().Subscribe(OnAfterSaveEntity);
            EventAggregator.GetEvent<TAfterRemoveEntityEvent>().Subscribe(OnAfterRemoveEntity);

            this.LoadBegin += () => IsLoaded = false;
            this.Loaded += () => IsLoaded = true;

            InitSpecialCommands();
            SubscribesToEvents();
        }

        public bool IsLoaded
        {
            get { return _isLoaded; }
            private set
            {
                _isLoaded = value;
                OnPropertyChanged();
            }
        }

        protected virtual void InitSpecialCommands() { }

        /// <summary>
        /// Подписка на события. Запуск в конце конструктора.
        /// </summary>
        protected virtual void SubscribesToEvents() { }

        /// <summary>
        /// Выбранный Lookup
        /// </summary>
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

        /// <summary>
        /// Выбранный Item.
        /// </summary>
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

        /// <summary>
        /// Загрузка всех Lookup'ов.
        /// </summary>
        /// <returns></returns>
        public virtual async Task LoadAsync()
        {
            LoadBegin?.Invoke();
            LookupsCollection.Clear();
            SelectedLookup = null;
            var entities = await UnitOfWork.Repository<TEntity>().GetAllAsNoTrackingAsync();
            var lookups =  entities.Select(x => (TLookup)Activator.CreateInstance(typeof(TLookup), x));
            lookups.ForEach(LookupsCollection.Add);
            Loaded?.Invoke();
        }

        public void Load(IEnumerable<TEntity> entities)
        {
            var lookups = entities.Select(x => (TLookup)Activator.CreateInstance(typeof(TLookup), x));
            Load(lookups);
        }

        public void Load(IEnumerable<TLookup> lookups)
        {
            LoadBegin?.Invoke();
            IsLoaded = false;
            LookupsCollection.Clear();
            if(!Lookups.Contains(SelectedLookup)) SelectedLookup = null;
            lookups.OrderBy(x => x).ForEach(LookupsCollection.Add);
            Loaded?.Invoke();
        }


        private event Action LoadBegin;

        public event Action Loaded;

        public event Action<TLookup> SelectedLookupChanged; 

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;

        #region Commands

        public ICommand NewItemCommand { get; protected set; }
        public ICommand EditItemCommand { get; protected set; }
        public ICommand RemoveItemCommand { get; protected set; }
        public ICommand SelectItemCommand { get; protected set; }
        public ICommand RefreshCommand { get; protected set; }

        /// <summary>
        /// Генерация нового айтема (при создании нового).
        /// </summary>
        /// <returns></returns>
        protected virtual TEntity GetNewItem()
        {
            return Activator.CreateInstance<TEntity>();
        }
        protected async void NewItemCommand_Execute()
        {
            await Container.Resolve<IUpdateDetailsService>().UpdateDetails(GetNewItem());
        }

        protected virtual bool NewItemCommand_CanExecute()
        {
            return AllowEdit;
        }


        protected async void EditItemCommand_Execute()
        {
            await Container.Resolve<IUpdateDetailsService>().UpdateDetails<TEntity>(SelectedLookup.Id);
        }

        protected virtual bool EditItemCommand_CanExecute()
        {
            return AllowEdit && !Equals(SelectedLookup, default(TLookup));
        }

        protected async void RemoveItemCommand_ExecuteAsync()
        {
            var dr = MessageService.ShowYesNoMessageDialog("Удаление", $"Вы действительно хотите удалить \"{SelectedLookup.DisplayMember}\"?");
            if (dr != MessageDialogResult.Yes) return;

            var entity = await UnitOfWork.Repository<TEntity>().GetByIdAsync(SelectedLookup.Id);
            if (entity == null) return;

            try
            {
                UnitOfWork.Repository<TEntity>().Delete(entity);
                await UnitOfWork.SaveChangesAsync();
                LookupsCollection.Remove(SelectedLookup);
                EventAggregator.GetEvent<TAfterRemoveEntityEvent>().Publish(entity);
            }
            catch (DbUpdateException e)
            {
                MessageService.ShowOkMessageDialog("DbUpdateException", e.GetAllExceptions());
            }
        }

        protected virtual bool RemoveItemCommand_CanExecute()
        {
            return AllowEdit && !Equals(SelectedLookup, default(TLookup));
        }

        private void SelectItemCommand_Execute()
        {
            CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
        }

        private bool SelectItemCommand_CanExecute()
        {
            return !Equals(SelectedLookup, default(TLookup));
        }

        private async void RefreshCommand_Execute()
        {
            await this.LoadAsync();
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
        protected virtual void OnAfterSaveEntity(TEntity entity)
        {
            //обновление существующего айтема
            var lookup = Lookups.SingleOrDefault(x => Equals(x.Id, entity.Id));
            if (lookup != null)
            {
                lookup.Refresh(entity);
                return;
            }

            //добавление айтема не из списка
            lookup = (TLookup)Activator.CreateInstance(typeof(TLookup), entity);
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