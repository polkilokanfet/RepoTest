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
        private SalesUnitWrapper _salesUnit;

        [TestInitialize]
        public void InitialMethod()
        {
            //Product product = new Product {ProductItem = new ProductItem()};
            //product.ProductItem.Prices.Add(new CostOnDate {Date = DateTime.Today, Cost = new Cost {Sum = 50}  });
            //ShipmentUnit shipmentUnit = new ShipmentUnit {ExpectedDeliveryPeriod = 5};

            //Project project = new Project();

            //var salesUnit = new SalesUnit
            //{
            //    Product = product,
            //    Cost = 100,
            //    ShipmentUnit = shipmentUnit,
            //};
            //salesUnit.PaymentsConditions.Add(new PaymentCondition { Part = 0.30, DaysToPoint = -2, PaymentConditionPoint = PaymentConditionPoint.ProductionStart });
            //salesUnit.PaymentsConditions.Add(new PaymentCondition { Part = 0.10, DaysToPoint = 20, PaymentConditionPoint = PaymentConditionPoint.ProductionStart });
            //salesUnit.PaymentsConditions.Add(new PaymentCondition { Part = 0.20, DaysToPoint = 20, PaymentConditionPoint = PaymentConditionPoint.ProductionEnd });
            //salesUnit.PaymentsConditions.Add(new PaymentCondition { Part = 0.15, DaysToPoint = -2, PaymentConditionPoint = PaymentConditionPoint.Shipment });
            //salesUnit.PaymentsConditions.Add(new PaymentCondition { Part = 0.25, DaysToPoint = 25, PaymentConditionPoint = PaymentConditionPoint.Delivery });

            //shipmentUnit.ProductComplexUnit = salesUnit;

            //_salesUnit = TestWrappersFactory.GetWrapper<ProductComplexUnitWrapper>(salesUnit);
        }

        [TestMethod]
        public void ReGeneratePlanPayments()
        {
            //_salesUnit.Payments.Clear();
            //_salesUnit.ReGeneratePlanPaymentsHard();
            //Assert.AreEqual(_salesUnit.Sum.Sum, _salesUnit.Payments.Sum(x => x.Sum.Sum));
            //foreach (var condition in _salesUnit.PaymentsConditions)
            //{
            //    Assert.IsTrue(_salesUnit.Payments.Any(x => Math.Abs(x.Sum.Sum - condition.Part * _salesUnit.Sum.Sum) < 0.0001));
            //}
        }

        [TestMethod]
        public void SalesUnitWrapperReloadPlannedPayments()
        {
            //_salesUnit.ReloadPaymentsPlannedFull();

            ////проверка соответствия плановых платежей и платежей по условиям контракта

            //var cost = _salesUnit.Sum.Sum;

            //Assert.AreEqual(_salesUnit.PaymentsPlanned.Count, _salesUnit.PaymentsConditions.Count); //количество плановых и фактических платежей совпадает
            //Assert.IsTrue(Math.Abs(cost - _salesUnit.PaymentsPlanned.Sum(x => x.Sum.Sum)) < 0.0001);
            //Assert.IsTrue(Math.Abs(cost - _salesUnit.PaymentsAll.Sum(x => x.Sum.Sum)) < 0.0001);

            //var firstPaymentSum = cost/3;
            //var firstPayment = new PaymentActual { Sum = new Sum { Sum = firstPaymentSum }, Date = DateTime.Today.AddDays(-20) };
            //_salesUnit.PaymentsActual.Add(TestWrappersFactory.GetWrapper<PaymentActualWrapper> (firstPayment));
            //Assert.IsTrue(Math.Abs(cost - _salesUnit.PaymentsAll.Sum(x => x.Sum.Sum)) < 0.0001);

            //var secondPayment = new PaymentActual { Sum = new Sum { Sum = cost - firstPaymentSum }, Date = DateTime.Today };
            //_salesUnit.PaymentsActual.Add(TestWrappersFactory.GetWrapper<PaymentActualWrapper> (secondPayment));
            //Assert.IsFalse(_salesUnit.PaymentsPlanned.Any());

            //_salesUnit.PaymentsActual.Remove(_salesUnit.PaymentsActual.First());
            //Assert.IsTrue(Math.Abs(cost - _salesUnit.PaymentsAll.Sum(x => x.Sum.Sum)) < 0.0001);
            //Assert.IsTrue(Math.Abs(firstPaymentSum - _salesUnit.SumNotPaid.Sum) < 0.0001);
            //Assert.IsTrue(Math.Abs(firstPaymentSum - _salesUnit.PaymentsPlanned.Sum(x => x.Sum.Sum)) < 0.0001);

            //firstPayment.Sum.Sum = firstPaymentSum / 2;
            //_salesUnit.PaymentsActual.Add(TestWrappersFactory.GetWrapper<PaymentActualWrapper> (firstPayment));
            //_salesUnit.PaymentsActual.Remove(_salesUnit.PaymentsActual.First());
            //Assert.IsTrue(Math.Abs(_salesUnit.SumNotPaid.Sum - _salesUnit.PaymentsPlanned.Sum(x => x.Sum.Sum)) < 0.0001);
        }

        [TestMethod]
        public void SalesUnitWrapperReactionOnActualPayment()
        {
        }

        [TestMethod]
        public void SalesUnitCostAndMarginalIncomeTest()
        {
            //_salesUnit.MarginalIncomeDate = DateTime.Today;

            //double cost = _salesUnit.Cost.Sum;
            //double md = _salesUnit.MarginalIncomeSingle;

            //Assert.IsTrue(Math.Abs(_salesUnit.MarginalIncomeInPercentSingle - md / cost * 100) < 0.0001);
        }
    }
}
