using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.Model.Wrapper.Groups;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class ShippingViewModel : LoadableBindableBase
    {
        private IValidatableChangeTrackingCollection<ShippingItemWrapper> _salesUnits;

        public ObservableCollection<ShipmentUnitsGroup> Groups { get; } = new ObservableCollection<ShipmentUnitsGroup>();

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
        }

        protected override void LoadedMethod()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            var salesUnits = ((ISalesUnitRepository)UnitOfWork.Repository<SalesUnit>()).GetUsersSalesUnits().Where(x => !x.IsLoosen);
            _salesUnits?.ForEach(x => x.PropertyChanged -= OnSalesUnitPropertyChanged);
            _salesUnits = new ValidatableChangeTrackingCollection<ShippingItemWrapper>(salesUnits.Select(x => new ShippingItemWrapper(x)));
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