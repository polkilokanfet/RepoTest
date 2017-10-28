using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class GetProductServiceWpf : IGetProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        private IList<Parameter> _parameters;
        private IList<Part> _parts;
        private IList<Product> _products;
        private IList<ProductsRelation> _requiredDependentProductsParameteres;

        public GetProductServiceWpf(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task LoadAsync()
        {
            _parameters = await _unitOfWork.Parameters.GetAllAsync();
            _parts = await _unitOfWork.Parts.GetAllAsync();
            _products = await _unitOfWork.Products.GetAllAsync();
            //_requiredDependentProductsParameteres = _unitOfWork.RequiredDependentProductsParameters.GetAllAsync().ToList();
        }

        public async Task<Product> GetProduct(Product templateProduct = null)
        {
            if (_parameters == null)
                await LoadAsync();
            ProductSelector productSelector = new ProductSelector(new List<ParameterGroup>(), _parts, _products, _requiredDependentProductsParameteres, preSelectedProduct: templateProduct);
            SelectProductWindow window = new SelectProductWindow {DataContext = productSelector};
            window.ShowDialog();

            if (!window.DialogResult.HasValue || !window.DialogResult.Value) return templateProduct;
            return productSelector.SelectedProduct;
        }
    }
}