using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace HVTApp.UI.ViewModels
{
    public class BaseLookupListViewModel<TLookupItem, TEntity, TDelailsViewModel, TAfterSaveEntityEvent> :
        BindableBase, IBaseLookupListViewModel<TLookupItem>
        where TEntity : class, IBaseEntity
        where TLookupItem : class, ILookupItem, new()
        where TDelailsViewModel : IDetailsViewModel<IWrapper<TEntity>, TEntity>
        where TAfterSaveEntityEvent : PubSubEvent<TEntity>, new()
    {
        protected readonly IUnityContainer Container;
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly ILookupDataService<TLookupItem> LookupDataService;
        protected readonly IEventAggregator EventAggregator;
        protected readonly IDialogService DialogService;

        private TLookupItem _selectedItem;
        private bool _loaded = false;

        public BaseLookupListViewModel(IUnityContainer container, ILookupDataService<TLookupItem> lookupDataService)
        {
            Container = container;
            LookupDataService = lookupDataService;
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            EventAggregator = Container.Resolve<IEventAggregator>();
            DialogService = Container.Resolve<IDialogService>();

            Items = new ObservableCollection<TLookupItem>();

            NewItemCommand = new DelegateCommand(NewItemCommand_ExecuteAsync, NewItemCommand_CanExecute);
            EditItemCommand = new DelegateCommand(EditItemCommand_ExecuteAsync, EditItemCommand_CanExecute);
            RemoveItemCommand = new DelegateCommand(RemoveItemCommand_Execute, RemoveItemCommand_CanExecute);
            SelectItemCommand = new DelegateCommand(SelectItemCommand_Execute, SelectItemCommand_CanExecute);

            LoadedCommand = new DelegateCommand(async () =>
            {
                if (!_loaded) await LoadAsync();
                _loaded = true;
            });

            EventAggregator.GetEvent<TAfterSaveEntityEvent>().Subscribe(OnAfterSaveEntity);
        }

        private void OnAfterSaveEntity(TEntity entity)
        {
            var lookup = Items.SingleOrDefault(x => x.Id == entity.Id);
            if (lookup != null)
            {
                lookup.DisplayMember = entity.ToString();
            }
            else
            {
                lookup = new TLookupItem { Id = entity.Id, DisplayMember = entity.ToString() };
                Items.Add(lookup);
            }
        }

        public ICollection<TLookupItem> Items { get; }

        public TLookupItem SelectedItem
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

        public async Task LoadAsync()
        {
            var lookups = await LookupDataService.GetAllLookupsAsync();
            Items.Clear();
            lookups.ToList().ForEach(Items.Add);
        }

        #region Commands
        public ICommand NewItemCommand { get; }
        public bool AutoLoadItems { get; set; }
        public ICommand EditItemCommand { get; }
        public ICommand RemoveItemCommand { get; }
        public ICommand SelectItemCommand { get; }
        public DelegateCommand LoadedCommand { get; set; }



        protected void NewItemCommand_ExecuteAsync()
        {
            var viewModel = Container.Resolve<TDelailsViewModel>();
            DialogService.ShowDialog(viewModel);
        }

        protected virtual bool NewItemCommand_CanExecute()
        {
            return true;
        }


        protected async void EditItemCommand_ExecuteAsync()
        {
            var viewModel = Container.Resolve<TDelailsViewModel>();
            //TODO: fix it
            //await viewModel.LoadItemsAsync(SelectedItem.Id);
            var dialogResult = DialogService.ShowDialog(viewModel);
            if (!dialogResult.HasValue || !dialogResult.Value)
                viewModel.Item.RejectChanges();
        }

        protected virtual bool EditItemCommand_CanExecute()
        {
            return SelectedItem != null;
        }

        protected async void RemoveItemCommand_Execute()
        {
            var repo = UnitOfWork.GetRepository<TEntity>();
            var entityToRemove = await repo.GetByIdAsync(SelectedItem.Id);
            if (entityToRemove != null)
            {
                UnitOfWork.GetRepository<TEntity>().Delete(entityToRemove);
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


        private void InvalidateCommands()
        {
            ((DelegateCommand)NewItemCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)EditItemCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)RemoveItemCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)SelectItemCommand).RaiseCanExecuteChanged();
        }
        #endregion


    }
}