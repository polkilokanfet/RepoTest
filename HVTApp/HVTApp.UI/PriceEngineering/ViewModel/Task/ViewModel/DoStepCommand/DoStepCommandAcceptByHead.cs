using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandAcceptByHead: DoStepCommandAcceptByDesignDepartmentBase<TaskViewModelDesignDepartmentHead>
    {
        protected override User Inspector => this.ViewModel.Model.DesignDepartment.Head;

        public DoStepCommandAcceptByHead(TaskViewModelDesignDepartmentHead viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }
    }
}