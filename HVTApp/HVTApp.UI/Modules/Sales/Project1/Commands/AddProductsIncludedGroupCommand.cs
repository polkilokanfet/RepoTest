using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.Services;
using HVTApp.UI.Modules.Sales.Project1.ViewModels;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;

namespace HVTApp.UI.Modules.Sales.Project1.Commands
{
    public class AddProductsIncludedGroupCommand : ProjectUnitEditBaseCommand
    {
        private readonly IGetProductService _productService;
        private readonly IDialogService _dialogService;

        public AddProductsIncludedGroupCommand(IProjectUnit projectUnit, IGetProductService productService, IDialogService dialogService) : base(projectUnit)
        {
            _productService = productService;
            _dialogService = dialogService;
        }

        public override void Execute(object parameter)
        {
            var viewModel = new ProductIncludedViewModel(_productService);
            var dr = _dialogService.ShowDialog(viewModel);
            if (dr == true)
            {
                ProjectUnit.AddProductIncluded(viewModel.Item.Model, viewModel.IsForEach);
            }
        }
    }
}