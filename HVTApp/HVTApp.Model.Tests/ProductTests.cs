using System;
using System.Collections.Generic;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void ProductTotalPriceTest()
        {
            SumOnDate price1 = new SumOnDate { Sum = 10, Date = DateTime.Today.AddDays(-7) };
            SumOnDate price2 = new SumOnDate { Sum = 5, Date = DateTime.Today };

            var productParent = new ProductWrapper();
            productParent.Prices.Add(new SumOnDateWrapper(price1));
            productParent.Prices.Add(new SumOnDateWrapper(price2));
            productParent.TotalPriceDate = DateTime.Today;

            Assert.IsTrue(Math.Abs(productParent.TotalPrice - price2.Sum) < 0.0001);

            SumOnDate price3 = new SumOnDate { Sum = 30, Date = DateTime.Today };
            SumOnDate price4 = new SumOnDate { Sum = 40, Date = DateTime.Today };

            var productChild1 = new ProductWrapper();
            productChild1.Prices.Add(new SumOnDateWrapper(price3));

            var productChild2 = new ProductWrapper();
            productChild2.Prices.Add(new SumOnDateWrapper(price4));

            productParent.ChildProducts.Add(productChild1);
            productParent.ChildProducts.Add(productChild2);

            var totalSum = price2.Sum + price3.Sum + price4.Sum;
            Assert.IsTrue(Math.Abs(productParent.TotalPrice - totalSum) < 0.0001);

            SumOnDate price5 = new SumOnDate { Sum = 50, Date = DateTime.Today };
            var productChild3 = new ProductWrapper();
            productChild3.Prices.Add(new SumOnDateWrapper(price5));
            productChild1.ChildProducts.Add(productChild3);

            totalSum += price5.Sum;
            Assert.IsTrue(Math.Abs(productParent.TotalPrice - totalSum) < 0.0001);

        }

        [TestMethod]
        public void ProductSameParametersTest()
        {
            Parameter p1 = new Parameter { Value = "p1p" };
            Parameter p2 = new Parameter { Value = "p2p" };

            Product pr1 = new Product();
            pr1.Parameters.Add(p1);
            Product pr2 = new Product();
            pr1.Parameters.Add(p2);

            ProductWrapper pr1W = new ProductWrapper(pr1);
            ProductWrapper pr2W = new ProductWrapper(pr2);

            Assert.IsFalse(pr1W.HasSameParameters(pr2W));

            var pl1 = new List<ParameterWrapper>(pr1W.Parameters);
            var pl2 = new List<ParameterWrapper>(pr2W.Parameters);
            pl2.ForEach(pr1W.Parameters.Add);
            pl1.ForEach(pr2W.Parameters.Add);

            Assert.IsTrue(pr1W.HasSameParameters(pr2W));
        }
    }
}
