using HVTApp.DataAccess.Lookup;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.Modules.Infrastructure;
using HVTApp.UI.BaseView;
using HVTApp.UI.Events;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public class ParametersGroupsViewModel : BaseListViewModel<ParametersGroupLookup, ParameterGroup, ParametersGroupDetailsViewModel, AfterSaveParameterGroupEvent>
    {
        public ParametersGroupsViewModel(IUnityContainer container, IParametersGroupLookupDataDataService lookupDataDataService) : base(container, lookupDataDataService)
        {
        }
    }
}
