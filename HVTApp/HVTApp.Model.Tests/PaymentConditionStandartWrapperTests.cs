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
            var wrapper = new PaymentConditionStandartWrapper();
            Assert.IsFalse(wrapper.IsValid);

            wrapper.PaymentsConditions.Add(new PaymentConditionWrapper(new PaymentCondition { PartInPercent = 30 }));
            Assert.IsFalse(wrapper.IsValid);

            wrapper.PaymentsConditions.Add(new PaymentConditionWrapper(new PaymentCondition { PartInPercent = 70 }));
            Assert.IsTrue(wrapper.IsValid);
        }
    }
}
