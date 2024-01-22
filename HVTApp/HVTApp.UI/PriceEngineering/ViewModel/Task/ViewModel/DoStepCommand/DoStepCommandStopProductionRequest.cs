using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandStopProductionRequest : DoStepCommand<TaskViewModelManagerOld>
    {
        protected override ScriptStep Step => ScriptStep.ProductionRequestStop;
        protected override string ConfirmationMessage => "¬ы уверены, что хотите остановить производство этого оборудовани€?";
        protected override IEnumerable<NotificationItem> GetEventServiceItems()
        {
            foreach (var user in UnitOfWork.Repository<User>().Find(user => user.Roles.Any(role => role.Role == Role.BackManagerBoss)))
            {
                yield return new NotificationItem(user, Role.BackManagerBoss, $"«апрос на остановку производства: {ViewModel.Model}");
            }
        }

        public DoStepCommandStopProductionRequest(TaskViewModelManagerOld viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override bool NeedAddSameStatusOnSubTasks => true;
    }
}