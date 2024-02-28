using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class StopCommand : BaseNotifyTechnicalRequrementsTaskViewModelCommand
    {
        protected override string ConfirmationMessage => "¬ы уверены, что хотите остановить задачу?";
        protected override TechnicalRequrementsTaskHistoryElementType HistoryElementType => TechnicalRequrementsTaskHistoryElementType.Stop;

        public StopCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ActionBeforeSave()
        {
            ViewModel.TechnicalRequrementsTaskWrapper.DesiredFinishDate = null;
        }

        protected override IEnumerable<NotificationUnit> GetNotificationUnits()
        {
            if (ViewModel.TechnicalRequrementsTaskWrapper.BackManager != null)
            {
                yield return new NotificationUnit
                {
                    ActionType = NotificationActionType.StopTechnicalRequirementsTask,
                    RecipientRole = Role.BackManager,
                    RecipientUser = ViewModel.TechnicalRequrementsTaskWrapper.BackManager.Model,
                    TargetEntityId = ViewModel.TechnicalRequrementsTaskWrapper.Model.Id
                };
            }
            else
            {
                var users = UnitOfWork.Repository<User>().Find(user => user.Roles.Any(role => role.Role == Role.BackManagerBoss));

                foreach (var user in users)
                {
                    yield return new NotificationUnit
                    {
                        ActionType = NotificationActionType.StopTechnicalRequirementsTask,
                        RecipientRole = Role.BackManagerBoss,
                        RecipientUser = user,
                        TargetEntityId = ViewModel.TechnicalRequrementsTaskWrapper.Model.Id
                    };
                }
            }
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.IsValid &&
                   ViewModel.IsStarted && 
                   !ViewModel.IsStopped;
        }
    }
}