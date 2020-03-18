using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.PlanAndEconomy.ViewModels
{
    public class SalesUnitPaymentGroup
    {
        private List<SalesUnit> _salesUnits;

        public List<SalesUnitPayment> SalesUnitPayments { get; }

        public SalesUnitPayment SalesUnitPayment => SalesUnitPayments.First();

        public int Amount => _salesUnits.Count;
        public bool IsPaid => _salesUnits.All(x => x.IsPaid);

        public double SumToPay => _salesUnits.Sum(x => x.Cost);
        public double Sum => SalesUnitPayments.Sum(x => x.Payment.Sum);
        public double SumWithVat => SalesUnitPayments.Sum(x => x.SumWithVat);

        public double PercentPaid => 100.0 - PercentNotPaid;
        public double PercentNotPaid => Math.Abs(SumToPay) < 0.000001 ? 0 : _salesUnits.Sum(x => x.SumNotPaid) / SumToPay * 100.0;

        public double SumNotPaidWithVat => _salesUnits.Sum(x => x.SumNotPaidWithVat);
        public DateTime LastDate => SalesUnitPayments.Select(x => x.Payment.Date).Max();

        public SalesUnitPaymentGroup(IEnumerable<SalesUnitPayment> salesUnitPayments)
        {
            SalesUnitPayments = salesUnitPayments
                .OrderByDescending(x => x.Payment.Date)
                .ThenBy(x => x.SalesUnit.OrderPosition)
                .ToList();

            _salesUnits = SalesUnitPayments.Select(x => x.SalesUnit).Distinct().ToList();
        }
    }

    public class SalesUnitPayment
    {
        public SalesUnit SalesUnit { get; }
        public PaymentActual Payment { get; }
        public PaymentDocument PaymentDocument { get; }

        public double SumWithVat => Payment.Sum * (100.0 + SalesUnit.Vat) / 100.0;
        public Contract Contract => SalesUnit.Specification?.Contract;
        public Company Contragent => Contract?.Contragent;
        public double Percent => Payment.Sum / SalesUnit.Cost * 100.0;
        public double PercentNotPaid => SalesUnit.SumNotPaid / SalesUnit.Cost * 100.0;

        public SalesUnitPayment(SalesUnit salesUnit, PaymentActual payment, PaymentDocument paymentDocument)
        {
            SalesUnit = salesUnit;
            Payment = payment;
            PaymentDocument = paymentDocument;
        }
    }
}