using HVTApp.Model.POCOs;
using HVTApp.Services.GetProductService;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class SalesUnitDetailsViewModel
    {
        public void Load(SalesUnitWrapper unit, IWrapperDataService wrapperDataService)
        {
            this.WrapperDataService = wrapperDataService;
            Item = unit;
        }

        protected override void InitSpecialCommands()
        {
            SelectProductCommand = new DelegateCommand(SelectProductCommand_Execute);
        }

        private async void SelectProductCommand_Execute()
        {
            var product = await Container.Resolve<IGetProductService>().GetProductAsync(Item.Model.Product);
            if (product != null)
            {
                product = await WrapperDataService.Repository<Product>().GetByIdAsync(product.Id);
                Item.Product = new ProductWrapper(product);
            }
        }
    }
}