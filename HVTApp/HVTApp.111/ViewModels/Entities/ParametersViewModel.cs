using HVTApp.DataAccess.Lookup;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Infrastructure;
using HVTApp.Modules.Infrastructure.Events;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class ParametersViewModel : BaseListViewModel<ParameterLookup, Parameter, ParameterDetailsViewModel, AfterSaveParameterEvent>
    {
        public ParametersViewModel(IUnityContainer container, IParameterLookupDataDataService lookupDataDataService) : base(container, lookupDataDataService)
        {
        }
    }
}