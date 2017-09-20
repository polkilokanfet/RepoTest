using System.Collections.Generic;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.Services.GetProductService
{
    public class GetProductServiceWpf : IGetProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IList<ParameterGroup> _groups;
        private readonly IList<Part> _parts;
        private readonly IList<Product> _products;
        private readonly IList<RequiredDependentProductsParameters> _requiredDependentProductsParameteres;

        public GetProductServiceWpf(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _groups = _unitOfWork.ParametersGroups.GetAll().Select(x => x.Model).ToList();
            _parts = _unitOfWork.Parts.GetAll().Select(x => x.Model).ToList();
            _products = _unitOfWork.Products.GetAll().Select(x => x.Model).ToList();
            _requiredDependentProductsParameteres = _unitOfWork.RequiredDependentProductsParameters.GetAll().Select(x => x.Model).ToList();
        }

        public ProductWrapper GetProduct(ProductWrapper templateProduct = null)
        {
            ProductSelector productSelector = new ProductSelector(_groups, _parts, _products, _requiredDependentProductsParameteres, preSelectedProduct: templateProduct?.Model);
            SelectProductWindow window = new SelectProductWindow {DataContext = productSelector};
            window.ShowDialog();

            if (!window.DialogResult.HasValue || !window.DialogResult.Value) return templateProduct;
            return _unitOfWork.Products.GetWrapper(productSelector.SelectedProduct);
        }
    }
}