using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    /// <summary>
    /// ќстановка производства дл€ BackManagerBoss (подтверждение)
    /// </summary>
    public class DoStepCommandStopProductionRequestConfirm : DoStepCommand<TaskViewModelBackManagerBoss>
    {
        protected override ScriptStep Step => ScriptStep.Stop;

        protected override string ConfirmationMessage => "¬ы уверены, что хотите согласовать остановку производства этого оборудовани€?";

        public DoStepCommandStopProductionRequestConfirm(TaskViewModelBackManagerBoss viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override IEnumerable<NotificationArgsItem> GetEventServiceItems()
        {
            yield return new NotificationArgsItem(Manager, Role.SalesManager, $"ѕроизводство остановлено: {ViewModel.Model}");
        }

        protected override string GetStatusComment()
        {
            return $"ќстановка производства согласована ({GlobalAppProperties.User}).";
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
                ViewModel.Status.Equals(ScriptStep.StopProductionRequest);
        }
    }
}