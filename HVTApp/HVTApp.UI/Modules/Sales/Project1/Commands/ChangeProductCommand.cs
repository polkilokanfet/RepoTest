using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;

namespace HVTApp.UI.Modules.Sales.Project1.Commands
{
    public class ChangeProductCommand : ProjectUnitEditUnitOfWorkBaseCommand
    {
        private readonly IGetProductService _productService;

        public ChangeProductCommand(IProjectUnit projectUnit, IGetProductService productService, IUnitOfWork unitOfWork) : base(projectUnit, unitOfWork)
        {
            _productService = productService;
        }

        public override void Execute(object parameter)
        {
            var product = _productService.GetProduct(ProjectUnit.Product?.Model);
            if (product == null) return;
            if (product.Id == ProjectUnit.Product?.Model.Id) return;
            ProjectUnit.Product = new ProductEmptyWrapper(UnitOfWork.Repository<Product>().GetById(product.Id));
        }
    }
}