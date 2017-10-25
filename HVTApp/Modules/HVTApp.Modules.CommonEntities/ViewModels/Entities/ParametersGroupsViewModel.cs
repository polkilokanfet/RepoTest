using System.Linq;
using HVTApp.DataAccess;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.DataAccess.Lookup;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class ParametersGroupsViewModel : BaseListViewModel<ParametersGroupLookup, ParameterGroup, ParametersGroupDetailsViewModel>
    {
        public ParametersGroupsViewModel(IUnityContainer container, IParametersGroupLookupDataDataService lookupDataDataService) : base(container, lookupDataDataService)
        {
        }
    }
}
