using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
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

            var salesUnits = await UnitOfWork.GetRepository<SalesUnit>().GetAllAsync();
            _salesUnits?.ForEach(x => x.PropertyChanged -= OnSalesUnitPropertyChanged);
            _salesUnits = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(salesUnits.Select(x => new SalesUnitWrapper(x)));
            _salesUnits.ForEach(x => x.PropertyChanged += OnSalesUnitPropertyChanged);

            Groups.Clear();
            Groups.AddRange(Grouping(_salesUnits));
        }

        private void OnSalesUnitPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private IEnumerable<ShipmentUnitsGroup> Grouping(IEnumerable<SalesUnitWrapper> units)
        {
            var groups = units.GroupBy(x => new
            {
                Facility = x.Facility.Id,
                Product = x.Product.Id,
                Order = x.Order?.Id,
                Project = x.Project.Id,
                Specification = x.Specification?.Id,
                ShipmentDateCalculated = x.ShipmentDateCalculated
            }).OrderBy(x => x.Key.ShipmentDateCalculated);

            return groups.Select(x => new ShipmentUnitsGroup(x));
        }
    }
}