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
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.ViewModels
{
    public abstract class BaseListViewModel<TEntity, TLookup, TAfterSaveEntityEvent, TAfterSelectEntityEvent, TAfterRemoveEntityEvent> :
        ViewModelBaseCanExportToExcel, IBaseListViewModel<TEntity, TLookup>, ISelectServiceViewModel<TEntity>, IDisposable
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
        private object[] _selectedLookups;

        public bool CurrentUserIsAdmin { get; } = GlobalAppProperties.User.RoleCurrent == Role.Admin;

        protected BaseListViewModel(IUnityContainer container) : base(container)
        {
            DialogService = Container.Resolve<IDialogService>();
            MessageService = Container.Resolve<IMessageService>();
            EventAggregator = Container.Resolve<IEventAggregator>();

            Lookups = new ObservableCollection<TLookup>();

            NewItemCommand = new DelegateLogCommand(NewItemCommand_Execute, NewItemCommand_CanExecute);
            EditItemCommand = new DelegateLogCommand(EditItemCommand_Execute, EditItemCommand_CanExecute);
            RemoveItemCommand = new DelegateLogCommand(RemoveItemCommand_Execute, RemoveItemCommand_CanExecute);

            SelectItemCommand = new DelegateLogCommand(SelectItemCommand_Execute, SelectItemCommand_CanExecute);
            SelectItemsCommand = new DelegateLogCommand(
                () =>
                {
                    CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
                }, 
                () => SelectedItems != null);

            RefreshCommand = new DelegateLogCommand(RefreshCommand_Execute);

            UnionCommand = new DelegateLogCommand(
                () =>
                {
                    var selectedItem = container.Resolve<ISelectService>().SelectItem(SelectedItems);
                    if (selectedItem == null) return;

                    IUnitOfWork unitOfWork = container.Resolve<IUnitOfWork>();
                    var repository = unitOfWork.Repository<TEntity>();
                    TEntity mainItem = repository.GetById(selectedItem.Id);
                    List<TEntity> otherItems = SelectedItems.Select(entity => repository.GetById(entity.Id)).ToList();
                    otherItems.Remove(mainItem);

                    if (UnionItemsAction(unitOfWork, mainItem, otherItems) == false) return;

                    foreach (var otherItem in otherItems)
                    {
                        EventAggregator.GetEvent<TAfterRemoveEntityEvent>().Publish(otherItem);
                    }
                    repository.DeleteRange(otherItems);
                    unitOfWork.SaveChanges();

                    container.Resolve<IMessageService>().ShowOkMessageDialog("Информация", "Объединение успешно завершено!");
                },
                () => SelectedItems != null && SelectedItems.Count() > 1);

            EventAggregator.GetEvent<TAfterSaveEntityEvent>().Subscribe(OnAfterSaveEntity);
            EventAggregator.GetEvent<TAfterRemoveEntityEvent>().Subscribe(OnAfterRemoveEntity);

            this.LoadBegin += () => IsLoaded = false;
            this.Loaded += () => IsLoaded = true;

            InitSpecialCommands();
            SubscribesToEvents();
            LastActionInCtor();
        }

        /// <summary>
        /// Последнее действие в конструкторе
        /// </summary>
        protected virtual void LastActionInCtor() { }

        protected virtual bool UnionItemsAction(IUnitOfWork unitOfWork, TEntity mainItem, List<TEntity> otherItems)
        {
            return false;
        }

        public bool IsLoaded
        {
            get => _isLoaded;
            private set
            {
                _isLoaded = value;
                RaisePropertyChanged();
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
            get => _selectedLookup;
            set
            {
                if (Equals(_selectedLookup, value)) return;
                _selectedLookup = value;
                SelectedItem = _selectedLookup?.Entity;
                RaisePropertyChanged();
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
            get => _selectedItem;
            set
            {
                if (Equals(_selectedItem, value)) return;
                _selectedItem = value;
                SelectedLookup = _selectedItem == null 
                    ? null
                    : Lookups.Single(lookup => lookup.Entity.Id == _selectedItem.Id);
                RaisePropertyChanged();
            }
        }

        public object[] SelectedLookups
        {
            get => _selectedLookups;
            set
            {
                _selectedLookups = value;
                ((DelegateLogCommand)UnionCommand).RaiseCanExecuteChanged();
                ((DelegateLogCommand)SelectItemsCommand).RaiseCanExecuteChanged();
            }
        }

        public IEnumerable<TEntity> SelectedItems => _selectedItem == null || _selectedLookups == null
            ? null
            : _selectedLookups.Cast<TLookup>().Select(lookup => lookup.Entity);

        public ICommand SelectItemsCommand { get; protected set; }

        public IEnumerable<TLookup> Lookups { get; }
        protected ICollection<TLookup> LookupsCollection => (ICollection<TLookup>)Lookups;

        /// <summary>
        /// Загрузка всех Lookup'ов.
        /// </summary>
        /// <returns></returns>
        public virtual void Load()
        {
            LoadBegin?.Invoke();
            LookupsCollection.Clear();
            SelectedLookup = null;
            var entities = UnitOfWork.Repository<TEntity>().GetAllAsNoTracking();
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
            if(!Lookups.Contains(SelectedLookup)) 
                SelectedLookup = null;
            lookups.OrderBy(lookup => lookup).ForEach(LookupsCollection.Add);
            Loaded?.Invoke();
        }


        private event Action LoadBegin;

        public event Action Loaded;

        public event Action<TLookup> SelectedLookupChanged; 

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;

        #region Commands

        /// <summary>
        /// Создание новой сущности
        /// </summary>
        public ICommand NewItemCommand { get; protected set; }

        /// <summary>
        /// Редактирование сущности
        /// </summary>
        public ICommand EditItemCommand { get; protected set; }

        /// <summary>
        /// Удаление сущности
        /// </summary>
        public ICommand RemoveItemCommand { get; protected set; }

        /// <summary>
        /// Выбор сущности
        /// </summary>
        public ICommand SelectItemCommand { get; protected set; }

        /// <summary>
        /// Перезагрузка списка сущностей
        /// </summary>
        public ICommand RefreshCommand { get; protected set; }

        /// <summary>
        /// Объединение сущностей.
        /// </summary>
        public ICommand UnionCommand { get; protected set; }

        /// <summary>
        /// Генерация нового айтема (при создании нового).
        /// </summary>
        /// <returns></returns>
        protected virtual TEntity GetNewItem()
        {
            return Activator.CreateInstance<TEntity>();
        }
        protected void NewItemCommand_Execute()
        {
            Container.Resolve<IUpdateDetailsService>().UpdateDetails(GetNewItem());
        }

        protected virtual bool NewItemCommand_CanExecute()
        {
            return AllowEdit;
        }


        protected void EditItemCommand_Execute()
        {
            Container.Resolve<IUpdateDetailsService>().UpdateDetails<TEntity>(SelectedLookup.Id);
        }

        protected virtual bool EditItemCommand_CanExecute()
        {
            return AllowEdit && !Equals(SelectedLookup, default(TLookup));
        }

        protected void RemoveItemCommand_Execute()
        {
            var dr = MessageService.ShowYesNoMessageDialog("Удаление", $"Вы действительно хотите удалить \"{SelectedLookup.DisplayMember}\"?", defaultNo:true);
            if (dr != MessageDialogResult.Yes) return;

            try
            {
                if (Container.Resolve<IUnitOfWork>().RemoveEntity(SelectedLookup.Entity).OperationCompletedSuccessfully)
                {
                    LookupsCollection.Remove(SelectedLookup);
                    EventAggregator.GetEvent<TAfterRemoveEntityEvent>().Publish(SelectedLookup.Entity);
                }
            }
            catch (DbUpdateException e)
            {
                
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

        private void RefreshCommand_Execute()
        {
            this.Load();
        }

        protected virtual void InvalidateCommands()
        {
            ((DelegateLogCommand)NewItemCommand).RaiseCanExecuteChanged();
            ((DelegateLogCommand)EditItemCommand).RaiseCanExecuteChanged();
            ((DelegateLogCommand)RemoveItemCommand).RaiseCanExecuteChanged();
            ((DelegateLogCommand)SelectItemCommand).RaiseCanExecuteChanged();
        }
        #endregion

        /// <summary>
        /// Реакция на корректировку айтема или на создание нового
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
            if (Lookups is ObservableCollection<TLookup> collection)
            {
                collection.Insert(0, lookup);
            }
            else
            {
                LookupsCollection.Add(lookup);
            }

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