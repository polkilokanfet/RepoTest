using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Tests.Factory;
using HVTApp.Model.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class SalesUnitTests
    {
        private SalesUnitWrapper _salesUnit;
        private TestWrappersFactory _factory;

        [TestInitialize]
        public void InitialMethod()
        {
            Product product = new Product { ProductItem = new ProductItem() };
            product.ProductItem.Prices.Add(new CostOnDate { Date = DateTime.Today, Cost = new Cost { Sum = 50 } });
            var salesUnit = new SalesUnit
            {
                ProductionUnit = new ProductionUnit {Product = product},
                OfferUnit = new OfferUnit(),
                Cost = 100,
                ShipmentUnit = new ShipmentUnit { ExpectedDeliveryPeriod = 5 },
                Specification = new Specification { Date = DateTime.Today, Vat = 0.2}
            };
            salesUnit.ProductionUnit.SalesUnit = salesUnit;
            salesUnit.ShipmentUnit.SalesUnit = salesUnit;

            salesUnit.PaymentsConditions.Add(new PaymentCondition { Part = 0.30, DaysToPoint = -2, PaymentConditionPoint = PaymentConditionPoint.ProductionStart });
            salesUnit.PaymentsConditions.Add(new PaymentCondition { Part = 0.10, DaysToPoint = 10, PaymentConditionPoint = PaymentConditionPoint.ProductionStart });
            salesUnit.PaymentsConditions.Add(new PaymentCondition { Part = 0.20, DaysToPoint = 20, PaymentConditionPoint = PaymentConditionPoint.ProductionEnd });
            salesUnit.PaymentsConditions.Add(new PaymentCondition { Part = 0.15, DaysToPoint = -3, PaymentConditionPoint = PaymentConditionPoint.Shipment });
            salesUnit.PaymentsConditions.Add(new PaymentCondition { Part = 0.25, DaysToPoint = 25, PaymentConditionPoint = PaymentConditionPoint.Delivery });


            _factory = new TestWrappersFactory();
            _salesUnit = _factory.GetWrapper<SalesUnitWrapper>(salesUnit);
        }

        [TestMethod]
        public void ReloadPaymentsPlannedFullTest()
        {
            _salesUnit.ReloadPaymentsPlannedFull();
            Assert.AreEqual(_salesUnit.Cost, _salesUnit.PaymentsPlanned.Sum(x => x.Sum));
            foreach (var condition in _salesUnit.PaymentsConditions)
            {
                Assert.IsTrue(_salesUnit.PaymentsPlanned.Single(x => Math.Abs(x.Sum - condition.Part * _salesUnit.Cost) < 0.0001) != null);
            }
        }

        [TestMethod]
        public void SalesUnitWrapperReactionOnActualPayment()
        {
            _salesUnit.ReloadPaymentsPlannedFull();
            double paymentSum = _salesUnit.Cost/3;
            _salesUnit.PaymentsActual.Add(_factory.GetWrapper<PaymentActualWrapper>(new PaymentActual {Sum = paymentSum}));
            Assert.IsTrue(Math.Abs(_salesUnit.Cost - paymentSum - _salesUnit.PaymentsPlanned.Sum(x => x.Sum)) < 0.0001);

            //_salesUnit.PaymentsActual.Clear();
            //Assert.IsTrue(Math.Abs(_salesUnit.Cost - _salesUnit.PaymentsPlanned.Sum(x => x.Sum)) < 0.0001);
        }

        [TestMethod]
        public void SalesUnitCostAndMarginalIncomeTest()
        {
            _salesUnit.MarginalIncomeDate = DateTime.Today;

            double cost = _salesUnit.Cost;
            double md = _salesUnit.MarginalIncome;

            Assert.IsTrue(Math.Abs(_salesUnit.MarginalIncomeInPercent - md / cost * 100) < 0.0001);
        }
    }
}
