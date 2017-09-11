using System;
using System.Collections.Generic;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using HVTApp.TestDataGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class PartTests
    {
        [TestMethod]
        public void PartSameParametersTest()
        {
            TestData testData = new TestData();
            var wrappersFactory = new Factory.TestWrappersFactory();

            //список параметров различен
            PartWrapper partWrapper1 = wrappersFactory.GetWrapper<PartWrapper> (testData.PartVeb110);
            PartWrapper partWrapper2 = wrappersFactory.GetWrapper<PartWrapper> (testData.PartZng110);
            Assert.IsFalse(partWrapper1.HasSameParameters(partWrapper2));

            //уравниваем списки параметров
            var pl1 = new List<ParameterWrapper>(partWrapper1.Parameters);
            var pl2 = new List<ParameterWrapper>(partWrapper2.Parameters);
            pl2.ForEach(partWrapper1.Parameters.Add);
            pl1.ForEach(partWrapper2.Parameters.Add);
            //теперь списки схожи
            Assert.IsTrue(partWrapper1.HasSameParameters(partWrapper2));
        }

        [TestMethod]
        public void PartCostTest()
        {
            var factory = new Factory.TestWrappersFactory();

            PartWrapper partWrapper = factory.GetWrapper<PartWrapper>();

            //при пустом списке себистоимости ловим ошибку
            try
            {
                partWrapper.GetPrice();
            }
            catch (ArgumentException e)
            {
                Assert.AreSame(e.Message, "Нет себистоимости для этой даты (или для более ранней даты)");
            }

            //берем себестоимость для ближайшей ранней даты
            partWrapper.Prices.Add(factory.GetWrapper<CostOnDateWrapper>(new CostOnDate { Date = DateTime.Today.AddDays(-5), Cost = 10 }));
            partWrapper.Prices.Add(factory.GetWrapper<CostOnDateWrapper>(new CostOnDate { Date = DateTime.Today.AddDays(-7), Cost = 15 }));
            partWrapper.Prices.Add(factory.GetWrapper<CostOnDateWrapper>(new CostOnDate { Date = DateTime.Today.AddDays(10), Cost = 20 }));
            Assert.AreEqual(partWrapper.GetPrice(), 10);

            //берем себистоимость соответствующей даты
            partWrapper.Prices.Add(factory.GetWrapper<CostOnDateWrapper>(new CostOnDate { Date = DateTime.Today, Cost = 25 }));
            Assert.AreEqual(partWrapper.GetPrice(), 25);
            Assert.AreEqual(partWrapper.GetPrice(DateTime.Today.AddDays(10)), 20);
        }
    }
}
