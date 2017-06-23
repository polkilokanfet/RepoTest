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
        private ProductComplexUnitWrapper _productComplexUnitWrapper;

        [TestInitialize]
        public void InitialMethod()
        {
            Product product = new Product {ProductItem = new ProductItem()};
            product.ProductItem.Prices.Add(new SumOnDate {Date = DateTime.Today, Sum = 50 });
            ProductShipmentUnit productShipmentUnit = new ProductShipmentUnit {ExpectedDeliveryPeriod = 5};

            Project project = new Project {EstimatedDate = DateTime.Today.AddDays(120)};

            var productComplexUnit = new ProductComplexUnit
            {
                Product = product,
                Cost = new SumAndVat { Sum = 100, Vat = 10 },
                ProductShipmentUnit = productShipmentUnit,
                Project = project
            };
            productComplexUnit.PaymentsConditions.Add(new PaymentCondition { PartInPercent = 30, DaysToPoint = -2, PaymentConditionPoint = PaymentConditionPoint.ProductionStart });
            productComplexUnit.PaymentsConditions.Add(new PaymentCondition { PartInPercent = 10, DaysToPoint = 20, PaymentConditionPoint = PaymentConditionPoint.ProductionStart });
            productComplexUnit.PaymentsConditions.Add(new PaymentCondition { PartInPercent = 20, DaysToPoint = 20, PaymentConditionPoint = PaymentConditionPoint.ProductionEnd });
            productComplexUnit.PaymentsConditions.Add(new PaymentCondition { PartInPercent = 15, DaysToPoint = -2, PaymentConditionPoint = PaymentConditionPoint.Shipment });
            productComplexUnit.PaymentsConditions.Add(new PaymentCondition { PartInPercent = 25, DaysToPoint = 25, PaymentConditionPoint = PaymentConditionPoint.Delivery });

            productShipmentUnit.ProductComplexUnit = productComplexUnit;

            _productComplexUnitWrapper = WrappersFactory.GetWrapper<ProductComplexUnitWrapper>(productComplexUnit);
        }

        [TestMethod]
        public void SalesUnitWrapperReloadPlannedPayments()
        {
            _productComplexUnitWrapper.ReloadPaymentsPlannedFull();

            //проверка соответствия плановых платежей и платежей по условиям контракта

            var cost = _productComplexUnitWrapper.Cost.Sum;

            Assert.AreEqual(_productComplexUnitWrapper.PaymentsPlanned.Count, _productComplexUnitWrapper.PaymentsConditions.Count); //количество плановых и фактических платежей совпадает
            Assert.IsTrue(Math.Abs(cost - _productComplexUnitWrapper.PaymentsPlanned.Sum(x => x.SumAndVat.Sum)) < 0.0001);
            Assert.IsTrue(Math.Abs(cost - _productComplexUnitWrapper.PaymentsAll.Sum(x => x.SumAndVat.Sum)) < 0.0001);

            var firstPaymentSum = cost/3;
            var firstPayment = new PaymentActual { SumAndVat = new SumAndVat { Sum = firstPaymentSum }, Date = DateTime.Today.AddDays(-20) };
            _productComplexUnitWrapper.PaymentsActual.Add(WrappersFactory.GetWrapper<PaymentActualWrapper> (firstPayment));
            Assert.IsTrue(Math.Abs(cost - _productComplexUnitWrapper.PaymentsAll.Sum(x => x.SumAndVat.Sum)) < 0.0001);

            var secondPayment = new PaymentActual { SumAndVat = new SumAndVat { Sum = cost - firstPaymentSum }, Date = DateTime.Today };
            _productComplexUnitWrapper.PaymentsActual.Add(WrappersFactory.GetWrapper<PaymentActualWrapper> (secondPayment));
            Assert.IsFalse(_productComplexUnitWrapper.PaymentsPlanned.Any());

            _productComplexUnitWrapper.PaymentsActual.Remove(_productComplexUnitWrapper.PaymentsActual.First());
            Assert.IsTrue(Math.Abs(cost - _productComplexUnitWrapper.PaymentsAll.Sum(x => x.SumAndVat.Sum)) < 0.0001);
            Assert.IsTrue(Math.Abs(firstPaymentSum - _productComplexUnitWrapper.SumRest.Sum) < 0.0001);
            Assert.IsTrue(Math.Abs(firstPaymentSum - _productComplexUnitWrapper.PaymentsPlanned.Sum(x => x.SumAndVat.Sum)) < 0.0001);

            firstPayment.SumAndVat.Sum = firstPaymentSum / 2;
            _productComplexUnitWrapper.PaymentsActual.Add(WrappersFactory.GetWrapper<PaymentActualWrapper> (firstPayment));
            _productComplexUnitWrapper.PaymentsActual.Remove(_productComplexUnitWrapper.PaymentsActual.First());
            Assert.IsTrue(Math.Abs(_productComplexUnitWrapper.SumRest.Sum - _productComplexUnitWrapper.PaymentsPlanned.Sum(x => x.SumAndVat.Sum)) < 0.0001);
        }

        [TestMethod]
        public void SalesUnitWrapperReactionOnActualPayment()
        {
        }

        [TestMethod]
        public void SalesUnitCostAndMarginalIncomeTest()
        {
            _productComplexUnitWrapper.MarginalIncomeDate = DateTime.Today;

            double cost = _productComplexUnitWrapper.Cost.Sum;
            double md = _productComplexUnitWrapper.MarginalIncomeSingle;

            Assert.IsTrue(Math.Abs(_productComplexUnitWrapper.MarginalIncomeInPercentSingle - md / cost * 100) < 0.0001);
        }
    }
}
