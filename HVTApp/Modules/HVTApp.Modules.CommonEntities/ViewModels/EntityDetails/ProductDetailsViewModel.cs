using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;
using HVTApp.UI.BaseView;
using HVTApp.UI.Events;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public class ProductDetailsViewModel : BaseDetailsViewModel<ProductWrapper, Product, AfterSaveProductEvent>
    {
        public ProductDetailsViewModel(IUnityContainer container) : base(container)
        {
        }
    }
}
