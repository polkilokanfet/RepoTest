using HVTApp.Model.POCOs;
using HVTApp.UI.BaseView;
using HVTApp.UI.Events;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public class ParametersGroupsViewModel : BaseListViewModel<ParameterGroupLookup, ParameterGroup, ParametersGroupDetailsViewModel, AfterSaveParameterGroupEvent>
    {
        public ParametersGroupsViewModel(IUnityContainer container, IParameterGroupLookupDataService lookupDataService) : base(container, lookupDataService)
        {
        }
    }
}
