using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.PlanAndEconomy.ViewModels
{
    public class SalesUnitPayment
    {
        public SalesUnit SalesUnit { get; }
        public PaymentActual Payment { get; }
        public PaymentDocument PaymentDocument { get; }

        public SalesUnitPayment(SalesUnit salesUnit, PaymentActual payment, PaymentDocument paymentDocument)
        {
            SalesUnit = salesUnit;
            Payment = payment;
            PaymentDocument = paymentDocument;
        }
    }
}