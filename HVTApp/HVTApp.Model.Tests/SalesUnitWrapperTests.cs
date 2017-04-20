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
            product.Prices.Add(new SumOnDate {Date = DateTime.Today, Sum = 50 });
            ProductionUnit productionUnit = new ProductionUnit {Product = product};
            ShipmentUnit shipmentUnit = new ShipmentUnit {ExpectedDeliveryPeriod = 5};

            Project project = new Project {EstimatedDate = DateTime.Today.AddDays(120)};

            var unit = new SalesUnit
            {
                CostSingle = new SumAndVat { Sum = 100, Vat = 10 },
                ProductionUnit = productionUnit,
                ShipmentUnit = shipmentUnit,
                Project = project
            };
            unit.PaymentsConditions.Add(new PaymentCondition { PartInPercent = 30, DaysToPoint = -2, PaymentConditionPoint = PaymentConditionPoint.ProductionStart });
            unit.PaymentsConditions.Add(new PaymentCondition { PartInPercent = 10, DaysToPoint = 20, PaymentConditionPoint = PaymentConditionPoint.ProductionStart });
            unit.PaymentsConditions.Add(new PaymentCondition { PartInPercent = 20, DaysToPoint = 20, PaymentConditionPoint = PaymentConditionPoint.ProductionEnd });
            unit.PaymentsConditions.Add(new PaymentCondition { PartInPercent = 15, DaysToPoint = -2, PaymentConditionPoint = PaymentConditionPoint.Shipment });
            unit.PaymentsConditions.Add(new PaymentCondition { PartInPercent = 25, DaysToPoint = 25, PaymentConditionPoint = PaymentConditionPoint.Delivery });

            productionUnit.SalesUnit = unit;
            shipmentUnit.SalesUnit = unit;

            _salesUnitWrapper = SalesUnitWrapper.GetWrapper(unit);
        }

        [TestMethod]
        public void SalesUnitWrapperReloadPlannedPayments()
        {
            _salesUnitWrapper.ReloadPaymentsPlannedFull();

            //проверка соответствия плановых платежей и платежей по условиям контракта

            var cost = _salesUnitWrapper.CostSingle.Sum;

            Assert.AreEqual(_salesUnitWrapper.PaymentsPlanned.Count, _salesUnitWrapper.PaymentsConditions.Count); //количество плановых и фактических платежей совпадает
            Assert.IsTrue(Math.Abs(cost - _salesUnitWrapper.PaymentsPlanned.Sum(x => x.SumAndVat.Sum)) < 0.0001);
            Assert.IsTrue(Math.Abs(cost - _salesUnitWrapper.PaymentsAll.Sum(x => x.SumAndVat.Sum)) < 0.0001);

            var firstPaymentSum = cost/3;
            var firstPayment = new PaymentActual { SumAndVat = new SumAndVat { Sum = firstPaymentSum }, Date = DateTime.Today.AddDays(-20) };
            _salesUnitWrapper.PaymentsActual.Add(PaymentActualWrapper.GetWrapper(firstPayment));
            Assert.IsTrue(Math.Abs(cost - _salesUnitWrapper.PaymentsAll.Sum(x => x.SumAndVat.Sum)) < 0.0001);

            var secondPayment = new PaymentActual { SumAndVat = new SumAndVat { Sum = cost - firstPaymentSum }, Date = DateTime.Today };
            _salesUnitWrapper.PaymentsActual.Add(PaymentActualWrapper.GetWrapper(secondPayment));
            Assert.IsFalse(_salesUnitWrapper.PaymentsPlanned.Any());

            _salesUnitWrapper.PaymentsActual.Remove(_salesUnitWrapper.PaymentsActual.First());
            Assert.IsTrue(Math.Abs(cost - _salesUnitWrapper.PaymentsAll.Sum(x => x.SumAndVat.Sum)) < 0.0001);
            Assert.IsTrue(Math.Abs(firstPaymentSum - _salesUnitWrapper.SumRest.Sum) < 0.0001);
            Assert.IsTrue(Math.Abs(firstPaymentSum - _salesUnitWrapper.PaymentsPlanned.Sum(x => x.SumAndVat.Sum)) < 0.0001);

            firstPayment.SumAndVat.Sum = firstPaymentSum / 2;
            _salesUnitWrapper.PaymentsActual.Add(PaymentActualWrapper.GetWrapper(firstPayment));
            _salesUnitWrapper.PaymentsActual.Remove(_salesUnitWrapper.PaymentsActual.First());
            Assert.IsTrue(Math.Abs(_salesUnitWrapper.SumRest.Sum - _salesUnitWrapper.PaymentsPlanned.Sum(x => x.SumAndVat.Sum)) < 0.0001);
        }

        [TestMethod]
        public void SalesUnitWrapperReactionOnActualPayment()
        {
        }

        [TestMethod]
        public void SalesUnitCostAndMarginalIncomeTest()
        {
            _salesUnitWrapper.MarginalIncomeDate = DateTime.Today;

            double cost = _salesUnitWrapper.CostSingle.Sum;
            double md = _salesUnitWrapper.MarginalIncomeSingle;

            Assert.IsTrue(Math.Abs(_salesUnitWrapper.MarginalIncomeInPercentSingle - md / cost * 100) < 0.0001);
        }
    }
}
