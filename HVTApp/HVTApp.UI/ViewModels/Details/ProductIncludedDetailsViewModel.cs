using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public partial class ProductIncludedDetailsViewModel
    {

        protected override void InitSpecialCommands()
        {
            SelectProductCommand = new DelegateLogCommand(
                () =>
                {
                    var product = Container.Resolve<IGetProductService>().GetProduct(Item.Model.Product);
                    if (product != null)
                    {
                        product = UnitOfWork.Repository<Product>().GetById(product.Id);
                        Item.Product = new ProductWrapper(product);
                    }
                });
        }
    }
}