using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Services.MessageService;
using HVTApp.UI.Lookup;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace HVTApp.UI.ViewModels
{
    public abstract class BaseListServiceViewModel<TEntity, TLookup, TAfterSaveEntityEvent> :
        BindableBase, IBaseListViewModel<TLookup>, ISelectServiceViewModel<TEntity>, IDisposable
        where TEntity : class, IBaseEntity
        where TLookup : ILookupItem
        where TAfterSaveEntityEvent : PubSubEvent<TEntity>, new()
    {
        protected readonly IUnityContainer Container;
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IDialogService DialogService;
        protected readonly IMessageService MessageService;

        private TLookup _selectedLookup;

        public IEnumerable<TLookup> Lookups { get; }

        protected BaseListServiceViewModel(IUnityContainer container)
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
            return (await UnitOfWork.GetRepository<TEntity>().GetAllAsNoTrackingAsync());
            //.Select(x => (TWrapper) Activator.CreateInstance(typeof(TWrapper), x));
        }

        public virtual async Task LoadAsync()
        {
            var lookups = Lookups as ICollection<TLookup>;
            lookups.Clear();
            var items = (await GetItems()).ToList();
            foreach (var item in items)
            {
                lookups.Add(GenerateLookup(item));
            }
        }

        protected virtual TLookup GenerateLookup(TEntity entity)
        {
            var lookup = Activator.CreateInstance<TLookup>();
            lookup.Refresh(entity);
            lookup.DisplayMember = entity.ToString();
            return lookup;
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
            if (MessageService.ShowYesNoMessageDialog("Удалить", $"Вы действительно хотите удалить '{SelectedLookup.DisplayMember}'?") != MessageDialogResult.Yes)
                return;

            var entityToRemove = await UnitOfWork.GetRepository<TEntity>().GetByIdAsync(SelectedLookup.Id);
            if (entityToRemove != null)
            {
                UnitOfWork.GetRepository<TEntity>().Delete(entityToRemove);
                UnitOfWork.Complete();
            }
            (Lookups as ICollection<TLookup>).Remove(SelectedLookup);
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


        protected virtual void OnAfterSaveEntity(TEntity entity)
        {
            var lookup = Lookups.Single(x => Equals(x.Id, entity.Id));
            lookup.Refresh(entity);
        }

        public void Dispose()
        {
            UnitOfWork?.Dispose();
        }
    }
}