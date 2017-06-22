using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
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
            Product product = new Product {ProductItem = new ProductItem()};
            product.ProductItem.Prices.Add(new SumOnDate {Date = DateTime.Today, Sum = 50 });
            ProductSalesUnit productSalesUnit = new ProductSalesUnit { Cost = new SumAndVat { Sum = 100, Vat = 10 }};
            ProductProductionUnit productProductionUnit = new ProductProductionUnit {Product = product};
            ProductShipmentUnit productShipmentUnit = new ProductShipmentUnit {ExpectedDeliveryPeriod = 5};

            Project project = new Project {EstimatedDate = DateTime.Today.AddDays(120)};

            var unit = new ProductComplexUnit
            {
                ProductSalesUnit = productSalesUnit,
                ProductProductionUnit = productProductionUnit,
                ProductShipmentUnit = productShipmentUnit,
                Project = project
            };
            productSalesUnit.PaymentsConditions.Add(new PaymentCondition { PartInPercent = 30, DaysToPoint = -2, PaymentConditionPoint = PaymentConditionPoint.ProductionStart });
            productSalesUnit.PaymentsConditions.Add(new PaymentCondition { PartInPercent = 10, DaysToPoint = 20, PaymentConditionPoint = PaymentConditionPoint.ProductionStart });
            productSalesUnit.PaymentsConditions.Add(new PaymentCondition { PartInPercent = 20, DaysToPoint = 20, PaymentConditionPoint = PaymentConditionPoint.ProductionEnd });
            productSalesUnit.PaymentsConditions.Add(new PaymentCondition { PartInPercent = 15, DaysToPoint = -2, PaymentConditionPoint = PaymentConditionPoint.Shipment });
            productSalesUnit.PaymentsConditions.Add(new PaymentCondition { PartInPercent = 25, DaysToPoint = 25, PaymentConditionPoint = PaymentConditionPoint.Delivery });

            productSalesUnit.ProductComplexUnit = unit;
            productProductionUnit.ProductComplexUnit = unit;
            productShipmentUnit.ProductComplexUnit = unit;

            _salesUnitWrapper = WrappersFactory.GetWrapper <ProductSalesUnit, SalesUnitWrapper> (unit.ProductSalesUnit);
        }

        [TestMethod]
        public void SalesUnitWrapperReloadPlannedPayments()
        {
            _salesUnitWrapper.ReloadPaymentsPlannedFull();

            //проверка соответствия плановых платежей и платежей по условиям контракта

            var cost = _salesUnitWrapper.Cost.Sum;

            Assert.AreEqual(_salesUnitWrapper.PaymentsPlanned.Count, _salesUnitWrapper.PaymentsConditions.Count); //количество плановых и фактических платежей совпадает
            Assert.IsTrue(Math.Abs(cost - _salesUnitWrapper.PaymentsPlanned.Sum(x => x.SumAndVat.Sum)) < 0.0001);
            Assert.IsTrue(Math.Abs(cost - _salesUnitWrapper.PaymentsAll.Sum(x => x.SumAndVat.Sum)) < 0.0001);

            var firstPaymentSum = cost/3;
            var firstPayment = new PaymentActual { SumAndVat = new SumAndVat { Sum = firstPaymentSum }, Date = DateTime.Today.AddDays(-20) };
            _salesUnitWrapper.PaymentsActual.Add(WrappersFactory.GetWrapper <PaymentActual, PaymentActualWrapper> (firstPayment));
            Assert.IsTrue(Math.Abs(cost - _salesUnitWrapper.PaymentsAll.Sum(x => x.SumAndVat.Sum)) < 0.0001);

            var secondPayment = new PaymentActual { SumAndVat = new SumAndVat { Sum = cost - firstPaymentSum }, Date = DateTime.Today };
            _salesUnitWrapper.PaymentsActual.Add(WrappersFactory.GetWrapper <PaymentActual, PaymentActualWrapper> (secondPayment));
            Assert.IsFalse(_salesUnitWrapper.PaymentsPlanned.Any());

            _salesUnitWrapper.PaymentsActual.Remove(_salesUnitWrapper.PaymentsActual.First());
            Assert.IsTrue(Math.Abs(cost - _salesUnitWrapper.PaymentsAll.Sum(x => x.SumAndVat.Sum)) < 0.0001);
            Assert.IsTrue(Math.Abs(firstPaymentSum - _salesUnitWrapper.SumRest.Sum) < 0.0001);
            Assert.IsTrue(Math.Abs(firstPaymentSum - _salesUnitWrapper.PaymentsPlanned.Sum(x => x.SumAndVat.Sum)) < 0.0001);

            firstPayment.SumAndVat.Sum = firstPaymentSum / 2;
            _salesUnitWrapper.PaymentsActual.Add(WrappersFactory.GetWrapper <PaymentActual, PaymentActualWrapper> (firstPayment));
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

            double cost = _salesUnitWrapper.Cost.Sum;
            double md = _salesUnitWrapper.MarginalIncomeSingle;

            Assert.IsTrue(Math.Abs(_salesUnitWrapper.MarginalIncomeInPercentSingle - md / cost * 100) < 0.0001);
        }
    }
}
