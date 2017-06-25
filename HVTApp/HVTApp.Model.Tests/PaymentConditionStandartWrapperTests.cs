using System;
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
            var wrapper = WrappersFactory.GetWrapper<PaymentConditionStandartWrapper> ();
            Assert.IsFalse(wrapper.IsValid);

            wrapper.PaymentsConditions.Add(WrappersFactory.GetWrapper<PaymentConditionWrapper>(new PaymentCondition { Part = 30 }));
            Assert.IsFalse(wrapper.IsValid);

            wrapper.PaymentsConditions.Add(WrappersFactory.GetWrapper<PaymentConditionWrapper>(new PaymentCondition { Part = 70 }));
            Assert.IsTrue(wrapper.IsValid);
        }
    }
}
