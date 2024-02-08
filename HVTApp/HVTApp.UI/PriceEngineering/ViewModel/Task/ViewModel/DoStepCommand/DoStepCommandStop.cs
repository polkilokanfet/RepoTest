using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model.Events.NotificationArgs;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    /// <summary>
    /// ќстановить проработку задачи
    /// </summary>
    public class DoStepCommandStop : DoStepCommand<TaskViewModelManagerOld>
    {
        protected override ScriptStep Step => ScriptStep.Stop;

        protected override string ConfirmationMessage => "¬ы уверены, что хотите остановить проработку задачи?";

        public DoStepCommandStop(TaskViewModelManagerOld viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override IEnumerable<NotificationUnit> GetNotificationUnits()
        {
            if (ViewModel.Model.UserConstructor != null)
            {
                yield return new NotificationUnit
                {
                    ActionType = EventServiceActionType.PriceEngineeringTaskStop,
                    RecipientRole = Role.Constructor,
                    RecipientUser = ViewModel.Model.UserConstructor,
                    TargetEntityId = ViewModel.Model.Id
                };
            }
        }

        protected override void BeforeDoStepAction()
        {
            foreach (var salesUnit in ViewModel.SalesUnits)
            {
                salesUnit.SignalToStartProduction = null;
                salesUnit.SignalToStartProductionDone = null;
            }
        }
    }
}