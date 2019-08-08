using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Groups;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ShippingViewModel : LoadableBindableBase
    {
        private IValidatableChangeTrackingCollection<SalesUnitWrapper> _salesUnits;

        public ObservableCollection<ShipmentUnitsGroup> Groups { get; } = new ObservableCollection<ShipmentUnitsGroup>();

        public ICommand SaveCommand { get; }
        public ICommand ReloadCommand { get; }

        public ShippingViewModel(IUnityContainer container) : base(container)
        {
            SaveCommand = new DelegateCommand(SaveCommand_Execute, () => _salesUnits != null && _salesUnits.IsChanged && _salesUnits.IsValid);
            ReloadCommand = new DelegateCommand(async () => await LoadAsync());
        }

        private async void SaveCommand_Execute()
        {
            _salesUnits.AcceptChanges();
            await UnitOfWork.SaveChangesAsync();
        }

        protected override async Task LoadedAsyncMethod()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            var salesUnits = await ((ISalesUnitRepository)UnitOfWork.Repository<SalesUnit>()).GetUsersSalesUnitsAsync();
            _salesUnits?.ForEach(x => x.PropertyChanged -= OnSalesUnitPropertyChanged);
            _salesUnits = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(salesUnits.Select(x => new SalesUnitWrapper(x)));
            _salesUnits.ForEach(x => x.PropertyChanged += OnSalesUnitPropertyChanged);

            Groups.Clear();
            Groups.AddRange(ShipmentUnitsGroup.Grouping(_salesUnits));
        }

        private void OnSalesUnitPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
    }
}