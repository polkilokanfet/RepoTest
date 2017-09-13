using System;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void ProductPriceTest()
        {
            var factory = new Factory.TestWrappersFactory();

            ProductWrapper productMain = factory.GetWrapper<ProductWrapper>(new Product {Part = new Part()});

            //ловим ошибку при пустом списке себистоимостей в главном продукте
            try
            {
                productMain.GetPrice();
            }
            catch (ArgumentException e)
            {
                Assert.AreSame(e.Message, "Нет себистоимости для этой даты (или для более ранней даты)");
            }

            //добавляем стоимость главного продукта
            productMain.Part.Prices.Add(factory.GetWrapper<CostOnDateWrapper>(new CostOnDate {Date = DateTime.Today.AddDays(-1), Cost = 10}));
            Assert.AreEqual(productMain.GetPrice(), 10);

            //добавляем стоимость дочернего продукта
            ProductWrapper productChild1 = factory.GetWrapper<ProductWrapper>(new Product { Part = new Part() });
            productChild1.Part.Prices.Add(factory.GetWrapper<CostOnDateWrapper>(new CostOnDate { Date = DateTime.Today.AddDays(-1), Cost = 10 }));
            productMain.DependentProducts.Add(productChild1);
            Assert.AreEqual(productMain.GetPrice(), 20);

            //добавляем стоимость дочернего продукта к дочернему продукту
            ProductWrapper productChild2 = factory.GetWrapper<ProductWrapper>(new Product { Part = new Part() });
            productChild2.Part.Prices.Add(factory.GetWrapper<CostOnDateWrapper>(new CostOnDate { Date = DateTime.Today.AddDays(-1), Cost = 10 }));
            productChild1.DependentProducts.Add(productChild2);
            Assert.AreEqual(productMain.GetPrice(), 30);
        }

    }
}