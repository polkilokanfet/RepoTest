using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.PlanAndEconomy.ViewModels
{
    public class Payment1
    {
        public SalesUnit SalesUnit { get; }
        public PaymentPlanned PaymentPlanned { get; }

        public Payment1(SalesUnit salesUnit, PaymentPlanned paymentPlanned)
        {
            SalesUnit = salesUnit;
            PaymentPlanned = paymentPlanned;
        }
    }
}