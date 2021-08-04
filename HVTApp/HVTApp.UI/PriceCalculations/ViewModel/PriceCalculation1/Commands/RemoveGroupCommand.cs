using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.UI.PriceCalculations.ViewModel.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1.Commands
{
    public class RemoveGroupCommand : BasePriceCalculationCommand
    {
        public RemoveGroupCommand(PriceCalculationViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            var result = MessageService.ShowYesNoMessageDialog("”даление", "ƒействительно хотите удалить из расчета группу оборудовани€?", defaultNo: true);
            if (result != MessageDialogResult.Yes) return;

            var selectedGroup = (PriceCalculationItem2Wrapper)ViewModel.SelectedItem;

            var salesUnits = selectedGroup.SalesUnits.ToList();

            //единицы, которы нельз€ удалить из расчета, т.к. они размещены в производстве
            var salesUnitsNotForRemove = salesUnits
                .Where(x => x.Model.SignalToStartProduction.HasValue)
                .Where(x => x.Model.ActualPriceCalculationItem(ViewModel.UnitOfWork1)?.Id == selectedGroup.Model.Id)
                .ToList();

            if (salesUnitsNotForRemove.Any())
            {
                MessageService.ShowOkMessageDialog("”даление", "¬ы не можете удалить некоторые строки, т.к. они размещены в производстве.");

                var salesUnitsToRemove = salesUnits.Except(salesUnitsNotForRemove).ToList();
                salesUnitsToRemove.ForEach(x => selectedGroup.SalesUnits.Remove(x));
                if (!selectedGroup.SalesUnits.Any())
                    ViewModel.PriceCalculationWrapper.PriceCalculationItems.Remove(selectedGroup);
            }
            else
            {
                ViewModel.PriceCalculationWrapper.PriceCalculationItems.Remove(selectedGroup);
            }
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.SelectedItem is PriceCalculationItem2Wrapper && !ViewModel.IsStarted;
        }
    }
}