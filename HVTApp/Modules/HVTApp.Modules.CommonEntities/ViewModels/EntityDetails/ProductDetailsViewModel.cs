using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
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
