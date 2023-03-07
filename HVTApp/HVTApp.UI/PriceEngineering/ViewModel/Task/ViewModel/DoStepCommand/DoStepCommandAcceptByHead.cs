using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandAcceptByHead: DoStepCommandBase
    {
        protected override ScriptStep Step => ScriptStep.VerificationAcceptByHead;
        protected override string ConfirmationMessage => "Вы уверены, что хотите принять результаты проработки?";

        public DoStepCommandAcceptByHead(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void DoStepAction()
        {
            ViewModel.Statuses.Add(ScriptStep.VerificationAcceptByHead);
            ViewModel.Statuses.Add(ScriptStep.FinishByConstructor);
            ViewModel.SaveCommand.Execute();
            this.EventAggregator.GetEvent<PriceEngineeringTaskVerificationAcceptedByHeadEvent>().Publish(ViewModel.Model);
            this.EventAggregator.GetEvent<PriceEngineeringTaskFinishedEvent>().Publish(ViewModel.Model);
        }
    }
}