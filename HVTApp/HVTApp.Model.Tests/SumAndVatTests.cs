﻿using System;
using HVTApp.Model.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class SumAndVatTests
    {
        [TestMethod]
        public void SumAndVatTest()
        {
            var sumAndVat = SumAndVatWrapper.GetWrapper( new SumAndVat {Sum = 100, Vat = 50});
            Assert.AreEqual(sumAndVat.SumWithVat, 150);

            sumAndVat.Vat = 100;
            Assert.AreEqual(sumAndVat.SumWithVat, 200);

            sumAndVat.Sum = 50;
            Assert.AreEqual(sumAndVat.SumWithVat, 100);

            sumAndVat.SumWithVat = 50;
            Assert.AreEqual(sumAndVat.Sum, 25);
        }
    }
}