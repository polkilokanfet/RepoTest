using System.Threading.Tasks;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Services.GetProductService;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.NewProductService
{
    public class NewProductServiceWpf : INewProductService
    {
        private readonly IUnityContainer _container;

        public NewProductServiceWpf(IUnityContainer container)
        {
            _container = container;
            container.Resolve<IDialogService>().Register<ProductNewViewModel, ProductNewWindow>();
        }

        public Product GetNewProduct()
        {
            var productNewViewModel = _container.Resolve<ProductNewViewModel>();
            productNewViewModel.Load(new CreateNewProductTask());
            var dr = _container.Resolve<IDialogService>().ShowDialog(productNewViewModel);
            if (dr.HasValue && dr == true) return productNewViewModel.Item.Product.Model;
            return null;
        }
    }
}
