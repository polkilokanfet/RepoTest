using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class PaymentsDetailsTests
    {
        [TestInitialize]
        public void Initialize()
        {
        }

        //[TestMethod]
        //public void PaymentToStartProductionTest()
        //{
        //    ProductMain productMain = new ProductMain
        //    {
        //        ShipmentCost = new ShipmentCost()
        //        {
        //            ShipmentCost = 50,
        //            Vat = 100
        //        }
        //    };
        //    PaymentsInfo paymentsInfo = new PaymentsInfo(productMain);
        //    productMain.PaymentsInfo = paymentsInfo;
        //    Assert.AreEqual(productMain.PaymentsInfo.PaymentsSumToStartProduction, 0.0);

        //    double part = 25;
        //    productMain.PaymentsInfo.PaymentsConditions.Add(new PaymentCondition { PartInPercent = part, PaymentConditionPoint = PaymentConditionPoint.ProductionStart });
        //    Assert.AreEqual(productMain.PaymentsInfo.PaymentsSumToStartProduction, part * productMain.ShipmentCost.SumWithVat);

        //    part = 25;
        //    productMain.PaymentsInfo.PaymentsConditions.Add(new PaymentCondition { PartInPercent = part, PaymentConditionPoint = PaymentConditionPoint.ProductionEnd });
        //    Assert.AreEqual(productMain.PaymentsInfo.PaymentsSumToStartProduction, part * productMain.ShipmentCost.SumWithVat);

        //    part = 24;
        //    productMain.PaymentsInfo.PaymentsConditions.Add(new PaymentCondition { PartInPercent = part, PaymentConditionPoint = PaymentConditionPoint.ProductionStart });
        //    Assert.AreEqual(productMain.PaymentsInfo.PaymentsSumToStartProduction, (25 + 24) * productMain.ShipmentCost.SumWithVat);
        //}
    }
}
