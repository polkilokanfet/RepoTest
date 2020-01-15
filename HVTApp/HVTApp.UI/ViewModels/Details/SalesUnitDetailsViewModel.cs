using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class SalesUnitDetailsViewModel
    {
        protected override void InitSpecialCommands()
        {
            SelectProductCommand = new DelegateCommand(SelectProductCommand_Execute);
        }

        private void SelectProductCommand_Execute()
        {
            var product = Container.Resolve<IGetProductService>().GetProduct(Item.Model.Product);
            if (product != null)
            {
                product = UnitOfWork.Repository<Product>().GetById(product.Id);
                Item.Product = new ProductWrapper(product);
            }
        }
    }
}