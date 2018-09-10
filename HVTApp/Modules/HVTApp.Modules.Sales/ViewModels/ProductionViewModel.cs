using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProductionViewModel : LoadableBindableBase
    {
        private List<SalesUnit> _allSalesUnits;
        private ProductUnitsGroup _selectedPotentialGroup;
        private ProductUnitsGroup _selectedProductionGroup;

        public ObservableCollection<ProductUnitsGroup> ProductionGroups { get; } = new ObservableCollection<ProductUnitsGroup>();
        public ObservableCollection<ProductUnitsGroup> PotentialGroups { get; } = new ObservableCollection<ProductUnitsGroup>();

        public ProductUnitsGroup SelectedProductionGroup
        {
            get { return _selectedProductionGroup; }
            set
            {
                _selectedProductionGroup = value;
                OnPropertyChanged();
            }
        }

        public ProductUnitsGroup SelectedPotentialGroup
        {
            get { return _selectedPotentialGroup; }
            set
            {
                _selectedPotentialGroup = value;
                ((DelegateCommand)ProductUnitCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand ProductUnitCommand { get; }
        public ICommand ReloadCommand { get; }

        public ProductionViewModel(IUnityContainer container) : base(container)
        {
            ProductUnitCommand = new DelegateCommand(ProductUnitCommand_Execute, () => SelectedPotentialGroup != null);
            ReloadCommand = new DelegateCommand(async () => await LoadAsync());
        }

        private async void ProductUnitCommand_Execute()
        {
            //подтверждение
            var ms = Container.Resolve<IMessageService>();
            var q = "Подтвердить намерение разместить оборудование в производстве?";
            if (ms.ShowYesNoMessageDialog("Размещение в производстве", q) != MessageDialogResult.Yes) return;

            //размещение в производстве
            SelectedPotentialGroup.ProdutGroup();
            await UnitOfWork.SaveChangesAsync();

            //работа с видами
            ProductionGroups.Add(SelectedPotentialGroup);
            SelectedProductionGroup = SelectedPotentialGroup;
            if (PotentialGroups.Contains(SelectedPotentialGroup))
            {
                PotentialGroups.Remove(SelectedPotentialGroup);
            }
            else
            {
                List<ProductUnitsGroup> remove = new List<ProductUnitsGroup>();
                foreach (var potentialGroup in PotentialGroups)
                {
                    if (potentialGroup.Groups.Contains(SelectedPotentialGroup))
                    {
                        potentialGroup.Groups.Remove(SelectedPotentialGroup);
                        if (!potentialGroup.Groups.Any())
                        {
                            remove.Add(potentialGroup);
                        }
                    }
                }
                remove.ForEach(x => PotentialGroups.Remove(x));
            }
        }

        protected override async Task LoadedAsyncMethod()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            _allSalesUnits = await UnitOfWork.GetRepository<SalesUnit>().GetAllAsync();

            var production = _allSalesUnits.Where(x => x.SignalToStartProduction != null).ToList();
            var potential = _allSalesUnits.Except(production).Where(x => !x.IsLoosen && x.Project.HighProbability);

            ProductionGroups.Clear();
            ProductionGroups.AddRange(Grouping(production));

            PotentialGroups.Clear();
            PotentialGroups.AddRange(Grouping(potential));
        }

        private IEnumerable<ProductUnitsGroup> Grouping(IEnumerable<SalesUnit> units)
        {
            var groups = units.GroupBy(x => new
            {
                x.Facility,
                x.Product,
                x.Order,
                x.Project,
                x.Specification,
                x.EndProductionDateCalculated
            }).OrderBy(x => x.Key.EndProductionDateCalculated);

            return groups.Select(x => new ProductUnitsGroup(x));
        }
    }

    public class ProductUnitsGroup
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

        public ObservableCollection<ProductUnitsGroup> Groups { get; } = new ObservableCollection<ProductUnitsGroup>();

        public ProductUnitsGroup(IEnumerable<SalesUnit> units)
        {
            _units = units.ToList();
            _unit = _units.First();

            if (_units.Count > 1)
            {
                Groups.AddRange(_units.Select(x => new ProductUnitsGroup(new[] {x})));
            }
        }

        public void ProdutGroup()
        {
            if (Groups.Any())
            {
                Groups.ForEach(x => x.ProdutGroup());
                return;
            }

            foreach (var unit in _units)
            {
                unit.SignalToStartProduction = DateTime.Today;
            }
        }
    }
}
