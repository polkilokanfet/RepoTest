using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.PlanAndEconomy.Dates
{
    public class DatesViewModel : LoadableExportableViewModel
    {
        private IUnitOfWork _unitOfWork;
        private IValidatableChangeTrackingCollection<SalesUnitDates> _salesUnits;

        public ObservableCollection<SalesUnitDatesGroup> Groups { get; } = new ObservableCollection<SalesUnitDatesGroup>();

        public ICommand SaveCommand { get; }

        public bool AutoFillingDates { get; set; } = true;

        public DatesViewModel(IUnityContainer container) : base(container)
        {
            SaveCommand = new DelegateCommand(
                () =>
                {
                    _salesUnits.PropertyChanged -= SalesUnitsOnPropertyChanged;

                    //принимаем все изменения
                    _salesUnits.AcceptChanges();
                    //сохраняем изменения
                    _unitOfWork.SaveChanges();
                    //проверяем актуальность команды
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

                    _salesUnits.PropertyChanged += SalesUnitsOnPropertyChanged;
                },
                () => _salesUnits != null &&
                      _salesUnits.IsValid &&
                      _salesUnits.IsChanged);
        }

        private IOrderedEnumerable<SalesUnitDatesGroup> _groups;

        protected override void GetData()
        {
            _unitOfWork = Container.Resolve<IUnitOfWork>();

            var salesUnits = ((ISalesUnitRepository)_unitOfWork.Repository<SalesUnit>()).GetForDatesView()
                .Where(salesUnit => salesUnit.Order != null || salesUnit.OrderInTakeDate <= DateTime.Today)
                .OrderBy(salesUnit => salesUnit.EndProductionDateCalculated)
                .Select(salesUnit => new SalesUnitDates(salesUnit))
                .ToList();
            _salesUnits = new ValidatableChangeTrackingCollection<SalesUnitDates>(salesUnits);

            //подписываемся на изменение каждой сущности
            _salesUnits.PropertyChanged += SalesUnitsOnPropertyChanged;

            _groups = _salesUnits
                .GroupBy(salesUnitDates => new
                {
                    Cost = salesUnitDates.Model.Cost,
                    Facility = salesUnitDates.Model.Facility.Id,
                    Product = salesUnitDates.Model.Product.Id,
                    Order = salesUnitDates.Model.Order?.Id,
                    Project = salesUnitDates.Model.Project.Id,
                    Specification = salesUnitDates.Model.Specification?.Id,
                    salesUnitDates.DeliveryDate,
                    salesUnitDates.EndProductionDate,
                    salesUnitDates.PickingDate,
                    salesUnitDates.RealizationDate,
                    salesUnitDates.ShipmentDate
                })
                .Select(x => new SalesUnitDatesGroup(x))
                .OrderBy(x => x.Units.First().Model.OrderInTakeDate);
        }

        protected override void AfterGetData()
        {
            Groups.SelectMany(x => x.Units).ForEach(x => x.PropertyChanged -= UnitOnPropertyChanged);
            Groups.Clear();
            Groups.AddRange(_groups);
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