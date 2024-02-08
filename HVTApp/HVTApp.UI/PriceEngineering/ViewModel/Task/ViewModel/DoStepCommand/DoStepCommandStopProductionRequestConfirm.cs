using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events.NotificationArgs;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    /// <summary>
    /// Остановка производства для BackManagerBoss (подтверждение)
    /// </summary>
    public class DoStepCommandStopProductionRequestConfirm : DoStepCommand<TaskViewModelBackManagerBoss>
    {
        protected override ScriptStep Step => ScriptStep.Stop;

        protected override string ConfirmationMessage => "Вы уверены, что хотите согласовать остановку производства этого оборудования?";

        public DoStepCommandStopProductionRequestConfirm(TaskViewModelBackManagerBoss viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override string GetStatusComment()
        {
            return $"Остановка производства согласована ({GlobalAppProperties.User}).";
        }

        protected override IEnumerable<NotificationUnit> GetNotificationUnits()
        {
            yield return new NotificationUnit
            {
                ActionType = EventServiceActionType.PriceEngineeringTaskProductionRequestStopConfirm,
                RecipientRole = Role.SalesManager,
                RecipientUser = Manager,
                TargetEntityId = ViewModel.Model.Id
            };
        }

        protected override bool NeedAddSameStatusOnSubTasks => true;

        protected override void BeforeDoStepAction()
        {
            ViewModel.Model.SalesUnits.ForEach(salesUnit => salesUnit.Order = null);
        }

        protected override bool CanExecuteMethod()
        {
            return
                GlobalAppProperties.User.RoleCurrent == Role.BackManagerBoss &&
                ViewModel.Status.Equals(ScriptStep.ProductionRequestStop);
        }
    }
}