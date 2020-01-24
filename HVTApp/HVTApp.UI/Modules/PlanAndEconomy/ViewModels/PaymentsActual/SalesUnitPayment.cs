using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.PlanAndEconomy.ViewModels
{
    public class SalesUnitPayment
    {
        public SalesUnit SalesUnit { get; }
        public PaymentActual Payment { get; }
        public PaymentDocument PaymentDocument { get; }

        public double SumWithVat => Payment.Sum * (100.0 + SalesUnit.Vat) / 100.0;
        public Contract Contract => SalesUnit.Specification?.Contract;
        public Company Contragent => Contract?.Contragent;
        public double Percent => Payment.Sum / SalesUnit.Cost * 100.0;

        public SalesUnitPayment(SalesUnit salesUnit, PaymentActual payment, PaymentDocument paymentDocument)
        {
            SalesUnit = salesUnit;
            Payment = payment;
            PaymentDocument = paymentDocument;
        }
    }
}