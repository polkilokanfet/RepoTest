using System;
using System.Linq;
using HVTApp.Model.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class SalesProductUnitWrapperTests
    {
        private SalesProductUnitWrapper _salesProductUnitWrapper;
        [TestInitialize]
        public void InitialMethod()
        {
            var unit = new SalesProductUnit { Cost = new SumAndVat { Sum = 100, Vat = 10 } };
            unit.PaymentsConditions.Add(new PaymentCondition { PartInPercent = 50, DaysToPoint = 2, PaymentConditionPoint = PaymentConditionPoint.ProductionStart });
            unit.PaymentsConditions.Add(new PaymentCondition { PartInPercent = 30, DaysToPoint = 2, PaymentConditionPoint = PaymentConditionPoint.ProductionEnd });
            unit.PaymentsConditions.Add(new PaymentCondition { PartInPercent = 20, DaysToPoint = 2, PaymentConditionPoint = PaymentConditionPoint.Delivery });
            _salesProductUnitWrapper = SalesProductUnitWrapper.GetWrapper(unit);
        }

        [TestMethod]
        public void SalesProductUnitWrapperReloadPlannedPayments()
        {
            _salesProductUnitWrapper.ReloadPaymentsPlanned();

            var cost = _salesProductUnitWrapper.Cost.SumWithVat;
            var paymentsConditions = _salesProductUnitWrapper.PaymentsConditions;
            var paymentsPlanned = _salesProductUnitWrapper.PaymentsPlanned;
            var paymentsActual = _salesProductUnitWrapper.PaymentsActual;
            var paymentsAll = paymentsActual.Union(paymentsPlanned);

            Assert.AreEqual(paymentsPlanned.Count, paymentsConditions.Count);
            Assert.IsTrue(Math.Abs(cost - paymentsPlanned.Sum(x => x.SumAndVat.SumWithVat)) < 0.0001);
            Assert.IsTrue(Math.Abs(cost - paymentsAll.Sum(x => x.SumAndVat.SumWithVat)) < 0.0001);

            var payment = new Payment {SumAndVat = new SumAndVat() };
            payment.SumAndVat.Sum = _salesProductUnitWrapper.Cost.Sum / 2;
            _salesProductUnitWrapper.PaymentsActual.Add(PaymentWrapper.GetWrapper(payment));
            Assert.IsTrue(Math.Abs(cost - paymentsAll.Sum(x => x.SumAndVat.SumWithVat)) < 0.0001);


        }

        [TestMethod]
        public void SalesProductUnitWrapperReactionOnActualPayment()
        {
        }


    }
}
