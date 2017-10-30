using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Events;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public class ParametersViewModel : BaseLookupListViewModel<ParameterLookup, Parameter, ParameterDetailsViewModel, AfterSaveParameterEvent>
    {
        public ParametersViewModel(IUnityContainer container, IParameterLookupDataService lookupDataService) : base(container, lookupDataService)
        {
        }
    }
}