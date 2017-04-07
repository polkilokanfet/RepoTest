using System;
using HVTApp.Model.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class PaymentConditionStandartWrapperTests
    {
        [TestMethod]
        public void PaymentConditionStandartWrapperValidation()
        {
            var wrapper = PaymentConditionStandartWrapper.GetWrapper();
            Assert.IsFalse(wrapper.IsValid);

            wrapper.PaymentsConditions.Add(PaymentConditionWrapper.GetWrapper(new PaymentCondition { PartInPercent = 30 }));
            Assert.IsFalse(wrapper.IsValid);

            wrapper.PaymentsConditions.Add(PaymentConditionWrapper.GetWrapper(new PaymentCondition { PartInPercent = 70 }));
            Assert.IsTrue(wrapper.IsValid);
        }
    }
}
