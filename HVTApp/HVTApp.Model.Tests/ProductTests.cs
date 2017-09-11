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
        [TestMethod]
        public void ProductPriceTest()
        {
            var factory = new Factory.TestWrappersFactory();

            ProductWrapper productMain = factory.GetWrapper<ProductWrapper>(new Product {Part = new Part()});

            //����� ������ ��� ������ ������ �������������� � ������� ��������
            try
            {
                productMain.GetPrice();
            }
            catch (ArgumentException e)
            {
                Assert.AreSame(e.Message, "��� ������������� ��� ���� ���� (��� ��� ����� ������ ����)");
            }

            //��������� ��������� �������� ��������
            productMain.Part.Prices.Add(factory.GetWrapper<CostOnDateWrapper>(new CostOnDate {Date = DateTime.Today.AddDays(-1), Cost = 10}));
            Assert.AreEqual(productMain.GetPrice(), 10);

            //��������� ��������� ��������� ��������
            ProductWrapper productChild1 = factory.GetWrapper<ProductWrapper>(new Product { Part = new Part() });
            productChild1.Part.Prices.Add(factory.GetWrapper<CostOnDateWrapper>(new CostOnDate { Date = DateTime.Today.AddDays(-1), Cost = 10 }));
            productMain.DependentProducts.Add(productChild1);
            Assert.AreEqual(productMain.GetPrice(), 20);

            //��������� ��������� ��������� �������� � ��������� ��������
            ProductWrapper productChild2 = factory.GetWrapper<ProductWrapper>(new Product { Part = new Part() });
            productChild2.Part.Prices.Add(factory.GetWrapper<CostOnDateWrapper>(new CostOnDate { Date = DateTime.Today.AddDays(-1), Cost = 10 }));
            productChild1.DependentProducts.Add(productChild2);
            Assert.AreEqual(productMain.GetPrice(), 30);
        }

    }
}