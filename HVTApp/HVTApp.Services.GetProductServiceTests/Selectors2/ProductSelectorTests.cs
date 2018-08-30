using System.Linq;
using System.Threading.Tasks;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.Services.GetProductService;
using HVTApp.TestDataGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Services.GetProductServiceTests.Selectors2
{
    [TestClass()]
    public class ProductSelectorTests
    {
        [TestMethod]
        public void ProductBlockSelectorTest()
        {
            var testData = new TestData();
            var parameters = testData.GetAll<Parameter>().ToList();

            var productBlockSelector = new ProductBlockSelector(parameters, testData.GetAll<ProductBlock>().ToList());

            var originParameters = parameters.Where(x => !x.ParameterRelations.Any());
            var originParameterSelector = productBlockSelector.ParameterSelectors.
                Single(x => x.ParametersFlaged.Select(p => p.Parameter).AllMembersAreSame(originParameters));

            foreach (var selectedParameterFlaged in originParameterSelector.ParametersFlaged)
            {
                originParameterSelector.SelectedParameterFlaged = selectedParameterFlaged;
            }
        }

        [TestMethod]
        public void ProductSelectorTest()
        {
            var testData = new TestData();
            var products = testData.GetAll<Product>().ToList();
            var productBlocks = testData.GetAll<ProductBlock>().ToList();
            var parameters = testData.GetAll<Parameter>().ToList();
            var productRelations = testData.GetAll<ProductRelation>().ToList();
            Bank bank = new Bank(products, productBlocks, parameters, productRelations);

            var productSelector = new ProductSelector(bank);
        }

        [TestMethod]
        public async Task GetProductServiceTest()
        {
            var getProductService = new GetProductServiceWpf(new UnitOfWorkTest(new TestData()));
            await getProductService.GetProductAsync();
        }
    }
}