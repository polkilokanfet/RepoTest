using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class SalesUnitTests
    {
        private SalesUnitWrapper _salesUnit;

        [TestInitialize]
        public void InitialMethod()
        {
            Product product = new Product { Part = new Part() };
            product.Part.Prices.Add(new CostOnDate { Date = DateTime.Today, Cost = 50 });
            var salesUnit = new SalesUnit
            {
                ProductionUnit = new ProductionUnit {Product = product},
                OfferUnit = new OfferUnit(),
                Cost = 100,
                ShipmentUnit = new ShipmentUnit { ExpectedDeliveryPeriod = 5 },
                Specification = new Specification { Date = DateTime.Today, Vat = 0.2}
            };

            salesUnit.PaymentsConditions.Add(new PaymentCondition { Part = 0.30, DaysToPoint = -2, PaymentConditionPoint = PaymentConditionPoint.ProductionStart });
            salesUnit.PaymentsConditions.Add(new PaymentCondition { Part = 0.10, DaysToPoint = 10, PaymentConditionPoint = PaymentConditionPoint.ProductionStart });
            salesUnit.PaymentsConditions.Add(new PaymentCondition { Part = 0.20, DaysToPoint = 20, PaymentConditionPoint = PaymentConditionPoint.ProductionEnd });
            salesUnit.PaymentsConditions.Add(new PaymentCondition { Part = 0.15, DaysToPoint = -3, PaymentConditionPoint = PaymentConditionPoint.Shipment });
            salesUnit.PaymentsConditions.Add(new PaymentCondition { Part = 0.25, DaysToPoint = 25, PaymentConditionPoint = PaymentConditionPoint.Delivery });


            _salesUnit = new SalesUnitWrapper(salesUnit);
            _salesUnit.ProductionUnit.SalesUnit = _salesUnit;
            _salesUnit.ShipmentUnit.SalesUnit = _salesUnit;
        }

        [TestMethod]
        public void PaymentToStartProductionTest()
        {
            SalesUnit salesUnit = new SalesUnit();
            SalesUnitWrapper suw = new SalesUnitWrapper(salesUnit);

            Assert.AreEqual(suw.SumToStartProduction, 0.0);

            //вносим условие - оплатить до запуска производства
            double part1 = 25;
            PaymentCondition paymentCondition1 = new PaymentCondition
            { Part = part1, DaysToPoint = -7, PaymentConditionPoint = PaymentConditionPoint.ProductionStart };
            suw.PaymentsConditions.Add(new PaymentConditionWrapper(paymentCondition1));
            Assert.AreEqual(suw.SumToStartProduction, part1 * salesUnit.Cost);

            //вносим условие - оплатить после запуска производства
            PaymentCondition paymentCondition2 = new PaymentCondition
            { Part = part1, DaysToPoint = 10, PaymentConditionPoint = PaymentConditionPoint.ProductionStart };
            suw.PaymentsConditions.Add(new PaymentConditionWrapper(paymentCondition2));
            Assert.AreEqual(suw.SumToStartProduction, part1 * salesUnit.Cost);

            //вносим условие - оплатить до окончания производства
            PaymentCondition paymentCondition3 = new PaymentCondition
            { Part = part1, DaysToPoint = -5, PaymentConditionPoint = PaymentConditionPoint.ProductionEnd };
            suw.PaymentsConditions.Add(new PaymentConditionWrapper(paymentCondition3));
            Assert.AreEqual(suw.SumToStartProduction, part1 * salesUnit.Cost);

            //вносим условие - оплатить до запуска производства
            double part2 = 20;
            PaymentCondition paymentCondition4 = new PaymentCondition
            { Part = part2, DaysToPoint = 0, PaymentConditionPoint = PaymentConditionPoint.ProductionStart };
            suw.PaymentsConditions.Add(new PaymentConditionWrapper(paymentCondition4));
            Assert.AreEqual(suw.SumToStartProduction, (part1 + part2) * salesUnit.Cost);
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
        public void SalesUnitWrapperOnActualPaymentsCollectionChanged()
        {
            _salesUnit.ReloadPaymentsPlannedFull();
            double paymentSum = _salesUnit.Cost/3;
            var payment = new PaymentActualWrapper(new PaymentActual {Sum = paymentSum});
            _salesUnit.PaymentsActual.Add(payment);
            Assert.IsTrue(Math.Abs(_salesUnit.Cost - paymentSum - _salesUnit.PaymentsPlanned.Sum(x => x.Sum)) < 0.0001);

            payment.Sum = paymentSum/2;
            Assert.IsTrue(Math.Abs(_salesUnit.Cost - paymentSum/2 - _salesUnit.PaymentsPlanned.Sum(x => x.Sum)) < 0.0001);

            _salesUnit.PaymentsActual.Clear();
            Assert.IsTrue(Math.Abs(_salesUnit.Cost - _salesUnit.PaymentsPlanned.Sum(x => x.Sum)) < 0.0001);
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
