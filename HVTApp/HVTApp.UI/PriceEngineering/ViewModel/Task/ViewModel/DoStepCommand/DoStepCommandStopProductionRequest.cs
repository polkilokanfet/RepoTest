using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandStopProductionRequest : DoStepCommand<TaskViewModelManagerOld>
    {
        protected override ScriptStep Step => ScriptStep.ProductionRequestStop;
        protected override string ConfirmationMessage => "Вы уверены, что хотите остановить производство этого оборудования?";

        public DoStepCommandStopProductionRequest(TaskViewModelManagerOld viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override IEnumerable<NotificationUnit> GetNotificationUnits()
        {
            foreach (var user in UnitOfWork.Repository<User>().Find(user => user.Roles.Any(role => role.Role == Role.BackManagerBoss)))
            {
                yield return new NotificationUnit
                {
                    ActionType = NotificationActionType.PriceEngineeringTaskProductionRequestStop,
                    RecipientRole = Role.BackManagerBoss,
                    RecipientUser = user,
                    TargetEntityId = ViewModel.Model.Id
                };
            }
        }

        protected override bool NeedAddSameStatusOnSubTasks => true;
    }
}