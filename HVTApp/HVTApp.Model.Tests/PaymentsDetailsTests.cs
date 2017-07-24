using HVTApp.Model.POCOs;
using HVTApp.Model.Tests.Factory;
using HVTApp.Model.Wrappers;
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

        [TestMethod]
        public void PaymentToStartProductionTest()
        {
            TestWrappersFactory factory = new TestWrappersFactory();

            SalesUnit salesUnit = new SalesUnit();
            SalesUnitWrapper suw = factory.GetWrapper<SalesUnitWrapper>(salesUnit);

            Assert.AreEqual(suw.SumToStartProduction, 0.0);

            //вносим условие - оплатить до запуска производства
            double part1 = 25;
            PaymentCondition paymentCondition1 = new PaymentCondition
            {Part = part1, DaysToPoint = -7, PaymentConditionPoint = PaymentConditionPoint.ProductionStart};
            suw.PaymentsConditions.Add(factory.GetWrapper<PaymentConditionWrapper>(paymentCondition1));
            Assert.AreEqual(suw.SumToStartProduction, part1 * salesUnit.Cost);

            //вносим условие - оплатить после запуска производства
            PaymentCondition paymentCondition2 = new PaymentCondition
            {Part = part1, DaysToPoint = 10, PaymentConditionPoint = PaymentConditionPoint.ProductionStart};
            suw.PaymentsConditions.Add(factory.GetWrapper<PaymentConditionWrapper>(paymentCondition2));
            Assert.AreEqual(suw.SumToStartProduction, part1 * salesUnit.Cost);

            //вносим условие - оплатить до окончания производства
            PaymentCondition paymentCondition3 = new PaymentCondition
            { Part = part1, DaysToPoint = -5, PaymentConditionPoint = PaymentConditionPoint.ProductionEnd };
            suw.PaymentsConditions.Add(factory.GetWrapper<PaymentConditionWrapper>(paymentCondition3));
            Assert.AreEqual(suw.SumToStartProduction, part1 * salesUnit.Cost);

            //вносим условие - оплатить до запуска производства
            double part2 = 20;
            PaymentCondition paymentCondition4 = new PaymentCondition
            { Part = part2, DaysToPoint = 0, PaymentConditionPoint = PaymentConditionPoint.ProductionStart };
            suw.PaymentsConditions.Add(factory.GetWrapper<PaymentConditionWrapper>(paymentCondition4));
            Assert.AreEqual(suw.SumToStartProduction, (part1 + part2) * salesUnit.Cost);
        }
    }
}
