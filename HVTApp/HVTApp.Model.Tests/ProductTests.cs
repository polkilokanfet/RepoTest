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
        private Part _partItem1, _partItem2, _partItem3, _partItem4;

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

            _partItem1 = new Part { Designation = "ProductItem1", Parameters = new List<Parameter> { parameter1, parameter2 } };
            _partItem2 = new Part { Designation = "ProductItem2", Parameters = new List<Parameter> { parameter3, parameter4 } };
            _partItem3 = new Part { Designation = "ProductItem3", Parameters = new List<Parameter> { parameter5, parameter6 } };
            _partItem4 = new Part { Designation = "ProductItem4", Parameters = new List<Parameter> { parameter7 } };
        }

        [TestMethod]
        public void ProductTotalPriceTest()
        {
            var wrappersFactory = new Factory.TestWrappersFactory();

            var equipment = _fixture.Build<Product>().Create();
            var equipmentParent = wrappersFactory.GetWrapper<ProductWrapper>(equipment);
            equipmentParent.TotalPriceDate = equipmentParent.Part.Prices[1].Date;

            Assert.IsTrue(Math.Abs(equipmentParent.TotalPrice - equipmentParent.Part.Prices[1].Cost.Sum) < 0.0001);

            var equipmentChild1 = wrappersFactory.GetWrapper<ProductWrapper>(_fixture.Create<Product>());
            equipmentChild1.Part.Prices[1].Date = equipmentParent.Part.Prices[1].Date;
            equipmentParent.DependentProducts.Add(equipmentChild1);

            var equipmentChild2 = wrappersFactory.GetWrapper<ProductWrapper>(_fixture.Create<Product>());
            equipmentChild2.Part.Prices[1].Date = equipmentParent.Part.Prices[1].Date;
            equipmentParent.DependentProducts.Add(equipmentChild2);


            var totalSum =  equipmentParent.Part.Prices[1].Cost.Sum + 
                            equipmentChild1.Part.Prices[1].Cost.Sum + 
                            equipmentChild2.Part.Prices[1].Cost.Sum;
            Assert.IsTrue(Math.Abs(equipmentParent.TotalPrice - totalSum) < 0.0001);

            var productChild3 = wrappersFactory.GetWrapper<ProductWrapper>(_fixture.Create<Product>());
            productChild3.Part.Prices[1].Date = equipmentParent.Part.Prices[1].Date;
            equipmentParent.DependentProducts.Add(productChild3);

            totalSum += productChild3.Part.Prices[1].Cost.Sum;
            Assert.IsTrue(Math.Abs(equipmentParent.TotalPrice - totalSum) < 0.0001);
        }

        [TestMethod]
        public void ProductItemsSameParametersTest()
        {
            var wrappersFactory = new Factory.TestWrappersFactory();

            PartWrapper partItemWrapper1 = wrappersFactory.GetWrapper<PartWrapper> (_partItem1);
            PartWrapper partItemWrapper2 = wrappersFactory.GetWrapper<PartWrapper> (_partItem2);

            Assert.IsFalse(partItemWrapper1.HasSameParameters(partItemWrapper2));

            var pl1 = new List<ParameterWrapper>(partItemWrapper1.Parameters);
            var pl2 = new List<ParameterWrapper>(partItemWrapper2.Parameters);
            pl2.ForEach(partItemWrapper1.Parameters.Add);
            pl1.ForEach(partItemWrapper2.Parameters.Add);

            Assert.IsTrue(partItemWrapper1.HasSameParameters(partItemWrapper2));
        }
    }
}
