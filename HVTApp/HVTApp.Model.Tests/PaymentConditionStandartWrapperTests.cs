using System;
using System.Linq;
using AutoFixture.AutoEF;
using HVTApp.DataAccess;
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
            _fixture = new Fixture();

            _fixture.Customize(new EntityCustomization(new DbContextEntityTypesProvider(typeof(HVTAppContext))));

            //отключаем поведение - бросать ошибку при обнаружении циклической связи
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            //подключаем поведение - останавливаться на стандартной глубине рекурсии
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public void PaymentConditionStandartWrapperValidation()
        {
            var wrapper = WrappersFactory.GetWrapper<StandartPaymentConditionsWrapper> ();
            Assert.IsFalse(wrapper.IsValid);

            wrapper.PaymentsConditions.Add(WrappersFactory.GetWrapper<PaymentConditionWrapper>(new PaymentCondition { Part = 0.30 }));
            Assert.IsFalse(wrapper.IsValid);

            wrapper.PaymentsConditions.Add(WrappersFactory.GetWrapper<PaymentConditionWrapper>(new PaymentCondition { Part = 0.70 }));
            Assert.IsTrue(wrapper.IsValid);
        }
    }
}
