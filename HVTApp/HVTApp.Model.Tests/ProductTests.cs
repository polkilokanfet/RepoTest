using System;
using System.Collections.Generic;
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
    public class ProductTests
    {
        private Fixture _fixture;
        private ProductItem _productItem1, _productItem2, _productItem3, _productItem4;

        [TestInitialize]
        public void InitializeMethod()
        {
            _fixture = new Fixture();

            _fixture.Customize(new EntityCustomization(new DbContextEntityTypesProvider(typeof(HVTAppContext))));

            //отключаем поведение - бросать ошибку при обнаружении циклической связи
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            //подключаем поведение - останавливаться на стандартной глубине рекурсии
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());


            Parameter parameter1 = new Parameter { Id = 1, Value = "parameter1" };
            Parameter parameter2 = new Parameter { Id = 2, Value = "parameter2" };
            Parameter parameter3 = new Parameter { Id = 3, Value = "parameter3" };
            Parameter parameter4 = new Parameter { Id = 4, Value = "parameter4" };
            Parameter parameter5 = new Parameter { Id = 5, Value = "parameter5" };
            Parameter parameter6 = new Parameter { Id = 6, Value = "parameter6" };
            Parameter parameter7 = new Parameter { Id = 7, Value = "parameter7" };

            _productItem1 = new ProductItem { Id = 1, Designation = "ProductItem1", Parameters = new List<Parameter> { parameter1, parameter2 } };
            _productItem2 = new ProductItem { Id = 2, Designation = "ProductItem2", Parameters = new List<Parameter> { parameter3, parameter4 } };
            _productItem3 = new ProductItem { Id = 3, Designation = "ProductItem3", Parameters = new List<Parameter> { parameter5, parameter6 } };
            _productItem4 = new ProductItem { Id = 4, Designation = "ProductItem4", Parameters = new List<Parameter> { parameter7 } };

            var iii = _fixture.Create<ProductItem>();
        }

        [TestMethod]
        public void ProductTotalPriceTest()
        {
            var product = _fixture.Create<Product>();
            var productParent = WrappersFactory.GetWrapper<ProductWrapper>(product);
            productParent.TotalPriceDate = productParent.ProductItem.Prices[1].Date;

            Assert.IsTrue(Math.Abs(productParent.TotalPrice - productParent.ProductItem.Prices[1].Cost.Sum) < 0.0001);

            var productChild1 = WrappersFactory.GetWrapper<ProductWrapper>(_fixture.Create<Product>());
            productChild1.ProductItem.Prices[1].Date = productParent.ProductItem.Prices[1].Date;
            productParent.ChildProducts.Add(productChild1);

            var productChild2 = WrappersFactory.GetWrapper<ProductWrapper>(_fixture.Create<Product>());
            productChild2.ProductItem.Prices[1].Date = productParent.ProductItem.Prices[1].Date;
            productParent.ChildProducts.Add(productChild2);


            var totalSum =  productParent.ProductItem.Prices[1].Cost.Sum + 
                            productChild1.ProductItem.Prices[1].Cost.Sum + 
                            productChild2.ProductItem.Prices[1].Cost.Sum;
            Assert.IsTrue(Math.Abs(productParent.TotalPrice - totalSum) < 0.0001);

            var productChild3 = WrappersFactory.GetWrapper<ProductWrapper>(_fixture.Create<Product>());
            productChild3.ProductItem.Prices[1].Date = productParent.ProductItem.Prices[1].Date;
            productParent.ChildProducts.Add(productChild3);

            totalSum += productChild3.ProductItem.Prices[1].Cost.Sum;
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
