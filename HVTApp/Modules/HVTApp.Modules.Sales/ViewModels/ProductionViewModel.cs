using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Converter;
using HVTApp.UI.Groups;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProductionViewModel : LoadableBindableBase
    {
        private SalesUnitsWrappersGroup _selectedPotentialWrappersGroup;
        private SalesUnitsWrappersGroup _selectedProductionWrappersGroup;

        public ObservableCollection<SalesUnitsWrappersGroup> ProductionGroups { get; } = new ObservableCollection<SalesUnitsWrappersGroup>();
        public ObservableCollection<SalesUnitsWrappersGroup> PotentialGroups { get; } = new ObservableCollection<SalesUnitsWrappersGroup>();

        public SalesUnitsWrappersGroup SelectedProductionWrappersGroup
        {
            get { return _selectedProductionWrappersGroup; }
            set
            {
                _selectedProductionWrappersGroup = value;
                OnPropertyChanged();
            }
        }

        public SalesUnitsWrappersGroup SelectedPotentialWrappersGroup
        {
            get { return _selectedPotentialWrappersGroup; }
            set
            {
                _selectedPotentialWrappersGroup = value;
                ((DelegateCommand)ProductUnitCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand ProductUnitCommand { get; }
        public ICommand ReloadCommand { get; }

        public ProductionViewModel(IUnityContainer container) : base(container)
        {
            ProductUnitCommand = new DelegateCommand(ProductUnitCommand_Execute, ProductUnitCommand_CanExecute);
            ReloadCommand = new DelegateCommand(async () => await LoadAsync());
        }

        private async void ProductUnitCommand_Execute()
        {
            //подтверждение
            var ms = Container.Resolve<IMessageService>();
            var q = "Подтвердить намерение разместить оборудование в производстве?";
            if (ms.ShowYesNoMessageDialog("Размещение в производстве", q) != MessageDialogResult.Yes) return;

            //размещение в производстве
            SelectedPotentialWrappersGroup.SignalToStartProduction = DateTime.Today;
            await UnitOfWork.SaveChangesAsync();

            //работа с видами
            ProductionGroups.Add(SelectedPotentialWrappersGroup);
            SelectedProductionWrappersGroup = SelectedPotentialWrappersGroup;
            if (PotentialGroups.Contains(SelectedPotentialWrappersGroup))
            {
                PotentialGroups.Remove(SelectedPotentialWrappersGroup);
            }
            else
            {
                foreach (var potentialGroup in PotentialGroups.ToList())
                {
                    if (potentialGroup.Groups.Contains(SelectedPotentialWrappersGroup))
                    {
                        potentialGroup.Groups.Remove(SelectedPotentialWrappersGroup);
                        if (!potentialGroup.Groups.Any())
                        {
                            PotentialGroups.Remove(potentialGroup);
                        }
                    }
                }
            }
        }

        private bool ProductUnitCommand_CanExecute()
        {
            return !string.IsNullOrEmpty(SelectedPotentialWrappersGroup?.TceRequest);
        }

        protected override async Task LoadedAsyncMethod()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Manager.Id == CommonOptions.User.Id);

            var production = salesUnits.Where(x => x.SignalToStartProduction != null && x.EndProductionDateCalculated >= DateTime.Today).ToList();
            var potential = salesUnits.Where(x => x.SignalToStartProduction == null && !x.IsLoosen && x.Project.InWork);

            var productionGroups = production.GroupBy(x => x, new SalesUnitsGroupsComparer()).Select(x => new SalesUnitsWrappersGroup(x.ToList())).OrderBy(x => x.EndProductionDateCalculated);
            ProductionGroups.Clear();
            ProductionGroups.AddRange(productionGroups);

            var potentialGroups = potential.GroupBy(x => x, new SalesUnitsGroupsComparer()).Select(x => new SalesUnitsWrappersGroup(x.ToList())).OrderBy(x => x.EndProductionDateCalculated);
            PotentialGroups.Clear();
            PotentialGroups.AddRange(potentialGroups);

            PotentialGroups.ForEach(x => x.PropertyChanged += (sender, args) => ((DelegateCommand)ProductUnitCommand).RaiseCanExecuteChanged());
        }

    }
}
