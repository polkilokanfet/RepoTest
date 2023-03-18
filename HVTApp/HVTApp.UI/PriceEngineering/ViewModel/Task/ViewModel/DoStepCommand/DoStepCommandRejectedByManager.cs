using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandRejectedByManager : DoStepCommand
    {
        protected override ScriptStep Step => ScriptStep.RejectByManager;
        protected override string ConfirmationMessage => "Вы уверены, что хотите отклонить проработку задачи?";
        protected override IEnumerable<NotificationArgsItem> GetEventServiceItems()
        {
            yield return new NotificationArgsItem(ViewModel.UserConstructor, Role.Constructor, $"Проработка ТСП отклонена менеджером: {ViewModel.Model}");
        }

        public DoStepCommandRejectedByManager(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }
    }
}