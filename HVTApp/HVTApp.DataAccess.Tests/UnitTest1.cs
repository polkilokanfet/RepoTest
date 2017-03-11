using System;
using System.Linq;
using HVTApp.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable All

namespace HVTApp.DataAccess.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ProductMain product = new ProductMain
            {
                CostInfo = new CostInfo
                {
                    Cost = 100,
                    CostPrice = 50,
                    Vat = 20
                },
                TermsInfo = new TermsInfo(),
            };
            PaymentsInfo paymentsInfo = new PaymentsInfo(product);
            product.PaymentsInfo = paymentsInfo;


            PaymentsCondition condition = new PaymentsCondition
            {
                PaymentConditionPoint = PaymentConditionPoint.ProductionStart,
                DaysToPoint = 10,
                PartInPercent = 100
            };
            product.PaymentsInfo.PaymentsConditions.Add(condition);

            UnitOfWork unitOfWork = new UnitOfWork(new HVTAppContext());
            unitOfWork.ProductsMain.Add(product);
            unitOfWork.Complete();

            unitOfWork = new UnitOfWork(new HVTAppContext());
            product = unitOfWork.ProductsMain.GetAll().Single(x => x.Id == product.Id);

            Assert.AreEqual(product.PaymentsInfo.PaymentsPlanned.Count, product.PaymentsInfo.PaymentsConditions.Count);
            Assert.AreEqual(product.PaymentsInfo.PaymentsPlanned.ToList().Sum(x => x.Sum), product.CostInfo.CostWithVat);

            bool actualPaymentsChange = false;
            bool plannedPaymentsReloaded = false;
            product.PaymentsInfo.PaymentsActual.CollectionChanged += (sender, args) => { actualPaymentsChange = true; };
            product.PaymentsInfo.PaymentsPlanned.CollectionReloaded += (sender, args) => { plannedPaymentsReloaded = true; };

            PaymentActual paymentActual = new PaymentActual
            {
                Sum = 20,
                Date = DateTime.Today
            };
            product.PaymentsInfo.PaymentsActual.Add(paymentActual);

            Assert.IsTrue(actualPaymentsChange);
            Assert.IsTrue(plannedPaymentsReloaded);

            Assert.AreEqual(product.PaymentsInfo.PaymentsPlanned.Count, 1);
            Assert.AreEqual(product.PaymentsInfo.PaymentsPlanned.ToList().Sum(x => x.Sum), product.CostInfo.CostWithVat - paymentActual.Sum);

            actualPaymentsChange = false;
            plannedPaymentsReloaded = false;
            product.PaymentsInfo.PaymentsActual.Remove(paymentActual);

            Assert.IsTrue(actualPaymentsChange);
            Assert.IsTrue(plannedPaymentsReloaded);

            Assert.AreEqual(product.PaymentsInfo.PaymentsPlanned.Count, 1);
            Assert.AreEqual(product.PaymentsInfo.PaymentsPlanned.ToList().Sum(x => x.Sum), product.CostInfo.CostWithVat);

            plannedPaymentsReloaded = false;
            product.CostInfo.Cost += 10;
            Assert.IsTrue(plannedPaymentsReloaded);
            Assert.AreEqual(product.PaymentsInfo.PaymentsPlanned.ToList().Sum(x => x.Sum), product.CostInfo.CostWithVat);

            plannedPaymentsReloaded = false;
            product.CostInfo.Vat = 0;
            Assert.IsTrue(plannedPaymentsReloaded);
            Assert.AreEqual(product.PaymentsInfo.PaymentsPlanned.ToList().Sum(x => x.Sum), product.CostInfo.CostWithVat);

            product.PaymentsInfo.PaymentsConditions.Clear();
            PaymentsCondition condition1 = new PaymentsCondition
            {
                PaymentConditionPoint = PaymentConditionPoint.ProductionStart,
                DaysToPoint = 0,
                PartInPercent = 50
            };
            PaymentsCondition condition2 = new PaymentsCondition
            {
                PaymentConditionPoint = PaymentConditionPoint.ProductionEnd,
                DaysToPoint = 10,
                PartInPercent = 50
            };

            product.PaymentsInfo.PaymentsConditions.Add(condition1);
            product.PaymentsInfo.PaymentsConditions.Add(condition2);

            Assert.AreEqual(product.PaymentsInfo.PaymentsPlanned.Count, product.PaymentsInfo.PaymentsConditions.Count);
        }
    }
}
