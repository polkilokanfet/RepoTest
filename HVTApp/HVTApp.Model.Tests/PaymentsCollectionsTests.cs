using System;
using System.Linq;
using HVTApp.Model.PaymentsCollections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class PaymentsCollectionsTests
    {
        [TestMethod]
        public void PaymentsCollectionsTest()
        {
            ProductMain product = new ProductMain
            {
                CostInfo = new CostInfo()
                {
                    Cost = 50,
                    CostPrice = 25,
                    Vat = 18
                }
            };
            PaymentsInfo paymentsInfo = new PaymentsInfo(product);
            product.PaymentsInfo = paymentsInfo;

            PaymentsCondition condition = new PaymentsCondition() {PaymentConditionPoint = PaymentConditionPoint.ProductionStart, DaysToPoint = 10, PartInPercent = 100};
            product.PaymentsInfo.PaymentsConditions.Add(condition);
            Assert.AreEqual(product.PaymentsInfo.PaymentsPlanned.Count, 1);
            Assert.AreEqual(product.PaymentsInfo.PaymentsPlanned.ToArray()[0].Sum, product.CostInfo.CostWithVat);

            bool actualPaymentsChange = false;
            bool plannedPaymentsReloaded = false;
            product.PaymentsInfo.PaymentsActual.CollectionChanged += (sender, args) => { actualPaymentsChange = true; };
            product.PaymentsInfo.PaymentsPlanned.CollectionReloaded += (sender, args) => { plannedPaymentsReloaded = true; };
            PaymentActual paymentActual = new PaymentActual() {Sum = 20, Date = DateTime.Today};
            product.PaymentsInfo.PaymentsActual.Add(paymentActual);

            Assert.IsTrue(actualPaymentsChange);
            Assert.IsTrue(plannedPaymentsReloaded);
            Assert.AreEqual(product.PaymentsInfo.PaymentsPlanned.Count, 1);
            Assert.AreEqual(product.PaymentsInfo.PaymentsPlanned.ToArray()[0].Sum, product.CostInfo.CostWithVat - paymentActual.Sum);

            actualPaymentsChange = false;
            product.PaymentsInfo.PaymentsActual.Remove(paymentActual);
            Assert.IsTrue(actualPaymentsChange);
            Assert.AreEqual(product.PaymentsInfo.PaymentsPlanned.Count, 1);
            Assert.AreEqual(product.PaymentsInfo.PaymentsPlanned.ToArray()[0].Sum, product.CostInfo.CostWithVat);

            product.CostInfo.Cost += 10;
            Assert.AreEqual(product.PaymentsInfo.PaymentsPlanned.ToArray()[0].Sum, product.CostInfo.CostWithVat);

            product.CostInfo.Vat = 0;
            Assert.AreEqual(product.PaymentsInfo.PaymentsPlanned.ToArray()[0].Sum, product.CostInfo.CostWithVat);

        }
    }
}
