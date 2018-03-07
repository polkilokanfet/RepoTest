using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public static class CommonData
    {
        public static List<Product> Products { get; set; }
        public static List<ProductBlock> ProductBlocks { get; set; }
        public static List<Parameter> Parameters { get; set; }
        public static List<ProductRelation> ProductRelations { get; set; }
    }

    public class GetProductServiceWpf : IGetProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductServiceWpf(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task LoadAsync()
        {
            CommonData.Parameters = await _unitOfWork.GetRepository<Parameter>().GetAllAsync();
            CommonData.Products = await _unitOfWork.GetRepository<Product>().GetAllAsync();
            CommonData.ProductRelations = await _unitOfWork.GetRepository<ProductRelation>().GetAllAsync();
            CommonData.ProductBlocks = await _unitOfWork.GetRepository<ProductBlock>().GetAllAsync();
        }

        public async Task<Product> GetProductAsync(Product templateProduct = null)
        {
            if (CommonData.Parameters == null)
            {
                await LoadAsync();
            }

            var selectedProduct = templateProduct == null
                ? null
                : await _unitOfWork.GetRepository<Product>().GetByIdAsync(templateProduct.Id);

            var productSelector = new ProductSelector(selectedProduct: selectedProduct);
            var window = new SelectProductWindow
            {
                DataContext = productSelector,
                Owner = Application.Current.MainWindow
            };
            window.ShowDialog();

            if (!window.DialogResult.HasValue || !window.DialogResult.Value) return templateProduct;

            var result = productSelector.SelectedProduct;
            if (!CommonData.Products.Contains(result))
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