using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class GetProductServiceWpf : IGetProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private ProductsBlocksParameters _productsBlocksParameters;

        public GetProductServiceWpf(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task LoadAsync()
        {
            var parameters = await _unitOfWork.GetRepository<Parameter>().GetAllAsync();
            var products = await _unitOfWork.GetRepository<Product>().GetAllAsync();
            var productRelations = await _unitOfWork.GetRepository<ProductRelation>().GetAllAsync();
            var productBlocks = await _unitOfWork.GetRepository<ProductBlock>().GetAllAsync();

            _productsBlocksParameters = new ProductsBlocksParameters(products, productBlocks, parameters, productRelations);
        }

        public async Task<Product> GetProductAsync(Product originProduct = null)
        {
            await LoadAsync();

            var selectedProduct = originProduct == null
                ? null
                : await _unitOfWork.GetRepository<Product>().GetByIdAsync(originProduct.Id);

            var productSelector = new ProductSelector(_productsBlocksParameters, null, selectedProduct);
            var window = new SelectProductWindow
            {
                DataContext = productSelector,
                Owner = Application.Current.MainWindow
            };
            window.ShowDialog();

            //выходим, если пользователь отменил выбор продукта.
            if (!window.DialogResult.HasValue || !window.DialogResult.Value) return originProduct;

            var result = productSelector.SelectedProduct;

            await AddIfMissed(result);
            //если выбранного продукта нет в базе
            if (!_productsBlocksParameters.Products.Contains(result))
            {
                _unitOfWork.GetRepository<Product>().Add(result);
                await GenerateDescribeProductBlockTasks(result);
                await _unitOfWork.SaveChangesAsync();
            }

            return result;
        }

        private async Task AddIfMissed(Product product)
        {
            foreach (var dependentProduct in product.DependentProducts)
            {
                await AddIfMissed(dependentProduct);
            }

            if (await _unitOfWork.GetRepository<Product>().GetByIdAsync(product.Id) == null)
            {
                _unitOfWork.GetRepository<Product>().Add(product);
            }
        }

        private async Task GenerateDescribeProductBlockTasks(Product product)
        {
            var tasks = await _unitOfWork.GetRepository<DescribeProductBlockTask>().GetAllAsync();
            var blocks = GetAllProductBlocks(product).Where(x => string.IsNullOrEmpty(x.StructureCostNumber));
            foreach (var productBlock in blocks)
            {
                if (tasks.Any(x => x.ProductBlock.Id == productBlock.Id))
                    continue;
                var task = new DescribeProductBlockTask {Product = product, ProductBlock = productBlock};
                _unitOfWork.GetRepository<DescribeProductBlockTask>().Add(task);
            }
        }

        private IEnumerable<ProductBlock> GetAllProductBlocks(Product product)
        {
            yield return product.ProductBlock;

            foreach (var dependentProduct in product.DependentProducts)
            {
                var dp = GetAllProductBlocks(dependentProduct);
                foreach (var productBlock in dp)
                {
                    yield return productBlock;
                }
            }
        }
    }

    public class ProductsBlocksParameters
    {
        public ProductsBlocksParameters(List<Product> products, List<ProductBlock> productBlocks, List<Parameter> parameters, List<ProductRelation> productRelations)
        {
            Products = products;
            ProductBlocks = productBlocks;
            Parameters = parameters;
            ProductRelations = productRelations;
        }

        public List<Product> Products { get; }
        public List<ProductBlock> ProductBlocks { get; }
        public List<Parameter> Parameters { get; }
        public List<ProductRelation> ProductRelations { get; }
    }
}