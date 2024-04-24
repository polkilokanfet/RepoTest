using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandRejectVerificationByInspector : DoStepCommandRejectVerificationBase<TaskViewModelInspector>
    {
        public DoStepCommandRejectVerificationByInspector(TaskViewModelInspector viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override string GetStatusComment()
        {
            return $"Проверяющий: {this.ViewModel.Model.DesignDepartment.Head}";
        }
    }
}