using System.Linq;
using HVTApp.Infrastructure;
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
            var parameters = testData.GetAll<Parameter>().ToList();
            var productSelector = new ProductSelector(parameters);

            var originParameters = parameters.Where(x => !x.RequiredPreviousParameters.Any());
            var originParameterSelector = productSelector.ParameterSelectors.Single(x => x.Parameters.AllMembersAreSame(originParameters));

            foreach (var originParameter in originParameters)
            {
                originParameterSelector.SelectedParameter = originParameter;
            }
        }
    }
}