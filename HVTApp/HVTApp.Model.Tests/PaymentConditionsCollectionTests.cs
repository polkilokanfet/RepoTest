using System;
using System.Collections.Generic;
using HVTApp.Model.PaymentsCollections;
using HVTApp.Model.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class PaymentConditionsCollectionTests
    {
        [TestMethod]
        public void PaymentConditionsCollectionTest()
        {
            PaymentsConditionsCollection paymentsConditionsCollection = new PaymentsConditionsCollection();
            Assert.AreEqual(paymentsConditionsCollection.Count, 0);

            PaymentsCondition paymentsCondition = new PaymentsCondition { PartInPercent = 50 };
            paymentsConditionsCollection.Add(paymentsCondition);
            Assert.AreEqual(paymentsConditionsCollection.Count, 1);

            paymentsConditionsCollection.Remove(paymentsCondition);
            Assert.AreEqual(paymentsConditionsCollection.Count, 0);

            paymentsConditionsCollection.Add(new PaymentsCondition { PartInPercent = 60 });
            paymentsConditionsCollection.Add(paymentsCondition);
            Assert.AreEqual(paymentsConditionsCollection.Count, 2);
            Assert.AreEqual(paymentsCondition.PartInPercent, 40);

            paymentsConditionsCollection.Add(new PaymentsCondition { PartInPercent = 60 });
            Assert.AreEqual(paymentsConditionsCollection.Count, 2);

            paymentsConditionsCollection.Clear();
            Assert.AreEqual(paymentsConditionsCollection.Count, 0);
        }

        [TestMethod]
        public void PaymentConditionWrappersCollectionTest()
        {
            var collection = new PaymentsConditionWrappersCollection(new List<PaymentsConditionWrapper>());
            Assert.AreEqual(collection.Count, 0);

            var condition = PaymentsConditionWrapper.GetWrapper(new PaymentsCondition { PartInPercent = 50 });
            collection.Add(condition);
            Assert.AreEqual(collection.Count, 1);

            collection.Remove(condition);
            Assert.AreEqual(collection.Count, 0);

            collection.Add(PaymentsConditionWrapper.GetWrapper(new PaymentsCondition { PartInPercent = 60 }));
            collection.Add(condition);
            Assert.AreEqual(collection.Count, 2);
            Assert.AreEqual(condition.PartInPercent, 40);

            collection.Add(PaymentsConditionWrapper.GetWrapper(new PaymentsCondition { PartInPercent = 60 }));
            Assert.AreEqual(collection.Count, 2);

            collection.Clear();
            Assert.AreEqual(collection.Count, 0);
        }
    }
}
