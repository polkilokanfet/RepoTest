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

            _fixture.Customize<ParameterGroup>(p => p.With(x => x.Parameters, new List<Parameter>()));


            Parameter parameter1 = new Parameter { Value = "parameter1" };
            Parameter parameter2 = new Parameter { Value = "parameter2" };
            Parameter parameter3 = new Parameter { Value = "parameter3" };
            Parameter parameter4 = new Parameter { Value = "parameter4" };
            Parameter parameter5 = new Parameter { Value = "parameter5" };
            Parameter parameter6 = new Parameter { Value = "parameter6" };
            Parameter parameter7 = new Parameter { Value = "parameter7" };

            _productItem1 = new ProductItem { Designation = "ProductItem1", Parameters = new List<Parameter> { parameter1, parameter2 } };
            _productItem2 = new ProductItem { Designation = "ProductItem2", Parameters = new List<Parameter> { parameter3, parameter4 } };
            _productItem3 = new ProductItem { Designation = "ProductItem3", Parameters = new List<Parameter> { parameter5, parameter6 } };
            _productItem4 = new ProductItem { Designation = "ProductItem4", Parameters = new List<Parameter> { parameter7 } };
        }

        [TestMethod]
        public void ProductTotalPriceTest()
        {
            var product = _fixture.Build<Product>().Create();
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
