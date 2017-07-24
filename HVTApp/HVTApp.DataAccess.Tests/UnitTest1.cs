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
        //        Sum = new Sum
        //        {
        //            Sum = 100,
        //            CostOnDate = 50,
        //            Vat = 20
        //        },
        //        TermsInfo = new TermsInfo(),
        //    };
        //    PaymentsInfo paymentsInfo = new PaymentsInfo(product);
        //    product.PaymentsInfo = paymentsInfo;


        //    Condition condition = new Condition
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

        //    Assert.AreEqual(product.PaymentsInfo.PaymentsPlaned.Count, product.PaymentsInfo.PaymentsConditions.Count);
        //    Assert.AreEqual(product.PaymentsInfo.PaymentsPlaned.ToList().Sum(x => x.Sum), product.Sum.CostWithVat);

        //    bool actualPaymentsChange = false;
        //    bool plannedPaymentsReloaded = false;
        //    product.PaymentsInfo.PaymentsActual.CollectionChanged += (sender, args) => { actualPaymentsChange = true; };
        //    product.PaymentsInfo.PaymentsPlaned.CollectionReloaded += (sender, args) => { plannedPaymentsReloaded = true; };

        //    PaymentActual paymentActual = new PaymentActual
        //    {
        //        Sum = 20,
        //        Date = DateTime.Today
        //    };
        //    product.PaymentsInfo.PaymentsActual.Add(paymentActual);

        //    Assert.IsTrue(actualPaymentsChange);
        //    Assert.IsTrue(plannedPaymentsReloaded);

        //    Assert.AreEqual(product.PaymentsInfo.PaymentsPlaned.Count, 1);
        //    Assert.AreEqual(product.PaymentsInfo.PaymentsPlaned.ToList().Sum(x => x.Sum), product.Sum.CostWithVat - paymentActual.Sum);

        //    actualPaymentsChange = false;
        //    plannedPaymentsReloaded = false;
        //    product.PaymentsInfo.PaymentsActual.Remove(paymentActual);

        //    Assert.IsTrue(actualPaymentsChange);
        //    Assert.IsTrue(plannedPaymentsReloaded);

        //    Assert.AreEqual(product.PaymentsInfo.PaymentsPlaned.Count, 1);
        //    Assert.AreEqual(product.PaymentsInfo.PaymentsPlaned.ToList().Sum(x => x.Sum), product.Sum.CostWithVat);

        //    plannedPaymentsReloaded = false;
        //    product.Sum.Sum += 10;
        //    Assert.IsTrue(plannedPaymentsReloaded);
        //    Assert.AreEqual(product.PaymentsInfo.PaymentsPlaned.ToList().Sum(x => x.Sum), product.Sum.CostWithVat);

        //    plannedPaymentsReloaded = false;
        //    product.Sum.Vat = 0;
        //    Assert.IsTrue(plannedPaymentsReloaded);
        //    Assert.AreEqual(product.PaymentsInfo.PaymentsPlaned.ToList().Sum(x => x.Sum), product.Sum.CostWithVat);

        //    product.PaymentsInfo.PaymentsConditions.Clear();
        //    Condition condition1 = new Condition
        //    {
        //        PaymentConditionPoint = PaymentConditionPoint.ProductionStart,
        //        DaysToPoint = 0,
        //        Part = 50
        //    };
        //    Condition condition2 = new Condition
        //    {
        //        PaymentConditionPoint = PaymentConditionPoint.ProductionEnd,
        //        DaysToPoint = 10,
        //        Part = 50
        //    };

        //    product.PaymentsInfo.PaymentsConditions.Add(condition1);
        //    product.PaymentsInfo.PaymentsConditions.Add(condition2);

        //    Assert.AreEqual(product.PaymentsInfo.PaymentsPlaned.Count, product.PaymentsInfo.PaymentsConditions.Count);
        //}
    }
}
