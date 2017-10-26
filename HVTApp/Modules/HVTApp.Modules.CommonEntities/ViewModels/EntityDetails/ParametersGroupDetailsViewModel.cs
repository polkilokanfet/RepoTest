using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;
using HVTApp.UI.BaseView;
using HVTApp.UI.Events;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public class ParametersGroupDetailsViewModel : BaseDetailsViewModel<ParameterGroupWrapper, ParameterGroup, AfterSaveParameterGroupEvent>
    {
        public ParametersGroupDetailsViewModel(IUnityContainer container) : base(container)
        {
        }
    }
}