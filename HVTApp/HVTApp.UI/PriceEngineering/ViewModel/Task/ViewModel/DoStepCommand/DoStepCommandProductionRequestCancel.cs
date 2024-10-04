using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandProductionRequestCancel : DoStepCommand<TaskViewModelManagerOld>
    {
        protected override ScriptStep Step => ScriptStep.ProductionRequestCancel;

        protected override string ConfirmationMessage => "Вы уверены, что хотите отозвать запрос на открытие производства?";

        public DoStepCommandProductionRequestCancel(TaskViewModelManagerOld viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override IEnumerable<NotificationUnit> GetNotificationUnits()
        {
            var planMaker = ViewModel.Model.UserPlanMaker;
            if (planMaker != null)
            {
                yield return new NotificationUnit
                {
                    ActionType = NotificationActionType.PriceEngineeringTaskProductionRequestCancel,
                    RecipientRole = Role.PlanMaker,
                    RecipientUser = planMaker,
                    TargetEntityId = ViewModel.Model.Id
                };
            }

            foreach (var user in UnitOfWork.Repository<User>().Find(user => user.Roles.Any(role => role.Role == Role.BackManagerBoss)))
            {
                yield return new NotificationUnit
                {
                    ActionType = NotificationActionType.PriceEngineeringTaskProductionRequestCancel,
                    RecipientRole = Role.BackManagerBoss,
                    RecipientUser = user,
                    TargetEntityId = ViewModel.Model.Id
                };
            }
        }

        protected override bool NeedAddSameStatusOnSubTasks => true;

        protected override void BeforeDoStepAction()
        {
            foreach (var su in ViewModel.SalesUnits)
            {
                su.SignalToStartProduction = null;
                su.SignalToStartProductionDone = null;
            }
        }
    }
}