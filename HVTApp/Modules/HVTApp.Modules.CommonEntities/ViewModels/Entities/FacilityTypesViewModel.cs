using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Events;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public class FacilityTypesViewModel : BaseListViewModel<FacilityTypeLookup, FacilityType, FacilityTypeDetailsViewModel, AfterSaveFacilityTypeEvent>
    {
        public FacilityTypesViewModel(IUnityContainer container, IFacilityTypeLookupDataService lookupDataService) : base(container, lookupDataService)
        {
        }
    }
}
