using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class ProductComplexUnitWrapperTests
    {
        private ProductComplexUnitWrapper _productComplex;

        [TestInitialize]
        public void InitialMethod()
        {
            Product product = new Product {ProductItem = new ProductItem()};
            product.ProductItem.Prices.Add(new CostOnDate {Date = DateTime.Today, Cost = new Cost {Sum = 50}  });
            ProductShipmentUnit productShipmentUnit = new ProductShipmentUnit {ExpectedDeliveryPeriod = 5};

            Project project = new Project {EstimatedDate = DateTime.Today.AddDays(120)};

            var productComplexUnit = new ProductComplexUnit
            {
                Product = product,
                Cost = new Cost { Sum = 100 },
                ProductShipmentUnit = productShipmentUnit,
                Project = project
            };
            productComplexUnit.PaymentsConditions.Add(new PaymentCondition { Part = 0.30, DaysToPoint = -2, PaymentConditionPoint = PaymentConditionPoint.ProductionStart });
            productComplexUnit.PaymentsConditions.Add(new PaymentCondition { Part = 0.10, DaysToPoint = 20, PaymentConditionPoint = PaymentConditionPoint.ProductionStart });
            productComplexUnit.PaymentsConditions.Add(new PaymentCondition { Part = 0.20, DaysToPoint = 20, PaymentConditionPoint = PaymentConditionPoint.ProductionEnd });
            productComplexUnit.PaymentsConditions.Add(new PaymentCondition { Part = 0.15, DaysToPoint = -2, PaymentConditionPoint = PaymentConditionPoint.Shipment });
            productComplexUnit.PaymentsConditions.Add(new PaymentCondition { Part = 0.25, DaysToPoint = 25, PaymentConditionPoint = PaymentConditionPoint.Delivery });

            productShipmentUnit.ProductComplexUnit = productComplexUnit;

            _productComplex = WrappersFactory.GetWrapper<ProductComplexUnitWrapper>(productComplexUnit);
        }

        [TestMethod]
        public void ReGeneratePlanPayments()
        {
            _productComplex.Payments.Clear();
            _productComplex.ReGeneratePlanPayments();
            Assert.AreEqual(_productComplex.Cost.Sum, _productComplex.Payments.Sum(x => x.Cost.Sum));
            foreach (var condition in _productComplex.PaymentsConditions)
            {
                Assert.IsTrue(_productComplex.Payments.Any(x => Math.Abs(x.Cost.Sum - condition.Part * _productComplex.Cost.Sum) < 0.0001));
            }
        }

        [TestMethod]
        public void SalesUnitWrapperReloadPlannedPayments()
        {
            //_productComplex.ReloadPaymentsPlannedFull();

            ////проверка соответствия плановых платежей и платежей по условиям контракта

            //var cost = _productComplex.Cost.Cost;

            //Assert.AreEqual(_productComplex.PaymentsPlanned.Count, _productComplex.PaymentsConditions.Count); //количество плановых и фактических платежей совпадает
            //Assert.IsTrue(Math.Abs(cost - _productComplex.PaymentsPlanned.Cost(x => x.Cost.Cost)) < 0.0001);
            //Assert.IsTrue(Math.Abs(cost - _productComplex.PaymentsAll.Cost(x => x.Cost.Cost)) < 0.0001);

            //var firstPaymentSum = cost/3;
            //var firstPayment = new Payment { Cost = new Cost { Cost = firstPaymentSum }, Date = DateTime.Today.AddDays(-20) };
            //_productComplex.PaymentsActual.Add(WrappersFactory.GetWrapper<PaymentActualWrapper> (firstPayment));
            //Assert.IsTrue(Math.Abs(cost - _productComplex.PaymentsAll.Cost(x => x.Cost.Cost)) < 0.0001);

            //var secondPayment = new Payment { Cost = new Cost { Cost = cost - firstPaymentSum }, Date = DateTime.Today };
            //_productComplex.PaymentsActual.Add(WrappersFactory.GetWrapper<PaymentActualWrapper> (secondPayment));
            //Assert.IsFalse(_productComplex.PaymentsPlanned.Any());

            //_productComplex.PaymentsActual.Remove(_productComplex.PaymentsActual.First());
            //Assert.IsTrue(Math.Abs(cost - _productComplex.PaymentsAll.Cost(x => x.Cost.Cost)) < 0.0001);
            //Assert.IsTrue(Math.Abs(firstPaymentSum - _productComplex.SumDontPaid.Cost) < 0.0001);
            //Assert.IsTrue(Math.Abs(firstPaymentSum - _productComplex.PaymentsPlanned.Cost(x => x.Cost.Cost)) < 0.0001);

            //firstPayment.Cost.Cost = firstPaymentSum / 2;
            //_productComplex.PaymentsActual.Add(WrappersFactory.GetWrapper<PaymentActualWrapper> (firstPayment));
            //_productComplex.PaymentsActual.Remove(_productComplex.PaymentsActual.First());
            //Assert.IsTrue(Math.Abs(_productComplex.SumDontPaid.Cost - _productComplex.PaymentsPlanned.Cost(x => x.Cost.Cost)) < 0.0001);
        }

        [TestMethod]
        public void SalesUnitWrapperReactionOnActualPayment()
        {
        }

        [TestMethod]
        public void SalesUnitCostAndMarginalIncomeTest()
        {
            _productComplex.MarginalIncomeDate = DateTime.Today;

            double cost = _productComplex.Cost.Sum;
            double md = _productComplex.MarginalIncomeSingle;

            Assert.IsTrue(Math.Abs(_productComplex.MarginalIncomeInPercentSingle - md / cost * 100) < 0.0001);
        }
    }
}
