using HVTApp.Model.POCOs;
using HVTApp.Services.GetProductService;
using HVTApp.TestDataGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Services.GetProductServiceTests.Selectors2
{
    [TestClass()]
    public class ProductSelectorTests
    {
        [TestMethod()]
        public void ProductSelectorTest()
        {
            TestData testData = new TestData();
            var parameters = testData.GetAll<Parameter>();
            ProductSelector productSelector = new ProductSelector(parameters);
        }
    }
}