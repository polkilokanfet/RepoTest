using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Sales.Shippings
{
    public class ShippingViewModel : ViewModelBaseCanExportToExcelSaveCustomization
    {
        private IValidatableChangeTrackingCollection<ShippingUnitWrapper> _salesUnits;

        public ObservableCollection<ShippingGroup> ShippingGroups { get; } = new ObservableCollection<ShippingGroup>();

        public ICommand SaveCommand { get; }
        public ICommand ReloadCommand { get; }

        public ShippingViewModel(IUnityContainer container) : base(container)
        {
            SaveCommand = new DelegateCommand(
                () =>
                {
                    _salesUnits.AcceptChanges();
                    UnitOfWork.SaveChanges();
                },
                () => _salesUnits != null && _salesUnits.IsChanged && _salesUnits.IsValid);

            ReloadCommand = new DelegateCommand(Load);

            Load();
        }

        private void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            var salesUnits = ((ISalesUnitRepository)UnitOfWork.Repository<SalesUnit>()).GetUsersSalesUnits().Where(x => !x.IsLoosen);
            if(_salesUnits != null) _salesUnits.PropertyChanged -= OnSalesUnitPropertyChanged;
            _salesUnits = new ValidatableChangeTrackingCollection<ShippingUnitWrapper>(salesUnits.Select(x => new ShippingUnitWrapper(x)));
            _salesUnits.PropertyChanged += OnSalesUnitPropertyChanged;

            ShippingGroups.Clear();
            var groups = _salesUnits.GroupBy(x => new
            {
                FacilityId = x.Model.Facility.Id,
                ProductId = x.Model.Product.Id,
                OrderId = x.Model.Order?.Id,
                ProjectId = x.Model.Project.Id,
                SpecificationId = x.Model.Specification?.Id,
                ShipmentDateCalculated = x.Model.ShipmentDateCalculated
            }).OrderBy(x => x.Key.ShipmentDateCalculated);

            ShippingGroups.AddRange(groups.Select(x => new ShippingGroup(x)));
        }

        private void OnSalesUnitPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
    }
}