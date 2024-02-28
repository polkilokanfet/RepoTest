using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class StartCommand : BaseNotifyTechnicalRequrementsTaskViewModelCommand
    {
        protected override string ConfirmationMessage => "Вы уверены, что хотите запустить задачу?";
        protected override TechnicalRequrementsTaskHistoryElementType HistoryElementType => TechnicalRequrementsTaskHistoryElementType.Start;

        public StartCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ActionBeforeSave()
        {
            //удаление бэк менеджера, если он уже не работает в компании
            if (ViewModel.TechnicalRequrementsTaskWrapper.BackManager != null &&
                ViewModel.TechnicalRequrementsTaskWrapper.BackManager.IsActual == false)
            {
                ViewModel.TechnicalRequrementsTaskWrapper.BackManager = null;
            }
        }

        protected override IEnumerable<NotificationUnit> GetNotificationUnits()
        {
            if (ViewModel.TechnicalRequrementsTaskWrapper.BackManager != null)
            {
                yield return new NotificationUnit
                {
                    ActionType = NotificationActionType.StartTechnicalRequirementsTask,
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
                        ActionType = NotificationActionType.StartTechnicalRequirementsTask,
                        RecipientRole = Role.BackManagerBoss,
                        RecipientUser = user,
                        TargetEntityId = ViewModel.TechnicalRequrementsTaskWrapper.Model.Id
                    };
                }
            }
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.IsStarted == false && 
                   ViewModel.IsValid && 
                   ViewModel.IsChanged;
        }
    }
}