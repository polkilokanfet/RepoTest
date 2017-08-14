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
        //    ProductMain Product = new ProductMain
        //    {
        //        Sum = new Sum
        //        {
        //            Sum = 100,
        //            CostOnDate = 50,
        //            Vat = 20
        //        },
        //        TermsInfo = new TermsInfo(),
        //    };
        //    PaymentsInfo paymentsInfo = new PaymentsInfo(Product);
        //    Product.PaymentsInfo = paymentsInfo;


        //    Condition condition = new Condition
        //    {
        //        PaymentConditionPoint = PaymentConditionPoint.ProductionStart,
        //        DaysToPoint = 10,
        //        Part = 100
        //    };
        //    Product.PaymentsInfo.PaymentsConditions.Add(condition);

        //    UnitOfWork unitOfWork = new UnitOfWork(new HVTAppContext());
        //    unitOfWork.ProductsMain.Add(Product);
        //    unitOfWork.Complete();

        //    unitOfWork = new UnitOfWork(new HVTAppContext());
        //    Product = unitOfWork.ProductsMain.GetAll().Single(x => x.Id == Product.Id);

        //    Assert.AreEqual(Product.PaymentsInfo.PaymentsPlaned.Count, Product.PaymentsInfo.PaymentsConditions.Count);
        //    Assert.AreEqual(Product.PaymentsInfo.PaymentsPlaned.ToList().Sum(x => x.Sum), Product.Sum.CostWithVat);

        //    bool actualPaymentsChange = false;
        //    bool plannedPaymentsReloaded = false;
        //    Product.PaymentsInfo.PaymentsActual.CollectionChanged += (sender, args) => { actualPaymentsChange = true; };
        //    Product.PaymentsInfo.PaymentsPlaned.CollectionReloaded += (sender, args) => { plannedPaymentsReloaded = true; };

        //    PaymentActual paymentActual = new PaymentActual
        //    {
        //        Sum = 20,
        //        Date = DateTime.Today
        //    };
        //    Product.PaymentsInfo.PaymentsActual.Add(paymentActual);

        //    Assert.IsTrue(actualPaymentsChange);
        //    Assert.IsTrue(plannedPaymentsReloaded);

        //    Assert.AreEqual(Product.PaymentsInfo.PaymentsPlaned.Count, 1);
        //    Assert.AreEqual(Product.PaymentsInfo.PaymentsPlaned.ToList().Sum(x => x.Sum), Product.Sum.CostWithVat - paymentActual.Sum);

        //    actualPaymentsChange = false;
        //    plannedPaymentsReloaded = false;
        //    product.PaymentsInfo.PaymentsActual.Remove(paymentActual);

        //    Assert.IsTrue(actualPaymentsChange);
        //    Assert.IsTrue(plannedPaymentsReloaded);

        //    Assert.AreEqual(Product.PaymentsInfo.PaymentsPlaned.Count, 1);
        //    Assert.AreEqual(Product.PaymentsInfo.PaymentsPlaned.ToList().Sum(x => x.Sum), Product.Sum.CostWithVat);

        //    plannedPaymentsReloaded = false;
        //    Product.Sum.Sum += 10;
        //    Assert.IsTrue(plannedPaymentsReloaded);
        //    Assert.AreEqual(Product.PaymentsInfo.PaymentsPlaned.ToList().Sum(x => x.Sum), Product.Sum.CostWithVat);

        //    plannedPaymentsReloaded = false;
        //    Product.Sum.Vat = 0;
        //    Assert.IsTrue(plannedPaymentsReloaded);
        //    Assert.AreEqual(Product.PaymentsInfo.PaymentsPlaned.ToList().Sum(x => x.Sum), Product.Sum.CostWithVat);

        //    Product.PaymentsInfo.PaymentsConditions.Clear();
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

        //    Product.PaymentsInfo.PaymentsConditions.Add(condition1);
        //    Product.PaymentsInfo.PaymentsConditions.Add(condition2);

        //    Assert.AreEqual(Product.PaymentsInfo.PaymentsPlaned.Count, Product.PaymentsInfo.PaymentsConditions.Count);
        //}
    }
}
