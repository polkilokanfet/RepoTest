using System;
using System.ComponentModel;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.ViewModels
{
    public abstract class BaseDetailsViewModel<TWrapper, TEntity, TAfterSaveEntityEvent> : IDetailsViewModel<TWrapper, TEntity> 
        where TEntity : class, IBaseEntity
        where TWrapper : class, IWrapper<TEntity>
        where TAfterSaveEntityEvent : PubSubEvent<TEntity>, new()
    {
        protected readonly IUnityContainer Container;
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IEventAggregator EventAggregator;

        private TWrapper _item;

        protected BaseDetailsViewModel(IUnityContainer container)
        {
            Container = container;
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            EventAggregator = Container.Resolve<IEventAggregator>();

            SaveCommand = new DelegateCommand(SaveCommand_Execute, SaveCommand_CanExecute);
        }


        public TWrapper Item
        {
            get { return _item; }
            protected set
            {
                if (Equals(_item, value)) return;
                if (_item != null) _item.PropertyChanged -= ItemOnPropertyChanged;
                _item = value;
                if (_item != null) _item.PropertyChanged += ItemOnPropertyChanged;
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        public void Load(TWrapper wrapper = null)
        {
            if (wrapper == null)
            {
                var entity = Activator.CreateInstance<TEntity>();
                Item = (TWrapper) Activator.CreateInstance(typeof(TWrapper), entity);
            }
            else
            {
                Item = wrapper;
            }
        }


        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;



        public ICommand SaveCommand { get; }

        protected virtual async void SaveCommand_Execute()
        {
            if ((await UnitOfWork.GetRepository<TEntity>().GetByIdAsync(Item.Model.Id)) == null)
                UnitOfWork.GetRepository<TEntity>().Add(Item.Model);

            Item.AcceptChanges();
            UnitOfWork.Complete();
            CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
            EventAggregator.GetEvent<TAfterSaveEntityEvent>().Publish(Item.Model);
        }

        protected virtual bool SaveCommand_CanExecute()
        {
            return Item.IsChanged && Item.IsValid;
        }

        private void ItemOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
    }
}