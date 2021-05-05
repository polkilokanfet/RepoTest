using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Modules.PlanAndEconomy.ViewModels;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.PlanAndEconomy.Dates;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.SupplyModule.ViewModels
{
    public class PickingDatesViewModel : ViewModelBaseCanExportToExcel
    {
        private IValidatableChangeTrackingCollection<SalesUnitDates> _salesUnits;

        public ObservableCollection<SalesUnitDatesGroup> Groups { get; } = new ObservableCollection<SalesUnitDatesGroup>();

        public DelegateLogCommand SaveCommand { get; }
        public DelegateLogCommand ReloadCommand { get; }

        public PickingDatesViewModel(IUnityContainer container) : base(container)
        {
            SaveCommand = new DelegateLogCommand(
                () =>
                {
                    _salesUnits.PropertyChanged -= SalesUnitsOnPropertyChanged;

                    //сохраняем изменения
                    UnitOfWork.SaveChanges();
                    //принимаем все изменения
                    _salesUnits.Where(x => x.IsChanged).ToList().ForEach(x => x.AcceptChanges());
                    //проверяем актуальность команды
                    SaveCommand.RaiseCanExecuteChanged();

                    _salesUnits.PropertyChanged += SalesUnitsOnPropertyChanged;
                },
                () => _salesUnits != null &&
                      _salesUnits.IsValid &&
                      _salesUnits.IsChanged);

            ReloadCommand = new DelegateLogCommand(Load);

            Load();
        }

        public void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            var salesUnits = UnitOfWork.Repository<SalesUnit>()
                .Find(x => !x.IsRemoved && !x.IsLoosen && !x.Product.ProductBlock.IsService && x.OrderInTakeDate <= DateTime.Today)
                .OrderBy(salesUnit => salesUnit.EndProductionDateCalculated)
                .Select(salesUnit => new SalesUnitDates(salesUnit))
                .ToList();
            _salesUnits = new ValidatableChangeTrackingCollection<SalesUnitDates>(salesUnits);

            //подписываемся на изменение каждой сущности
            _salesUnits.PropertyChanged += SalesUnitsOnPropertyChanged;

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
                    x.PickingDate
                })
                .Select(x => new SalesUnitDatesGroup(x))
                .OrderBy(x => x.Units.First().Model.EndProductionDateCalculated);

            Groups.AddRange(groups);
        }

        private void SalesUnitsOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            ( SaveCommand).RaiseCanExecuteChanged();
        }
    }
}