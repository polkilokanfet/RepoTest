using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable All

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class ProductMainTests
    {
        [TestMethod]
        public void ProductMainEventsIsFired()
        {
            ProductMain product = new ProductMain();
            product.CostInfo = new CostInfo();
            product.PaymentsInfo = new PaymentsInfo(product);

            bool plannedPaymentsReloaded = false;

            product.PaymentsInfo.PaymentsPlanned.CollectionReloaded += (s, a) => { plannedPaymentsReloaded = true; };

            product.CostInfo.Cost = 100;
            Assert.IsTrue(plannedPaymentsReloaded);

            plannedPaymentsReloaded = false;
            product.CostInfo.Vat = 20;
            Assert.IsTrue(plannedPaymentsReloaded);

            PaymentsCondition condition = new PaymentsCondition
            {
                PaymentConditionPoint = PaymentConditionPoint.ProductionStart,
                DaysToPoint = 10,
                PartInPercent = 100
            };
            plannedPaymentsReloaded = false;
            product.PaymentsInfo.PaymentsConditions.Add(condition);
            Assert.IsTrue(plannedPaymentsReloaded);

            PaymentActual paymentActual = new PaymentActual
            {
                Sum = 20,
                Date = DateTime.Today
            };
            plannedPaymentsReloaded = false;
            product.PaymentsInfo.PaymentsActual.Add(paymentActual);
            Assert.IsTrue(plannedPaymentsReloaded);

            plannedPaymentsReloaded = false;
            product.PaymentsInfo.PaymentsActual.Clear();
            Assert.IsTrue(plannedPaymentsReloaded);

            plannedPaymentsReloaded = false;
            product.PaymentsInfo.PaymentsConditions.Clear();
            Assert.IsTrue(plannedPaymentsReloaded);
        }
    }
}
