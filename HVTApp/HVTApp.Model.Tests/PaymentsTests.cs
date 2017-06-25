using System;
using HVTApp.Model.POCOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class PaymentsTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var fixture = new Fixture();
            var productComplex = fixture.Build<ProductComplexUnit>()
                                        .Without(x => x.Order)
                                        .Create();
        }
    }
}
