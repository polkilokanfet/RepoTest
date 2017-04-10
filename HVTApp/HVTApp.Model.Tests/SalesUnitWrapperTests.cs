using System;
using System.Linq;
using HVTApp.Model.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class SalesUnitWrapperTests
    {
        private SalesUnitWrapper _salesUnitWrapper;

        [TestInitialize]
        public void InitialMethod()
        {
            Product product = new Product();
            product.Prices.Add(new SumOnDate {Date = DateTime.Today, SumAndVat = new SumAndVat {Sum = 50, Vat = 10} });
            ProductionUnit productionUnit = new ProductionUnit {Product = product};

            var unit = new SalesUnit { ProductionUnit = productionUnit, Cost = new SumAndVat { Sum = 100, Vat = 10 } };
            unit.PaymentsConditions.Add(new PaymentCondition { PartInPercent = 50, DaysToPoint = 2, PaymentConditionPoint = PaymentConditionPoint.ProductionStart });
            unit.PaymentsConditions.Add(new PaymentCondition { PartInPercent = 30, DaysToPoint = 2, PaymentConditionPoint = PaymentConditionPoint.ProductionEnd });
            unit.PaymentsConditions.Add(new PaymentCondition { PartInPercent = 20, DaysToPoint = 2, PaymentConditionPoint = PaymentConditionPoint.Delivery });
            _salesUnitWrapper = SalesUnitWrapper.GetWrapper(unit);
        }

        [TestMethod]
        public void SalesUnitWrapperReloadPlannedPayments()
        {
            _salesUnitWrapper.ReloadPaymentsPlanned();

            var cost = _salesUnitWrapper.Cost.SumWithVat;
            var paymentsConditions = _salesUnitWrapper.PaymentsConditions;
            var paymentsPlanned = _salesUnitWrapper.PaymentsPlanned;
            var paymentsActual = _salesUnitWrapper.PaymentsActual;
            var paymentsAll = paymentsActual.Union(paymentsPlanned);

            Assert.AreEqual(paymentsPlanned.Count, paymentsConditions.Count); //количество плановых и фактических платежей совпадает
            Assert.IsTrue(Math.Abs(cost - paymentsPlanned.Sum(x => x.SumAndVat.SumWithVat)) < 0.0001);
            Assert.IsTrue(Math.Abs(cost - paymentsAll.Sum(x => x.SumAndVat.SumWithVat)) < 0.0001);

            var payment = new Payment {SumAndVat = new SumAndVat { Sum = _salesUnitWrapper.Cost.Sum / 2 } };
            _salesUnitWrapper.PaymentsActual.Add(PaymentWrapper.GetWrapper(payment));
            Assert.IsTrue(Math.Abs(cost - paymentsAll.Sum(x => x.SumAndVat.SumWithVat)) < 0.0001);


        }

        [TestMethod]
        public void SalesUnitWrapperReactionOnActualPayment()
        {
        }

        [TestMethod]
        public void SalesUnitCostAndMarginalIncomeTest()
        {
            _salesUnitWrapper.MarginalIncome.Date = DateTime.Today;

            double cost = _salesUnitWrapper.Cost.SumWithVat;
            double md = _salesUnitWrapper.MarginalIncome.SumAndVat.SumWithVat;

            Assert.IsTrue(Math.Abs(_salesUnitWrapper.MarginalIncomeInPercent - md / cost * 100) < 0.0001);
        }
    }
}
