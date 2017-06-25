using System;
using System.Collections.Generic;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class ProductTests
    {

        private Parameter _parameter1, _parameter2, _parameter3, _parameter4, _parameter5, _parameter6, _parameter7;
        private ProductItem _productItem1, _productItem2, _productItem3, _productItem4;

        [TestInitialize]
        public void Init()
        {
            _parameter1 = new Parameter { Id = 1, Value = "parameter1" };
            _parameter2 = new Parameter { Id = 2, Value = "parameter2" };
            _parameter3 = new Parameter { Id = 3, Value = "parameter3" };
            _parameter4 = new Parameter { Id = 4, Value = "parameter4" };
            _parameter5 = new Parameter { Id = 5, Value = "parameter5" };
            _parameter6 = new Parameter { Id = 6, Value = "parameter6" };
            _parameter7 = new Parameter { Id = 7, Value = "parameter7" };

            _productItem1 = new ProductItem { Id = 1, Designation = "ProductItem1", Parameters = new List<Parameter> { _parameter1, _parameter2 } };
            _productItem2 = new ProductItem { Id = 2, Designation = "ProductItem2", Parameters = new List<Parameter> { _parameter3, _parameter4 } };
            _productItem3 = new ProductItem { Id = 3, Designation = "ProductItem3", Parameters = new List<Parameter> { _parameter5, _parameter6 } };
            _productItem4 = new ProductItem { Id = 4, Designation = "ProductItem3", Parameters = new List<Parameter> { _parameter7 } };
        }

        [TestMethod]
        public void ProductTotalPriceTest()
        {
            Fixture fixture = new Fixture();
            CostOnDate price1 = fixture.Create<CostOnDate>();
            CostOnDate price2 = fixture.Create<CostOnDate>();

            var productParent = WrappersFactory.GetWrapper<ProductWrapper>(new Product {ProductItem = _productItem1});
            productParent.ProductItem.Prices.Add(WrappersFactory.GetWrapper<CostOnDateWrapper> (price1));
            productParent.ProductItem.Prices.Add(WrappersFactory.GetWrapper<CostOnDateWrapper> (price2));
            productParent.TotalPriceDate = DateTime.Today;

            Assert.IsTrue(Math.Abs(productParent.TotalPrice - price2.Cost.Sum) < 0.0001);

            CostOnDate price3 = fixture.Create<CostOnDate>();
            CostOnDate price4 = fixture.Create<CostOnDate>();

            var productChild1 = WrappersFactory.GetWrapper<ProductWrapper>(new Product {ProductItem = new ProductItem()});
            productChild1.ProductItem.Prices.Add(WrappersFactory.GetWrapper<CostOnDateWrapper> (price3));

            var productChild2 = WrappersFactory.GetWrapper<ProductWrapper>(new Product {ProductItem = new ProductItem()});
            productChild2.ProductItem.Prices.Add(WrappersFactory.GetWrapper<CostOnDateWrapper> (price4));

            productParent.ChildProducts.Add(productChild1);
            productParent.ChildProducts.Add(productChild2);

            var totalSum = price2.Cost.Sum + price3.Cost.Sum + price4.Cost.Sum;
            Assert.IsTrue(Math.Abs(productParent.TotalPrice - totalSum) < 0.0001);

            CostOnDate price5 = fixture.Create<CostOnDate>();
            var productChild3 = WrappersFactory.GetWrapper<ProductWrapper>(new Product {ProductItem = new ProductItem()});
            productChild3.ProductItem.Prices.Add(WrappersFactory.GetWrapper<CostOnDateWrapper> (price5));
            productChild1.ChildProducts.Add(productChild3);

            totalSum += price5.Cost.Sum;
            Assert.IsTrue(Math.Abs(productParent.TotalPrice - totalSum) < 0.0001);

        }

        [TestMethod]
        public void ProductItemsSameParametersTest()
        {
            ProductItemWrapper productItemWrapper1 = WrappersFactory.GetWrapper<ProductItemWrapper> (_productItem1);
            ProductItemWrapper productItemWrapper2 = WrappersFactory.GetWrapper<ProductItemWrapper> (_productItem2);

            Assert.IsFalse(productItemWrapper1.HasSameParameters(productItemWrapper2));

            var pl1 = new List<ParameterWrapper>(productItemWrapper1.Parameters);
            var pl2 = new List<ParameterWrapper>(productItemWrapper2.Parameters);
            pl2.ForEach(productItemWrapper1.Parameters.Add);
            pl1.ForEach(productItemWrapper2.Parameters.Add);

            Assert.IsTrue(productItemWrapper1.HasSameParameters(productItemWrapper2));
        }
    }
}
