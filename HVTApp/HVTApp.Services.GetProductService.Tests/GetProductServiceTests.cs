using System;
using AutoFixture.AutoEF;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace HVTApp.Services.GetProductService.Tests
{
    [TestClass]
    public class GetProductServiceTests
    {
        [TestInitialize]
        public void InitializeMethod()
        {
            Fixture fixture = new Fixture();
            var nnn = fixture.Customize(new EntityCustomization(new DbContextEntityTypesProvider(typeof(HVTAppContext))))
                .Create<Company>();
            //var parameterGroup1 = fixture.Create<ParameterGroup>();
            //var product = fixture.Create<Product>();
        }
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
