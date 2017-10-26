using HVTApp.DataAccess.Lookup;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Infrastructure;
using HVTApp.Modules.Infrastructure.Events;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class ParametersGroupsViewModel : BaseListViewModel<ParametersGroupLookup, ParameterGroup, ParametersGroupDetailsViewModel, AfterSaveParameterGroupEvent>
    {
        public ParametersGroupsViewModel(IUnityContainer container, IParametersGroupLookupDataDataService lookupDataDataService) : base(container, lookupDataDataService)
        {
        }
    }
}
