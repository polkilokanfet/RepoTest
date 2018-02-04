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
            var testData = new TestData();
            var products = testData.GetAll<Product>();
            var parameters = testData.GetAll<Parameter>().ToList();
            var productRelations = testData.GetAll<ProductRelation>().ToList();

            var productSelector = new ProductSelector(products, parameters, productRelations);

            var originParameters = parameters.Where(x => !x.ParameterRelations.Any());
            var originParameterSelector = productSelector.ParameterSelectors.Single(x => x.Parameters.AllMembersAreSame(originParameters));

            foreach (var originParameter in originParameters)
            {
                originParameterSelector.SelectedParameter = originParameter;
            }
        }
    }
}