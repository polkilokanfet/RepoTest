using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Comparers;
using HVTApp.Model.POCOs;
using HVTApp.UI.Groups;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProductionViewModel : LoadableBindableBase
    {
        public SalesUnitsWrappersGroupsContainer ProductionGroups { get; } = new SalesUnitsWrappersGroupsContainer();
        public SalesUnitsWrappersGroupsContainer PotentialGroups { get; } = new SalesUnitsWrappersGroupsContainer();

        public ICommand ProductUnitCommand { get; }
        public ICommand ReloadCommand { get; }

        public ProductionViewModel(IUnityContainer container) : base(container)
        {
            //реакция на смену выбранной потенциальной группы производства
            PotentialGroups.SelectedGroupChanged += group => { ((DelegateCommand)ProductUnitCommand).RaiseCanExecuteChanged(); };

            ReloadCommand = new DelegateCommand(async () => { await LoadAsync(); });

            ProductUnitCommand = new DelegateCommand(ProductUnitCommand_Execute, () => PotentialGroups.SelectedGroup != null);
        }

        private async void ProductUnitCommand_Execute()
        {
            //подтверждение
            var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Размещение в производстве", "Разместить оборудование в производстве?");
            if (dr != MessageDialogResult.Yes) return;

            //размещение в производстве
            //фиксируем дату подачи сигнала на включение в план производства
            PotentialGroups.SelectedGroup.SignalToStartProduction = DateTime.Today;
            //сохранение изменений
            await UnitOfWork.SaveChangesAsync();

            //работа с видами
            ProductionGroups.Add(PotentialGroups.SelectedGroup);
            ProductionGroups.SelectedGroup = PotentialGroups.SelectedGroup;
            if (PotentialGroups.Contains(PotentialGroups.SelectedGroup))
            {
                PotentialGroups.Remove(PotentialGroups.SelectedGroup);
            }
            else
            {
                foreach (var potentialGroup in PotentialGroups.ToList())
                {
                    if (potentialGroup.Groups.Contains(PotentialGroups.SelectedGroup))
                    {
                        potentialGroup.Groups.Remove(PotentialGroups.SelectedGroup);
                        if (!potentialGroup.Groups.Any())
                        {
                            PotentialGroups.Remove(potentialGroup);
                        }
                    }
                }
            }
        }

        protected override async Task LoadedAsyncMethod()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            var salesUnits = (await ((ISalesUnitRepository)UnitOfWork.Repository<SalesUnit>()).GetUsersSalesUnitsAsync()).ToList();

            //оборудование, размещенное в производстве
            var productionGroups = salesUnits
                .Where(x => x.SignalToStartProduction != null && x.EndProductionDateCalculated >= DateTime.Today)
                .GroupBy(x => x, new SalesUnitsGroupsComparer())
                .Select(x => new SalesUnitsWrappersGroup(x.ToList()))
                .OrderBy(x => x.EndProductionDateCalculated);

            ProductionGroups.ClearAndAddRange(productionGroups);

            //оборудование, поторое может быть размещено в производстве
            var potentialGroups = salesUnits
                .Where(x => x.SignalToStartProduction == null && !x.IsLoosen && x.Project.InWork)
                .GroupBy(x => x, new SalesUnitsGroupsComparer())
                .Select(x => new SalesUnitsWrappersGroup(x.ToList()))
                .OrderBy(x => x.EndProductionDateCalculated);

            PotentialGroups.ClearAndAddRange(potentialGroups);

            PotentialGroups.ForEach(x => x.PropertyChanged += (sender, args) =>
            {
                ((DelegateCommand) ProductUnitCommand).RaiseCanExecuteChanged();
            });
        }

    }
}