using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;

namespace HVTApp.UI.Modules.Sales.Project1.Commands
{
    public class ChangeProductCommand : ProjectUnitEditBaseCommand
    {
        private readonly IGetProductService _productService;

        public ChangeProductCommand(IProjectUnit projectUnit, IGetProductService productService) : base(projectUnit)
        {
            _productService = productService;
        }

        public override void Execute(object parameter)
        {
            var product = _productService.GetProduct(ProjectUnit.Product?.Model);
            if (product == null) return;
            ProjectUnit.Product = new ProductEmptyWrapper(product);
        }
    }
}