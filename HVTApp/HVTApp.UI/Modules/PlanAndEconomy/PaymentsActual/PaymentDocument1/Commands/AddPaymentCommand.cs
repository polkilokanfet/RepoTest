using System.Linq;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.PlanAndEconomy.PaymentsActual
{
    public class AddPaymentCommand : BasePaymentDocumentCommand
    {
        public AddPaymentCommand(PaymentDocumentViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            var sum = ViewModel.Item.DockSumWithVat;
            var date = ViewModel.Item.DockDate;

            var salesUnits = ViewModel.SelectedPotentialUnits.Cast<SalesUnit>().ToList();

            foreach (var selectedUnit in salesUnits)
            {
                ViewModel.Item.Payments.Add(new PaymentActualWrapper2(new PaymentActual(), selectedUnit, ViewModel.Item.Model));
                ViewModel.Potential.Remove(selectedUnit);
            }
            ViewModel.SelectedPotentialUnits = null;

            ViewModel.Item.DockSumWithVat = sum;
            ViewModel.Item.DockDate = date;
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.SelectedPotentialUnits != null;
        }
    }
}