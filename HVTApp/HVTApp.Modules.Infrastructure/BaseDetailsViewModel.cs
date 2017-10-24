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
    public abstract class BaseDetailsViewModel<TWrapper> : IDetailsViewModel<TWrapper> 
        where TWrapper : class, IWrapper<IBaseEntity>
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
            throw new NotImplementedException();
        }

        public async Task LoadAsync(Guid id)
        {
            var company = await UnitOfWork.GetEntityByIdAsync<Company>(id);
            Item = new CompanyWrapper(company) as TWrapper;
        }


        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;



        public ICommand SaveCommand { get; }

        private void SaveCommand_Execute()
        {
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