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
        //    ProductMain Equipment = new ProductMain
        //    {
        //        Sum = new Sum
        //        {
        //            Sum = 100,
        //            CostOnDate = 50,
        //            Vat = 20
        //        },
        //        TermsInfo = new TermsInfo(),
        //    };
        //    PaymentsInfo paymentsInfo = new PaymentsInfo(Equipment);
        //    Equipment.PaymentsInfo = paymentsInfo;


        //    Condition condition = new Condition
        //    {
        //        PaymentConditionPoint = PaymentConditionPoint.ProductionStart,
        //        DaysToPoint = 10,
        //        Part = 100
        //    };
        //    Equipment.PaymentsInfo.PaymentsConditions.Add(condition);

        //    UnitOfWork unitOfWork = new UnitOfWork(new HVTAppContext());
        //    unitOfWork.ProductsMain.Add(Equipment);
        //    unitOfWork.Complete();

        //    unitOfWork = new UnitOfWork(new HVTAppContext());
        //    Equipment = unitOfWork.ProductsMain.GetAll().Single(x => x.Id == Equipment.Id);

        //    Assert.AreEqual(Equipment.PaymentsInfo.PaymentsPlaned.Count, Equipment.PaymentsInfo.PaymentsConditions.Count);
        //    Assert.AreEqual(Equipment.PaymentsInfo.PaymentsPlaned.ToList().Sum(x => x.Sum), Equipment.Sum.CostWithVat);

        //    bool actualPaymentsChange = false;
        //    bool plannedPaymentsReloaded = false;
        //    Equipment.PaymentsInfo.PaymentsActual.CollectionChanged += (sender, args) => { actualPaymentsChange = true; };
        //    Equipment.PaymentsInfo.PaymentsPlaned.CollectionReloaded += (sender, args) => { plannedPaymentsReloaded = true; };

        //    PaymentActual paymentActual = new PaymentActual
        //    {
        //        Sum = 20,
        //        Date = DateTime.Today
        //    };
        //    Equipment.PaymentsInfo.PaymentsActual.Add(paymentActual);

        //    Assert.IsTrue(actualPaymentsChange);
        //    Assert.IsTrue(plannedPaymentsReloaded);

        //    Assert.AreEqual(Equipment.PaymentsInfo.PaymentsPlaned.Count, 1);
        //    Assert.AreEqual(Equipment.PaymentsInfo.PaymentsPlaned.ToList().Sum(x => x.Sum), Equipment.Sum.CostWithVat - paymentActual.Sum);

        //    actualPaymentsChange = false;
        //    plannedPaymentsReloaded = false;
        //    product.PaymentsInfo.PaymentsActual.Remove(paymentActual);

        //    Assert.IsTrue(actualPaymentsChange);
        //    Assert.IsTrue(plannedPaymentsReloaded);

        //    Assert.AreEqual(Equipment.PaymentsInfo.PaymentsPlaned.Count, 1);
        //    Assert.AreEqual(Equipment.PaymentsInfo.PaymentsPlaned.ToList().Sum(x => x.Sum), Equipment.Sum.CostWithVat);

        //    plannedPaymentsReloaded = false;
        //    Equipment.Sum.Sum += 10;
        //    Assert.IsTrue(plannedPaymentsReloaded);
        //    Assert.AreEqual(Equipment.PaymentsInfo.PaymentsPlaned.ToList().Sum(x => x.Sum), Equipment.Sum.CostWithVat);

        //    plannedPaymentsReloaded = false;
        //    Equipment.Sum.Vat = 0;
        //    Assert.IsTrue(plannedPaymentsReloaded);
        //    Assert.AreEqual(Equipment.PaymentsInfo.PaymentsPlaned.ToList().Sum(x => x.Sum), Equipment.Sum.CostWithVat);

        //    Equipment.PaymentsInfo.PaymentsConditions.Clear();
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

        //    Equipment.PaymentsInfo.PaymentsConditions.Add(condition1);
        //    Equipment.PaymentsInfo.PaymentsConditions.Add(condition2);

        //    Assert.AreEqual(Equipment.PaymentsInfo.PaymentsPlaned.Count, Equipment.PaymentsInfo.PaymentsConditions.Count);
        //}
    }
}
