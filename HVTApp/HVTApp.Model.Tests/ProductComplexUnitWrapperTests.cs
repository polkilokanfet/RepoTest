﻿using System;
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
            ShipmentUnit shipmentUnit = new ShipmentUnit {ExpectedDeliveryPeriod = 5};

            Project project = new Project {EstimatedDate = DateTime.Today.AddDays(120)};

            var productComplexUnit = new ProductComplexUnit
            {
                Product = product,
                Cost = new Cost { Sum = 100 },
                ShipmentUnit = shipmentUnit,
                Project = project
            };
            productComplexUnit.PaymentsConditions.Add(new PaymentCondition { Part = 0.30, DaysToPoint = -2, PaymentConditionPoint = PaymentConditionPoint.ProductionStart });
            productComplexUnit.PaymentsConditions.Add(new PaymentCondition { Part = 0.10, DaysToPoint = 20, PaymentConditionPoint = PaymentConditionPoint.ProductionStart });
            productComplexUnit.PaymentsConditions.Add(new PaymentCondition { Part = 0.20, DaysToPoint = 20, PaymentConditionPoint = PaymentConditionPoint.ProductionEnd });
            productComplexUnit.PaymentsConditions.Add(new PaymentCondition { Part = 0.15, DaysToPoint = -2, PaymentConditionPoint = PaymentConditionPoint.Shipment });
            productComplexUnit.PaymentsConditions.Add(new PaymentCondition { Part = 0.25, DaysToPoint = 25, PaymentConditionPoint = PaymentConditionPoint.Delivery });

            shipmentUnit.ProductComplexUnit = productComplexUnit;

            _productComplex = WrappersFactory.GetWrapper<ProductComplexUnitWrapper>(productComplexUnit);
        }

        [TestMethod]
        public void ReGeneratePlanPayments()
        {
            //_productComplex.Payments.Clear();
            //_productComplex.ReGeneratePlanPaymentsHard();
            //Assert.AreEqual(_productComplex.Sum.Sum, _productComplex.Payments.Sum(x => x.Sum.Sum));
            //foreach (var condition in _productComplex.PaymentsConditions)
            //{
            //    Assert.IsTrue(_productComplex.Payments.Any(x => Math.Abs(x.Sum.Sum - condition.Part * _productComplex.Sum.Sum) < 0.0001));
            //}
        }

        [TestMethod]
        public void SalesUnitWrapperReloadPlannedPayments()
        {
            //_productComplex.ReloadPaymentsPlannedFull();

            ////проверка соответствия плановых платежей и платежей по условиям контракта

            //var cost = _productComplex.Sum.Sum;

            //Assert.AreEqual(_productComplex.PaymentsPlanned.Count, _productComplex.PaymentsConditions.Count); //количество плановых и фактических платежей совпадает
            //Assert.IsTrue(Math.Abs(cost - _productComplex.PaymentsPlanned.Sum(x => x.Sum.Sum)) < 0.0001);
            //Assert.IsTrue(Math.Abs(cost - _productComplex.PaymentsAll.Sum(x => x.Sum.Sum)) < 0.0001);

            //var firstPaymentSum = cost/3;
            //var firstPayment = new PaymentActual { Sum = new Sum { Sum = firstPaymentSum }, Date = DateTime.Today.AddDays(-20) };
            //_productComplex.PaymentsActual.Add(WrappersFactory.GetWrapper<PaymentActualWrapper> (firstPayment));
            //Assert.IsTrue(Math.Abs(cost - _productComplex.PaymentsAll.Sum(x => x.Sum.Sum)) < 0.0001);

            //var secondPayment = new PaymentActual { Sum = new Sum { Sum = cost - firstPaymentSum }, Date = DateTime.Today };
            //_productComplex.PaymentsActual.Add(WrappersFactory.GetWrapper<PaymentActualWrapper> (secondPayment));
            //Assert.IsFalse(_productComplex.PaymentsPlanned.Any());

            //_productComplex.PaymentsActual.Remove(_productComplex.PaymentsActual.First());
            //Assert.IsTrue(Math.Abs(cost - _productComplex.PaymentsAll.Sum(x => x.Sum.Sum)) < 0.0001);
            //Assert.IsTrue(Math.Abs(firstPaymentSum - _productComplex.SumNotPaid.Sum) < 0.0001);
            //Assert.IsTrue(Math.Abs(firstPaymentSum - _productComplex.PaymentsPlanned.Sum(x => x.Sum.Sum)) < 0.0001);

            //firstPayment.Sum.Sum = firstPaymentSum / 2;
            //_productComplex.PaymentsActual.Add(WrappersFactory.GetWrapper<PaymentActualWrapper> (firstPayment));
            //_productComplex.PaymentsActual.Remove(_productComplex.PaymentsActual.First());
            //Assert.IsTrue(Math.Abs(_productComplex.SumNotPaid.Sum - _productComplex.PaymentsPlanned.Sum(x => x.Sum.Sum)) < 0.0001);
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