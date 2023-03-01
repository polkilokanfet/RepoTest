using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandRejectedByConstructor : DoStepCommandBase
    {
        protected override ScriptStep2 Step => ScriptStep2.RejectedByConstructor;
        protected override string ConfirmationMessage => "Вы уверены, что хотите отклонить проработку задачи?";

        public DoStepCommandRejectedByConstructor(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void DoStepAction()
        {
            ViewModel.RejectChanges();
            base.DoStepAction();
        }
    }
}