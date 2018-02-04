using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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
    public abstract class BaseDetailsViewModel<TWrapper, TEntity, TAfterSaveEntityEvent> : 
        IDetailsViewModel<TWrapper, TEntity>, INotifyPropertyChanged, IDisposable
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
            InitCommands();
        }

        protected virtual void InitCommands()
        {
        }

        public async Task LoadAsync(Guid? id = null)
        {
            TEntity entity = null;

            if (id == null)
                entity = Activator.CreateInstance<TEntity>();
            else
                entity = await UnitOfWork.GetRepository<TEntity>().GetByIdAsync(id.Value);

            Item = (TWrapper) Activator.CreateInstance(typeof(TWrapper), entity);

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
                if (_item != null) _item.PropertyChanged -= ItemOnPropertyChanged;
                _item = value;
                if (_item != null) _item.PropertyChanged += ItemOnPropertyChanged;
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

            CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
        }

        protected virtual bool SaveCommand_CanExecute()
        {
            return Item != null && Item.IsChanged && Item.IsValid;
        }

        private void ItemOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
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
    }
}