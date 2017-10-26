using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;
using HVTApp.Modules.Infrastructure.Events;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class ParameterDetailsViewModel : BaseDetailsViewModel<ParameterWrapper, Parameter, AfterSaveParameterEvent>
    {
        public ParameterDetailsViewModel(IUnityContainer container) : base(container)
        {
        }
    }
}