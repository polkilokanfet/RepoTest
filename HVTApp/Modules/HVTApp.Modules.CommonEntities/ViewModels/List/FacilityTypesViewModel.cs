using HVTApp.Model.POCOs;
using HVTApp.UI.Events;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public class FacilityTypesViewModel : BaseWrapperListViewModel<FacilityTypeWrapper, FacilityType, FacilityTypeDetailsViewModel, AfterSaveFacilityTypeEvent>
    {
        public FacilityTypesViewModel(IUnityContainer container, FacilityTypeWrapperDataService wrapperDataService) : base(container, wrapperDataService)
        {
        }
    }
}
