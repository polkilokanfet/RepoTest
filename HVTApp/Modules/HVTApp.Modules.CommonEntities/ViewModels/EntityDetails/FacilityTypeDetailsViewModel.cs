using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using HVTApp.UI.BaseView;
using HVTApp.UI.Events;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public class FacilityTypeDetailsViewModel : BaseDetailsViewModel<FacilityTypeWrapper, FacilityType, AfterSaveFacilityTypeEvent>
    {
        public FacilityTypeDetailsViewModel(IUnityContainer container) : base(container)
        {
        }
    }
}