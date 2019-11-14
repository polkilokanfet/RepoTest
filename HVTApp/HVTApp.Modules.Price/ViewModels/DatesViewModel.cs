using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Modules.PlanAndEconomy.ViewModels.Groups;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.PlanAndEconomy.ViewModels
{
    public class DatesViewModel : ViewModelBase
    {
        private IUnitOfWork _unitOfWork;
        private IValidatableChangeTrackingCollection<SalesUnitDates> _salesUnits;

        public ObservableCollection<SalesUnitDatesGroup> Groups { get; } = new ObservableCollection<SalesUnitDatesGroup>();

        public ICommand SaveCommand { get; }
        public ICommand ReloadCommand { get; }

        public DatesViewModel(IUnityContainer container) : base(container)
        {
            SaveCommand = new DelegateCommand(
                async () =>
                {
                    _salesUnits.PropertyChanged -= SalesUnitsOnPropertyChanged;

                    //��������� ���������
                    await _unitOfWork.SaveChangesAsync();
                    //��������� ��� ���������
                    _salesUnits.Where(x => x.IsChanged).ToList().ForEach(x => x.AcceptChanges());
                    //��������� ������������ �������
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

                    _salesUnits.PropertyChanged += SalesUnitsOnPropertyChanged;
                },
                () => _salesUnits != null &&
                      _salesUnits.IsValid &&
                      _salesUnits.IsChanged);
            ReloadCommand = new DelegateCommand(async () => await LoadAsync());
        }

        public async Task LoadAsync()
        {
            _unitOfWork = Container.Resolve<IUnitOfWork>();

            var salesUnits = (await _unitOfWork.Repository<SalesUnit>().GetAllAsync())
                .Where(x => x.OrderInTakeDate <= DateTime.Today)
                .Where(EditingRequired)
                .OrderBy(salesUnit => salesUnit.EndProductionDateCalculated)
                .Select(salesUnit => new SalesUnitDates(salesUnit))
                .ToList();
            _salesUnits = new ValidatableChangeTrackingCollection<SalesUnitDates>(salesUnits);

            //������������� �� ��������� ������ ��������
            _salesUnits.PropertyChanged += SalesUnitsOnPropertyChanged;

            Groups.Clear();
            var groups = _salesUnits
                .GroupBy(x => new
                {
                    Facility = x.Model.Facility.Id,
                    Product = x.Model.Product.Id,
                    Order = x.Model.Order?.Id,
                    Project = x.Model.Project.Id,
                    Specification = x.Model.Specification?.Id,
                    x.DeliveryDate,
                    x.EndProductionDate,
                    x.PickingDate,
                    x.RealizationDate,
                    x.ShipmentDate
                })
                .Select(x => new SalesUnitDatesGroup(x))
                .OrderBy(x => x.Units.First().Model.OrderInTakeDate);

            Groups.AddRange(groups);
        }

        private void SalesUnitsOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();
        }

        /// <summary>
        /// ������� ������� �������� ���������� � ������� ������.
        /// </summary>
        /// <param name="salesUnit"></param>
        /// <returns></returns>
        private bool EditingRequired(SalesUnit salesUnit)
        {
            return !salesUnit.DeliveryDate.HasValue ||
                   !salesUnit.EndProductionDate.HasValue ||
                   !salesUnit.PickingDate.HasValue ||
                   !salesUnit.RealizationDate.HasValue ||
                   !salesUnit.ShipmentDate.HasValue ||
                   string.IsNullOrEmpty(salesUnit.SerialNumber);
        }

    }
}