using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class TenderDetailsViewModel : BaseDetailsViewModel<TenderWrapper, Tender>
    {
        public TenderDetailsViewModel(IUnityContainer container) : base(container)
        {
        }
    }
}