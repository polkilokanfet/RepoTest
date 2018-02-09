using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class GetProductServiceWpf : IGetProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        private IList<Parameter> _parameters;
        private IList<Product> _products;
        private IList<ProductRelation> _productRelations;
        private IList<ProductBlock> _productBlocks;

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
        }

        public async Task<Product> GetProductAsync(Product templateProduct = null)
        {
            if (_parameters == null) await LoadAsync();

            ProductSelector.Products = _products;
            ProductSelector.ProductRelations = _productRelations;
            ProductSelector.Parameters = _parameters;

            ProductBlockSelector.ProductBlocks = _productBlocks;


            var productSelector = new ProductSelector(_parameters, templateProduct);
            var window = new SelectProductWindow
            {
                DataContext = productSelector,
                Owner = Application.Current.MainWindow
            };
            window.ShowDialog();

            if (!window.DialogResult.HasValue || !window.DialogResult.Value) return templateProduct;

            var result = productSelector.SelectedProduct;
            if (!_products.Contains(result))
            {
                _unitOfWork.GetRepository<Product>().Add(result);
                await _unitOfWork.SaveChangesAsync();
            }

            return result;
        }
    }
}