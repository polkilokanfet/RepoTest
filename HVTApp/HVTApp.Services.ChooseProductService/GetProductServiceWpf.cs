using System.Collections.Generic;
using System.Threading.Tasks;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class GetProductServiceWpf : IGetProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        private IList<Parameter> _parameters;
        private IList<Product> _products;
        private IList<ProductRelation> _productRelations;

        public GetProductServiceWpf(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task LoadAsync()
        {
            _parameters = await _unitOfWork.GetRepository<Parameter>().GetAllAsync();
            _products = await _unitOfWork.GetRepository<Product>().GetAllAsync();
            _productRelations = await _unitOfWork.GetRepository<ProductRelation>().GetAllAsync();
        }

        public async Task<Product> GetProductAsync(Product templateProduct = null)
        {
            if (_parameters == null)
                await LoadAsync();

            ProductSelector.Products = _products;
            ProductSelector.ProductRelations = _productRelations;
            ProductSelector.Parameters = _parameters;


            var productSelector = new ProductSelector(_parameters, templateProduct);
            var window = new SelectProductWindow {DataContext = productSelector};
            window.ShowDialog();

            if (!window.DialogResult.HasValue || !window.DialogResult.Value) return templateProduct;
            return null;
        }
    }
}