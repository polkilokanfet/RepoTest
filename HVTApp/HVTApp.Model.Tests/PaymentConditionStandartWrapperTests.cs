using System;
using HVTApp.Model.Factory;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class PaymentConditionStandartWrapperTests
    {
        [TestMethod]
        public void PaymentConditionStandartWrapperValidation()
        {
            var wrapper = WrappersFactory.GetWrapper <PaymentConditionStandart, PaymentConditionStandartWrapper> ();
            Assert.IsFalse(wrapper.IsValid);

            wrapper.PaymentsConditions.Add(WrappersFactory.GetWrapper<PaymentCondition, PaymentConditionWrapper>(new PaymentCondition { PartInPercent = 30 }));
            Assert.IsFalse(wrapper.IsValid);

            wrapper.PaymentsConditions.Add(WrappersFactory.GetWrapper <PaymentCondition, PaymentConditionWrapper> (new PaymentCondition { PartInPercent = 70 }));
            Assert.IsTrue(wrapper.IsValid);
        }
    }
}
