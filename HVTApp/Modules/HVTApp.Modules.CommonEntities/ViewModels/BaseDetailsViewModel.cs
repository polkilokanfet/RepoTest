using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.DataAccess.Annotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.ViewModels
{
    public abstract class BaseDetailsViewModel<TWrapper, TEntity, TAfterSaveEntityEvent> : IDetailsViewModel<TWrapper, TEntity>, INotifyPropertyChanged
        where TEntity : class, IBaseEntity
        where TWrapper : class, IWrapper<TEntity>
        where TAfterSaveEntityEvent : PubSubEvent<TEntity>, new()
    {
        protected readonly IUnityContainer Container;
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly WrapperDataService WrapperDataService;
        protected readonly IEventAggregator EventAggregator;

        private TWrapper _item;

        protected BaseDetailsViewModel(IUnityContainer container, TWrapper item)
        {
            Container = container;
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            EventAggregator = Container.Resolve<IEventAggregator>();
            WrapperDataService = Container.Resolve<WrapperDataService>();

            SaveCommand = new DelegateCommand(SaveCommand_Execute, SaveCommand_CanExecute);

            if (item == null)
            {
                var entity = Activator.CreateInstance<TEntity>();
                Item = (TWrapper) Activator.CreateInstance(typeof(TWrapper), entity);
            }
            else
            {
                Item = item;
            }
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

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;



        public ICommand SaveCommand { get; }

        protected virtual async void SaveCommand_Execute()
        {
            if ((await UnitOfWork.GetRepository<TEntity>().GetByIdAsync(Item.Model.Id)) == null)
                UnitOfWork.GetRepository<TEntity>().Add(Item.Model);

            CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
            //Item.AcceptChanges();
            //UnitOfWork.Complete();
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}