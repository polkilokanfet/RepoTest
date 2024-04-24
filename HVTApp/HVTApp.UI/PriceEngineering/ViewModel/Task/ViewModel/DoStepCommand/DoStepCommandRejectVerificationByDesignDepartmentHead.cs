using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandRejectVerificationByDesignDepartmentHead : DoStepCommandRejectVerificationBase<TaskViewModelDesignDepartmentHead>
    {
        public DoStepCommandRejectVerificationByDesignDepartmentHead(TaskViewModelDesignDepartmentHead viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override string GetStatusComment()
        {
            return $"Проверяющий: {this.ViewModel.Model.DesignDepartment.Head}";
        }
    }
}