using System;
using HVTApp.Model.POCOs;
using HVTApp.Model.Tests.Factory;
using HVTApp.Model.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

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
            Fixture fixture = FixtureTest.GetFixture();
            TestWrappersFactory factory = new TestWrappersFactory();

            SalesUnit salesUnit = fixture.Build<SalesUnit>().Create();
            SalesUnitWrapper suw = factory.GetWrapper<SalesUnitWrapper>(salesUnit);

            Assert.AreEqual(suw.SumToStartProduction, 0.0);

            //вносим условие - оплатить до запуска производства
            double part1 = 25;
            PaymentCondition paymentCondition1 = fixture.Build<PaymentCondition>().
                With(x => x.Part, part1).
                With(x => x.PaymentConditionPoint, PaymentConditionPoint.ProductionStart).
                With(x => x.DaysToPoint, -7).Create();
            suw.PaymentsConditions.Add(factory.GetWrapper<PaymentConditionWrapper>(paymentCondition1));
            Assert.AreEqual(suw.SumToStartProduction, part1 * salesUnit.Cost);

            //вносим условие - оплатить после запуска производства
            PaymentCondition paymentCondition = fixture.Build<PaymentCondition>().
                With(x => x.Part, part1).
                With(x => x.PaymentConditionPoint, PaymentConditionPoint.ProductionStart).
                With(x => x.DaysToPoint, -7).Create();
            suw.PaymentsConditions.Add(factory.GetWrapper<PaymentConditionWrapper>(paymentCondition1));
            Assert.AreEqual(suw.SumToStartProduction, part1 * salesUnit.Cost);

            //вносим условие - оплатить до окончания производства
            PaymentCondition paymentCondition3 = fixture.Build<PaymentCondition>().
                With(x => x.PaymentConditionPoint, PaymentConditionPoint.ProductionEnd).Create();
            suw.PaymentsConditions.Add(factory.GetWrapper<PaymentConditionWrapper>(paymentCondition3));
            Assert.AreEqual(suw.SumToStartProduction, part1 * salesUnit.Cost);


            //вносим условие - оплатить до запуска производства
            double part2 = 20;
            PaymentCondition paymentCondition4 = fixture.Build<PaymentCondition>().
                With(x => x.Part, part2).
                With(x => x.PaymentConditionPoint, PaymentConditionPoint.ProductionStart).
                With(x => x.DaysToPoint, 0).Create();
            suw.PaymentsConditions.Add(factory.GetWrapper<PaymentConditionWrapper>(paymentCondition1));
            Assert.AreEqual(suw.SumToStartProduction, (part1 + part2) * salesUnit.Cost);
        }
    }
}
