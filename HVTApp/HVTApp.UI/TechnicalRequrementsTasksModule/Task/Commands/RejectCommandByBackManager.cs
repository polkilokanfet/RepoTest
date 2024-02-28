using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class RejectCommandByBackManager : BaseNotifyTechnicalRequrementsTaskViewModelCommand
    {
        protected override string ConfirmationMessage => "Вы уверены, что хотите отклонить эту задачу?";
        protected override TechnicalRequrementsTaskHistoryElementType HistoryElementType => TechnicalRequrementsTaskHistoryElementType.Reject;

        public RejectCommandByBackManager(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
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
            var manager = ViewModel.TechnicalRequrementsTaskWrapper.Model.Requrements
                .SelectMany(x => x.SalesUnits)
                .FirstOrDefault()?.Project.Manager;

            if (manager != null)
            {
                yield return new NotificationUnit
                {
                    ActionType = NotificationActionType.RejectTechnicalRequirementsTask,
                    RecipientRole = Role.SalesManager,
                    RecipientUser = manager,
                    TargetEntityId = ViewModel.TechnicalRequrementsTaskWrapper.Model.Id
                };
            }
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.IsValid &&
                   ViewModel.IsStarted &&
                   !ViewModel.IsRejected &&
                   !ViewModel.IsFinished;
        }
    }
}