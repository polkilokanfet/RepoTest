using System;
using HVTApp.Model.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void ProductTotalPriceTest()
        {
            SumOnDate price1 = new SumOnDate { SumAndVat = new SumAndVat { Sum = 10, Vat = 50}, Date = DateTime.Today.AddDays(-7) };
            SumOnDate price2 = new SumOnDate { SumAndVat = new SumAndVat { Sum = 5, Vat = 50}, Date = DateTime.Today };

            var productParent = ProductWrapper.GetWrapper();
            productParent.Prices.Add(SumOnDateWrapper.GetWrapper(price1));
            productParent.Prices.Add(SumOnDateWrapper.GetWrapper(price2));
            productParent.TotalPrice.Date = DateTime.Today;

            Assert.IsTrue(Math.Abs(productParent.TotalPrice.SumAndVat.Sum - price2.SumAndVat.Sum) < 0.0001);

            SumOnDate price3 = new SumOnDate { SumAndVat = new SumAndVat { Sum = 30, Vat = 50}, Date = DateTime.Today };
            SumOnDate price4 = new SumOnDate { SumAndVat = new SumAndVat { Sum = 40, Vat = 50 }, Date = DateTime.Today };

            var productChild1 = ProductWrapper.GetWrapper();
            productChild1.Prices.Add(SumOnDateWrapper.GetWrapper(price3));

            var productChild2 = ProductWrapper.GetWrapper();
            productChild2.Prices.Add(SumOnDateWrapper.GetWrapper(price4));

            productParent.ChildProducts.Add(productChild1);
            productParent.ChildProducts.Add(productChild2);

            var totalSum = price2.SumAndVat.Sum + price3.SumAndVat.Sum + price4.SumAndVat.Sum;
            Assert.IsTrue(Math.Abs(productParent.TotalPrice.SumAndVat.Sum - totalSum) < 0.0001);

            SumOnDate price5 = new SumOnDate { SumAndVat = new SumAndVat { Sum = 50, Vat = 50 }, Date = DateTime.Today };
            var productChild3 = ProductWrapper.GetWrapper();
            productChild3.Prices.Add(SumOnDateWrapper.GetWrapper(price5));
            productChild1.ChildProducts.Add(productChild3);

            totalSum += price5.SumAndVat.Sum;
            Assert.IsTrue(Math.Abs(productParent.TotalPrice.SumAndVat.Sum - totalSum) < 0.0001);

        }
    }
}
