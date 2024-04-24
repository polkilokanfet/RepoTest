using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandAcceptByInspector : DoStepCommandAcceptByDesignDepartmentBase<TaskViewModelInspector>
    {
        protected override User Inspector => this.ViewModel.Model.UserConstructorInspector;

        public DoStepCommandAcceptByInspector(TaskViewModelInspector viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }
    }
}