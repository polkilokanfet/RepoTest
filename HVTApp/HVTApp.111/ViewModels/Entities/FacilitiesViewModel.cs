﻿using HVTApp.DataAccess.Lookup;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Infrastructure;
using HVTApp.Modules.Infrastructure.Events;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class FacilitiesViewModel : BaseListViewModel<FacilityLookup, Facility, FacilityDetailsViewModel, AfterSaveFacilityEvent>
    {
        public FacilitiesViewModel(IUnityContainer container, IFacilityLookupDataDataService lookupDataDataService) : base(container, lookupDataDataService)
        {
        }
    }
}
