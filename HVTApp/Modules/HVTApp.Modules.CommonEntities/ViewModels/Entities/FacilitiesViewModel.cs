using HVTApp.DataAccess.Lookup;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.UI.BaseView;
using HVTApp.UI.Events;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public class FacilitiesViewModel : BaseListViewModel<FacilityLookup, Facility, FacilityDetailsViewModel, AfterSaveFacilityEvent>
    {
        public FacilitiesViewModel(IUnityContainer container, IFacilityLookupDataDataService lookupDataDataService) : base(container, lookupDataDataService)
        {
        }
    }
}
