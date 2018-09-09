using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Mvvm;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProductionViewModel : BindableBase
    {
        private readonly IUnityContainer _container;
        private IUnitOfWork _unitOfWork;
        private List<SalesUnit> _allSalesUnits;

        public ObservableCollection<UnutsGroup> Groups { get; } = new ObservableCollection<UnutsGroup>();

        public ProductionViewModel(IUnityContainer container)
        {
            _container = container;
        }

        public async Task LoadAsync()
        {
            _unitOfWork = _container.Resolve<IUnitOfWork>();
            _allSalesUnits = await _unitOfWork.GetRepository<SalesUnit>().GetAllAsNoTrackingAsync();

            var groups = _allSalesUnits.GroupBy(x => new
            {
                x.Facility,
                x.Product,
                x.Order,
                x.Project,
                x.Specification,
                x.EndProductionDateCalculated
            }).OrderBy(x => x.Key.EndProductionDateCalculated);

            Groups.Clear();
            Groups.AddRange(groups.Select(x => new UnutsGroup(x)));
        }
    }

    public class UnutsGroup
    {
        private readonly List<SalesUnit> _units;
        private readonly SalesUnit _unit;

        public Facility Facility => _unit.Facility;
        public ProductType ProductType => _unit.Product.ProductType;
        public string ProductDesignation => _unit.Product.Designation;
        public int Amount => _units.Count;
        public string Order => _unit.Order?.Number;
        public Company Company => _unit.Specification?.Contract.Contragent;
        public string Specification => _unit.Specification?.Number;
        public string Contract => _unit.Specification?.Contract.Number;
        public DateTime EndProductionDate => _unit.EndProductionDateCalculated;

        public ObservableCollection<UnutsGroup> Groups { get; } = new ObservableCollection<UnutsGroup>();

        public UnutsGroup(IEnumerable<SalesUnit> units)
        {
            _units = units.ToList();
            _unit = _units.First();

            if (_units.Count > 1)
            {
                Groups.AddRange(_units.Select(x => new UnutsGroup(new[] {x})));
            }
        }
    }
}
