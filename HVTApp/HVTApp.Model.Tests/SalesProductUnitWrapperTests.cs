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
            var unit = new SalesProductUnit { Cost = new SumAndVat { Sum = 100, Vat = 0 } };
            unit.PaymentsConditions.Add(new PaymentCondition { PartInPercent = 50, DaysToPoint = 2, PaymentConditionPoint = PaymentConditionPoint.ProductionStart });
            unit.PaymentsConditions.Add(new PaymentCondition { PartInPercent = 30, DaysToPoint = 2, PaymentConditionPoint = PaymentConditionPoint.ProductionEnd });
            unit.PaymentsConditions.Add(new PaymentCondition { PartInPercent = 20, DaysToPoint = 2, PaymentConditionPoint = PaymentConditionPoint.Delivery });
            _salesProductUnitWrapper = SalesProductUnitWrapper.GetWrapper(unit);
        }

        [TestMethod]
        public void SalesProductUnitWrapperReloadPlannedPayments()
        {
            _salesProductUnitWrapper.ReloadPaymentsPlanned();
            Assert.AreEqual(_salesProductUnitWrapper.PaymentsPlanned.Count, _salesProductUnitWrapper.PaymentsConditions.Count);
            Assert.AreEqual(_salesProductUnitWrapper.Cost.SumWithVat, _salesProductUnitWrapper.PaymentsPlanned.Sum(x => x.SumAndVat.SumWithVat));
        }

        [TestMethod]
        public void SalesProductUnitWrapperReactionOnActualPayment()
        {
        }


    }
}
