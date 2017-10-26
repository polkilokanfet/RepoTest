using HVTApp.DataAccess.Lookup;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.UI.BaseView;
using HVTApp.UI.Events;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public class ParametersViewModel : BaseListViewModel<ParameterLookup, Parameter, ParameterDetailsViewModel, AfterSaveParameterEvent>
    {
        public ParametersViewModel(IUnityContainer container, IParameterLookupDataDataService lookupDataDataService) : base(container, lookupDataDataService)
        {
        }
    }
}