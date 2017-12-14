using System.Collections.Generic;
using System.Linq;
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
        private IList<ProductsRelation> _productsRelations;

        public GetProductServiceWpf(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task LoadAsync()
        {
            _parameters = await _unitOfWork.ParameterRepository.GetAllAsync();
            _products = await _unitOfWork.ProductRepository.GetAllAsync();
            _productsRelations = await _unitOfWork.ProductsRelationRepository.GetAllAsync();
        }

        public async Task<Product> GetProductAsync(Product templateProduct = null)
        {
            if (_parameters == null)
                await LoadAsync();
            ProductSelector productSelector = new ProductSelector(new List<ParameterGroup>(), _parts, _products, _productsRelations, preSelectedProduct: templateProduct);
            SelectProductWindow window = new SelectProductWindow {DataContext = productSelector};
            window.ShowDialog();

            if (!window.DialogResult.HasValue || !window.DialogResult.Value) return templateProduct;
            return productSelector.SelectedProduct;
        }
    }
}