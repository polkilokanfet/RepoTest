using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using HVTApp.UI.Events;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public class ParameterDetailsViewModel : BaseDetailsViewModel<ParameterWrapper, Parameter, AfterSaveParameterEvent>
    {
        public ParameterDetailsViewModel(IUnityContainer container) : base(container)
        {
        }
    }
}