using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.DataAccess.Annotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.UI.Lookup;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.ViewModels
{
    public abstract class BaseDetailsViewModel<TWrapper, TEntity, TAfterSaveEntityEvent> : 
        IDetailsViewModel<TWrapper, TEntity>, INotifyPropertyChanged, IDisposable
        where TEntity : class, IBaseEntity
        where TWrapper : class, IWrapper<TEntity>
        where TAfterSaveEntityEvent : PubSubEvent<TEntity>, new()
    {
        protected readonly IUnityContainer Container;
        protected readonly IEventAggregator EventAggregator;
        protected IUnitOfWork UnitOfWork;

        private TWrapper _item;

        protected BaseDetailsViewModel(IUnityContainer container)
        {
            Container = container;
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            EventAggregator = Container.Resolve<IEventAggregator>();

            SaveCommand = new DelegateCommand(SaveCommand_Execute, SaveCommand_CanExecute);
            InitCommands();
        }

        protected virtual void InitCommands()
        {
        }

        public async Task LoadAsync(TEntity entity)
        {
            Item = (TWrapper)Activator.CreateInstance(typeof(TWrapper), entity);
            await LoadOtherAsync();
        }

        public async Task LoadAsync(TWrapper wrapper, IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            Item = wrapper;
            await LoadOtherAsync();
        }

        public virtual async Task LoadAsync(Guid? id = null)
        {
            TEntity entity = (id == null)
                ? Activator.CreateInstance<TEntity>()
                : await UnitOfWork.GetRepository<TEntity>().GetByIdAsync(id.Value);

            Item = (TWrapper)Activator.CreateInstance(typeof(TWrapper), entity);

            await LoadOtherAsync();
        }

        protected virtual async Task LoadOtherAsync()
        {
        }

        public ICommand SaveCommand { get; }

        public TWrapper Item
        {
            get { return _item; }
            protected set
            {
                if (Equals(_item, value)) return;

                if (_item != null)
                    _item.PropertyChanged -= ItemOnPropertyChanged;

                _item = value;
                if (_item != null)
                    _item.PropertyChanged += ItemOnPropertyChanged;

                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;

        protected virtual async void SaveCommand_Execute()
        {
            if (await UnitOfWork.GetRepository<TEntity>().GetByIdAsync(Item.Model.Id) == null)
                UnitOfWork.GetRepository<TEntity>().Add(Item.Model);
            Item.AcceptChanges();
            await UnitOfWork.SaveChangesAsync();

            EventAggregator.GetEvent<TAfterSaveEntityEvent>().Publish(Item.Model);

            OnCloseRequested(new DialogRequestCloseEventArgs(true));
        }

        protected virtual bool SaveCommand_CanExecute()
        {
            return Item != null && Item.IsChanged && Item.IsValid;
        }

        private void ItemOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        protected void SelectAndSetWrapper<TEntity1, TLookup, TWrapper1>(IEnumerable<TEntity1> entities, string propertyName)
            where TEntity1 : class, IBaseEntity
            where TLookup : LookupItem<TEntity1>
            where TWrapper1 : WrapperBase<TEntity1>
        {
            var lookups = entities.Select(x => (TLookup)Activator.CreateInstance(typeof(TLookup), x));
            var entity = Container.Resolve<ISelectService>().SelectItem(lookups);
            var propertyValue = (TWrapper1)Item.GetType().GetProperty(propertyName).GetValue(Item);
            if (entity != null && !Equals(entity.Id, propertyValue?.Model.Id))
            {
                var wrapper = (TWrapper1)Activator.CreateInstance(typeof(TWrapper1), entity.Entity);
                Item.GetType().GetProperty(propertyName).SetValue(Item, wrapper);
                Item.GetType().GetProperty(propertyName + "Id").SetValue(Item, entity.Entity.Id);
            }
        }


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public void Dispose()
        {
            UnitOfWork?.Dispose();
        }

        protected virtual void OnCloseRequested(DialogRequestCloseEventArgs e)
        {
            CloseRequested?.Invoke(this, e);
        }
    }
}