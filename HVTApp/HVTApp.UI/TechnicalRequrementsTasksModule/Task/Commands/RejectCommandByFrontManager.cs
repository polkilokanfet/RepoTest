using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class RejectCommandByFrontManager : BaseNotifyTechnicalRequrementsTaskViewModelCommand
    {
        protected override string ConfirmationMessage => "Вы уверены, что хотите отклонить данную проработку?";
        protected override TechnicalRequrementsTaskHistoryElementType HistoryElementType => TechnicalRequrementsTaskHistoryElementType.RejectByFrontManager;

        public RejectCommandByFrontManager(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            if (string.IsNullOrWhiteSpace(ViewModel.HistoryElementWrapper.Comment))
            {
                MessageService.Message("Информация", "Перед отклонением необходимо заполнить комментарий (причину отклонения)");
                return;
            }

            base.ExecuteMethod();

            ViewModel.HistoryElementWrapper = null;
        }

        protected override IEnumerable<NotificationUnit> GetNotificationUnits()
        {
            if (ViewModel.TechnicalRequrementsTaskWrapper.BackManager != null)
            {
                yield return new NotificationUnit
                {
                    ActionType = NotificationActionType.RejectByFrontManagerTechnicalRequirementsTask,
                    RecipientRole = Role.BackManager,
                    RecipientUser = ViewModel.TechnicalRequrementsTaskWrapper.BackManager.Model,
                    TargetEntityId = ViewModel.TechnicalRequrementsTaskWrapper.Model.Id
                };
            }
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.IsValid &&
                   ViewModel.IsFinished &&
                   !ViewModel.IsAccepted;
        }
    }
}