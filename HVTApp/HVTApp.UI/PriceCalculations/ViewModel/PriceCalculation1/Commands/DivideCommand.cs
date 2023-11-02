using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.PriceCalculations.ViewModel.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1.Commands
{
    public class DivideCommand : BasePriceCalculationCommand
    {
        public DivideCommand(PriceCalculationViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            var dialogResult = MessageService.ConfirmationDialog("Разбиение", "Действительно хотите разбить выбранную строку?", defaultNo: true);
            if (dialogResult == false) return;

            var selectedItem = (PriceCalculationItem2Wrapper)ViewModel.SelectedItem;
            var salesUnit = selectedItem.SalesUnits.First();

            var salesUnitsToDivide = selectedItem.SalesUnits.ToList();
            salesUnitsToDivide.Remove(salesUnit);

            foreach (var unit in salesUnitsToDivide)
            {
                selectedItem.SalesUnits.Remove(unit);

                var priceCalculationItem = new PriceCalculationItem
                {
                    OrderInTakeDate = selectedItem.OrderInTakeDate,
                    RealizationDate = selectedItem.RealizationDate,
                    PaymentConditionSet = selectedItem.PaymentConditionSet.Model
                };
                var priceCalculationItemWrapper = new PriceCalculationItem2Wrapper(priceCalculationItem);
                priceCalculationItemWrapper.SalesUnits.Add(unit);
                foreach (var structureCost in selectedItem.StructureCosts)
                {
                    var sc = new StructureCost
                    {
                        Comment = structureCost.Comment,
                        AmountNumerator = structureCost.AmountNumerator,
                        AmountDenomerator = structureCost.AmountDenomerator,
                        Number = structureCost.Number,
                        UnitPrice = structureCost.UnitPrice
                    };
                    priceCalculationItemWrapper.StructureCosts.Add(new StructureCost2Wrapper(sc));
                }

                ViewModel.PriceCalculationWrapper.PriceCalculationItems.Add(priceCalculationItemWrapper);
            }
        }

        protected override bool CanExecuteMethod()
        {
            return !ViewModel.IsStarted && 
                   ViewModel.SelectedItem is PriceCalculationItem2Wrapper &&
                   ((PriceCalculationItem2Wrapper) ViewModel.SelectedItem).Amount > 1;
        }
    }
}