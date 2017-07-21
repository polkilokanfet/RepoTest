using System;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class PaymentConditionStandartWrapperTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void InitializeMethod()
        {
            _fixture = FixtureTest.GetFixture();
        }

        [TestMethod]
        public void PaymentConditionStandartWrapperValidation()
        {
            var wrappersFactory = new Factory.TestWrappersFactory();

            var wrapper = wrappersFactory.GetWrapper<StandartPaymentConditionsWrapper> ();
            Assert.IsFalse(wrapper.IsValid);

            PaymentCondition paymentCondition1 = _fixture.Build<PaymentCondition>().With(x => x.Part, 0.3).Create();
            wrapper.PaymentsConditions.Add(wrappersFactory.GetWrapper<PaymentConditionWrapper>(paymentCondition1));
            Assert.IsFalse(wrapper.IsValid);

            PaymentCondition paymentCondition2 = _fixture.Build<PaymentCondition>().With(x => x.Part, 1-paymentCondition1.Part).Create();
            wrapper.PaymentsConditions.Add(wrappersFactory.GetWrapper<PaymentConditionWrapper>(paymentCondition2));
            Assert.IsTrue(wrapper.IsValid);
        }
    }
}
