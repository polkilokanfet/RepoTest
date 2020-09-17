using HVTApp.Infrastructure;
using HVTApp.Model.Wrapper;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class SupervisionPriceViewModel : ProductIncludedDetailsViewModel
    {
        public SupervisionPriceViewModel(ProductIncludedWrapper wrapper, IUnitOfWork unitOfWork, IUnityContainer container) : base(container)
        {
            Load(wrapper, unitOfWork);
        }
    }
}