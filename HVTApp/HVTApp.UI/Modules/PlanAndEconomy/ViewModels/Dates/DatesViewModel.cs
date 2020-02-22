using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.PlanAndEconomy.ViewModels
{
    public class DatesViewModel : ViewModelBaseCanExportToExcel
    {
        private IUnitOfWork _unitOfWork;
        private IValidatableChangeTrackingCollection<SalesUnitDates> _salesUnits;

        public ObservableCollection<SalesUnitDatesGroup> Groups { get; } = new ObservableCollection<SalesUnitDatesGroup>();

        public ICommand SaveCommand { get; }
        public ICommand ReloadCommand { get; }

        public bool AutoFillingDates { get; set; } = true;

        public DatesViewModel(IUnityContainer container) : base(container)
        {
            SaveCommand = new DelegateCommand(
                () =>
                {
                    _salesUnits.PropertyChanged -= SalesUnitsOnPropertyChanged;

                    //сохраняем изменения
                    _unitOfWork.SaveChanges();
                    //принимаем все изменения
                    _salesUnits.Where(x => x.IsChanged).ToList().ForEach(x => x.AcceptChanges());
                    //проверяем актуальность команды
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

                    _salesUnits.PropertyChanged += SalesUnitsOnPropertyChanged;
                },
                () => _salesUnits != null &&
                      _salesUnits.IsValid &&
                      _salesUnits.IsChanged);

            ReloadCommand = new DelegateCommand(Load);
        }

        public void Load()
        {
            _unitOfWork = Container.Resolve<IUnitOfWork>();

            var salesUnits = _unitOfWork.Repository<SalesUnit>().GetAll()
                .Where(x => !x.IsLoosen && x.OrderInTakeDate <= DateTime.Today)
                .OrderBy(salesUnit => salesUnit.EndProductionDateCalculated)
                .Select(salesUnit => new SalesUnitDates(salesUnit))
                .ToList();
            _salesUnits = new ValidatableChangeTrackingCollection<SalesUnitDates>(salesUnits);

            //подписываемся на изменение каждой сущности
            _salesUnits.PropertyChanged += SalesUnitsOnPropertyChanged;

            Groups.SelectMany(x => x.Units).ForEach(x => x.PropertyChanged -= UnitOnPropertyChanged);
            Groups.Clear();
            var groups = _salesUnits
                .GroupBy(x => new
                {
                    Cost = x.Model.Cost,
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
            Groups.SelectMany(x => x.Units).ForEach(x => x.PropertyChanged += UnitOnPropertyChanged);
        }

        private void UnitOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (AutoFillingDates && args.PropertyName == nameof(SalesUnitDates.ShipmentDate))
                ((SalesUnitDates)sender).SetCalculatedDates();
        }

        private void SalesUnitsOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();
        }
    }
}