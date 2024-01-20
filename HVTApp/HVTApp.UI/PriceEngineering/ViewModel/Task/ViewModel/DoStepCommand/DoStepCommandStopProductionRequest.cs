using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandStopProductionRequest : DoStepCommand
    {
        protected override ScriptStep Step => ScriptStep.StopProductionRequest;
        protected override string ConfirmationMessage => "¬ы уверены, что хотите остановить производство этого оборудовани€?";
        protected override IEnumerable<NotificationArgsItem> GetEventServiceItems()
        {
            foreach (var user in Container.Resolve<IUnitOfWork>().Repository<User>().Find(user => user.Roles.Any(role => role.Role == Role.BackManagerBoss)))
            {
                yield return new NotificationArgsItem(user, Role.BackManagerBoss, $"«апрос на остановку производства: {ViewModel.Model}");
            }
        }

        public DoStepCommandStopProductionRequest(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override bool SetSameStatusOnSubTasks => true;
    }
}