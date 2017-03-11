using System;
using HVTApp.Model.PaymentsCollections;
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

            PaymentsCondition paymentsCondition = new PaymentsCondition {PartInPercent = 50};
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
    }
}
