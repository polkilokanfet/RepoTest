using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class ParameterDetailsViewModel : BaseDetailsViewModel<ParameterWrapper>
    {
        public ParameterDetailsViewModel(IUnityContainer container) : base(container)
        {
        }
    }
}