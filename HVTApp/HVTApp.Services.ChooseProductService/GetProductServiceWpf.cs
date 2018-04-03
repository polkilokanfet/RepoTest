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
        private bool _loaded = false;

        private List<Product> _products;
        private List<ProductBlock> _productBlocks;
        private List<Parameter> _parameters;
        private List<ProductRelation> _productRelations;

        public GetProductServiceWpf(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task LoadAsync()
        {
            _parameters = await _unitOfWork.GetRepository<Parameter>().GetAllAsync();
            _products = await _unitOfWork.GetRepository<Product>().GetAllAsync();
            _productRelations = await _unitOfWork.GetRepository<ProductRelation>().GetAllAsync();
            _productBlocks = await _unitOfWork.GetRepository<ProductBlock>().GetAllAsync();

            _loaded = true;
        }

        public async Task<Product> GetProductAsync(Product originProduct = null)
        {
            if (!_loaded)
                await LoadAsync();

            var selectedProduct = originProduct == null
                ? null
                : await _unitOfWork.GetRepository<Product>().GetByIdAsync(originProduct.Id);

            var productSelector = new ProductSelector(selectedProduct: selectedProduct);
            var window = new SelectProductWindow
            {
                DataContext = productSelector,
                Owner = Application.Current.MainWindow
            };
            window.ShowDialog();

            if (!window.DialogResult.HasValue || !window.DialogResult.Value) return originProduct;

            var result = productSelector.SelectedProduct;
            if (!_products.Contains(result))
            {
                _unitOfWork.GetRepository<Product>().Add(result);
                await GenerateDescribeProductBlockTasks(result);
                await _unitOfWork.SaveChangesAsync();
            }

            return result;
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
}