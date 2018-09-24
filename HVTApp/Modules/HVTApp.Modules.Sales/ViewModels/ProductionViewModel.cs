using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Groups;
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
            SelectedPotentialGroup.ProductingGroup();
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
            _allSalesUnits = await UnitOfWork.Repository<SalesUnit>().GetAllAsync();

            var production = _allSalesUnits.Where(x => x.SignalToStartProduction != null).ToList();
            var potential = _allSalesUnits.Except(production).Where(x => !x.IsLoosen && x.Project.InWork);

            ProductionGroups.Clear();
            ProductionGroups.AddRange(ProductUnitsGroup.Grouping(production));

            PotentialGroups.Clear();
            PotentialGroups.AddRange(ProductUnitsGroup.Grouping(potential));
        }

    }
}
