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
        //[TestMethod]
        //public void TestMethod1()
        //{
        //    ProductMain product = new ProductMain
        //    {
        //        Cost = new Cost
        //        {
        //            Cost = 100,
        //            CostOnDate = 50,
        //            Vat = 20
        //        },
        //        TermsInfo = new TermsInfo(),
        //    };
        //    PaymentsInfo paymentsInfo = new PaymentsInfo(product);
        //    product.PaymentsInfo = paymentsInfo;


        //    PaymentCondition condition = new PaymentCondition
        //    {
        //        PaymentConditionPoint = PaymentConditionPoint.ProductionStart,
        //        DaysToPoint = 10,
        //        Part = 100
        //    };
        //    product.PaymentsInfo.PaymentsConditions.Add(condition);

        //    UnitOfWork unitOfWork = new UnitOfWork(new HVTAppContext());
        //    unitOfWork.ProductsMain.Add(product);
        //    unitOfWork.Complete();

        //    unitOfWork = new UnitOfWork(new HVTAppContext());
        //    product = unitOfWork.ProductsMain.GetAll().Single(x => x.Id == product.Id);

        //    Assert.AreEqual(product.PaymentsInfo.Payments.Count, product.PaymentsInfo.PaymentsConditions.Count);
        //    Assert.AreEqual(product.PaymentsInfo.Payments.ToList().Cost(x => x.Cost), product.Cost.CostWithVat);

        //    bool actualPaymentsChange = false;
        //    bool plannedPaymentsReloaded = false;
        //    product.PaymentsInfo.PaymentsActual.CollectionChanged += (sender, args) => { actualPaymentsChange = true; };
        //    product.PaymentsInfo.Payments.CollectionReloaded += (sender, args) => { plannedPaymentsReloaded = true; };

        //    Payment paymentActual = new Payment
        //    {
        //        Cost = 20,
        //        Date = DateTime.Today
        //    };
        //    product.PaymentsInfo.PaymentsActual.Add(paymentActual);

        //    Assert.IsTrue(actualPaymentsChange);
        //    Assert.IsTrue(plannedPaymentsReloaded);

        //    Assert.AreEqual(product.PaymentsInfo.Payments.Count, 1);
        //    Assert.AreEqual(product.PaymentsInfo.Payments.ToList().Cost(x => x.Cost), product.Cost.CostWithVat - paymentActual.Cost);

        //    actualPaymentsChange = false;
        //    plannedPaymentsReloaded = false;
        //    product.PaymentsInfo.PaymentsActual.Remove(paymentActual);

        //    Assert.IsTrue(actualPaymentsChange);
        //    Assert.IsTrue(plannedPaymentsReloaded);

        //    Assert.AreEqual(product.PaymentsInfo.Payments.Count, 1);
        //    Assert.AreEqual(product.PaymentsInfo.Payments.ToList().Cost(x => x.Cost), product.Cost.CostWithVat);

        //    plannedPaymentsReloaded = false;
        //    product.Cost.Cost += 10;
        //    Assert.IsTrue(plannedPaymentsReloaded);
        //    Assert.AreEqual(product.PaymentsInfo.Payments.ToList().Cost(x => x.Cost), product.Cost.CostWithVat);

        //    plannedPaymentsReloaded = false;
        //    product.Cost.Vat = 0;
        //    Assert.IsTrue(plannedPaymentsReloaded);
        //    Assert.AreEqual(product.PaymentsInfo.Payments.ToList().Cost(x => x.Cost), product.Cost.CostWithVat);

        //    product.PaymentsInfo.PaymentsConditions.Clear();
        //    PaymentCondition condition1 = new PaymentCondition
        //    {
        //        PaymentConditionPoint = PaymentConditionPoint.ProductionStart,
        //        DaysToPoint = 0,
        //        Part = 50
        //    };
        //    PaymentCondition condition2 = new PaymentCondition
        //    {
        //        PaymentConditionPoint = PaymentConditionPoint.ProductionEnd,
        //        DaysToPoint = 10,
        //        Part = 50
        //    };

        //    product.PaymentsInfo.PaymentsConditions.Add(condition1);
        //    product.PaymentsInfo.PaymentsConditions.Add(condition2);

        //    Assert.AreEqual(product.PaymentsInfo.Payments.Count, product.PaymentsInfo.PaymentsConditions.Count);
        //}
    }
}
