using System.Linq;
using HVTApp.DataAccess;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.DataAccess.Lookup;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;
using HVTApp.Modules.Infrastructure.Events;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class FacilityTypesViewModel : BaseListViewModel<FacilityTypeLookup, FacilityType, FacilityTypeDetailsViewModel, AfterSaveFacilityTypeEvent>
    {
        public FacilityTypesViewModel(IUnityContainer container, IFacilityTypeLookupDataDataService lookupDataDataService) : base(container, lookupDataDataService)
        {
        }
    }
}
