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

            var productParent = WrappersFactory.GetWrapper<Product, ProductWrapper>(new Product {ProductItem = new ProductItem()});
            productParent.ProductItem.Prices.Add(WrappersFactory.GetWrapper <SumOnDate, SumOnDateWrapper> (price1));
            productParent.ProductItem.Prices.Add(WrappersFactory.GetWrapper <SumOnDate, SumOnDateWrapper> (price2));
            productParent.TotalPriceDate = DateTime.Today;

            Assert.IsTrue(Math.Abs(productParent.TotalPrice - price2.Sum) < 0.0001);

            SumOnDate price3 = new SumOnDate { Sum = 30, Date = DateTime.Today };
            SumOnDate price4 = new SumOnDate { Sum = 40, Date = DateTime.Today };

            var productChild1 = WrappersFactory.GetWrapper<Product, ProductWrapper>(new Product {ProductItem = new ProductItem()});
            productChild1.ProductItem.Prices.Add(WrappersFactory.GetWrapper <SumOnDate, SumOnDateWrapper> (price3));

            var productChild2 = WrappersFactory.GetWrapper<Product, ProductWrapper>(new Product {ProductItem = new ProductItem()});
            productChild2.ProductItem.Prices.Add(WrappersFactory.GetWrapper <SumOnDate, SumOnDateWrapper> (price4));

            productParent.ChildProducts.Add(productChild1);
            productParent.ChildProducts.Add(productChild2);

            var totalSum = price2.Sum + price3.Sum + price4.Sum;
            Assert.IsTrue(Math.Abs(productParent.TotalPrice - totalSum) < 0.0001);

            SumOnDate price5 = new SumOnDate { Sum = 50, Date = DateTime.Today };
            var productChild3 = WrappersFactory.GetWrapper<Product, ProductWrapper>(new Product {ProductItem = new ProductItem()});
            productChild3.ProductItem.Prices.Add(WrappersFactory.GetWrapper <SumOnDate, SumOnDateWrapper> (price5));
            productChild1.ChildProducts.Add(productChild3);

            totalSum += price5.Sum;
            Assert.IsTrue(Math.Abs(productParent.TotalPrice - totalSum) < 0.0001);

        }

        [TestMethod]
        public void ProductSameParametersTest()
        {
            Parameter p1 = new Parameter { Value = "p1p" };
            Parameter p2 = new Parameter { Value = "p2p" };

            ProductItem pr1 = new ProductItem();
            pr1.Parameters.Add(p1);
            pr1.Parameters.Add(p2);
            ProductItem pr2 = new ProductItem();

            ProductItemWrapper pr1W = WrappersFactory.GetWrapper <ProductItem, ProductItemWrapper> (pr1);
            ProductItemWrapper pr2W = WrappersFactory.GetWrapper <ProductItem, ProductItemWrapper> (pr2);

            Assert.IsFalse(pr1W.HasSameParameters(pr2W));

            var pl1 = new List<ParameterWrapper>(pr1W.Parameters);
            var pl2 = new List<ParameterWrapper>(pr2W.Parameters);
            pl2.ForEach(pr1W.Parameters.Add);
            pl1.ForEach(pr2W.Parameters.Add);

            Assert.IsTrue(pr1W.HasSameParameters(pr2W));
        }
    }
}
