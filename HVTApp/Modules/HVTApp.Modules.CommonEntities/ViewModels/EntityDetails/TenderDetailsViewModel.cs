using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using HVTApp.UI.BaseView;
using HVTApp.UI.Events;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class TenderDetailsViewModel : BaseDetailsViewModel<TenderWrapper, Tender, AfterSaveTenderEvent>
    {
        public TenderDetailsViewModel(IUnityContainer container) : base(container)
        {
        }
    }
}