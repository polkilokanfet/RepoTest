using System.Collections.Generic;
using System.Linq;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class GetProductServiceWpf : IGetProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IList<Parameter> _parameters;
        private readonly IList<Part> _parts;
        private readonly IList<Product> _products;
        private readonly IList<ProductsRelation> _requiredDependentProductsParameteres;

        public GetProductServiceWpf(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _parameters = _unitOfWork.Parameters.GetAll().ToList();
            _parts = _unitOfWork.Parts.GetAll().ToList();
            _products = _unitOfWork.Products.GetAll().ToList();
            //_requiredDependentProductsParameteres = _unitOfWork.RequiredDependentProductsParameters.GetAll().ToList();
        }

        public Product GetProduct(Product templateProduct = null)
        {
            ProductSelector productSelector = new ProductSelector(new List<ParameterGroup>(), _parts, _products, _requiredDependentProductsParameteres, preSelectedProduct: templateProduct);
            SelectProductWindow window = new SelectProductWindow {DataContext = productSelector};
            window.ShowDialog();

            if (!window.DialogResult.HasValue || !window.DialogResult.Value) return templateProduct;
            return productSelector.SelectedProduct;
        }
    }
}