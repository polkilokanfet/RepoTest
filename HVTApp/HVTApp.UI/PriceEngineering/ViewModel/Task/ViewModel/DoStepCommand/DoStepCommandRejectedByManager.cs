using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandRejectedByManager : DoStepCommandBase
    {
        protected override ScriptStep2 Step => ScriptStep2.RejectByManager;
        protected override string ConfirmationMessage => "Вы уверены, что хотите отклонить проработку задачи?";

        public DoStepCommandRejectedByManager(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }
    }
}