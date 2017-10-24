using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Infrastructure
{
    public abstract class BaseDetailsViewModel<TWrapper, TEntity> : IDetailsViewModel<TWrapper, TEntity> 
        where TEntity : class, IBaseEntity
        where TWrapper : class, IWrapper<TEntity> 
    {
        protected readonly IUnityContainer Container;
        protected readonly IUnitOfWork UnitOfWork;
        private TWrapper _item;

        protected BaseDetailsViewModel(IUnityContainer container)
        {
            Container = container;
            UnitOfWork = Container.Resolve<IUnitOfWork>();

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

        public async Task LoadAsync()
        {
            var entity = Activator.CreateInstance<TEntity>();
            Item = (TWrapper)Activator.CreateInstance(typeof(TWrapper), entity);
        }

        public async Task LoadAsync(Guid id)
        {
            var entity = await UnitOfWork.GetEntityByIdAsync<TEntity>(id);
            Item = (TWrapper)Activator.CreateInstance(typeof(TWrapper), entity);
        }


        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;



        public ICommand SaveCommand { get; }

        private void SaveCommand_Execute()
        {
            UnitOfWork.Complete();
            CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
        }

        private bool SaveCommand_CanExecute()
        {
            return Item.IsChanged && Item.IsValid;
        }

        private void ItemOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
    }
}