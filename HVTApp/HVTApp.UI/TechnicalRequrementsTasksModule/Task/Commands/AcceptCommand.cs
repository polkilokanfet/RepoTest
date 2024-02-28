using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class AcceptCommand : BaseNotifyTechnicalRequrementsTaskViewModelCommand
    {
        protected override string ConfirmationMessage => "¬ы уверены, что хотите прин€ть проработку задачи?";
        protected override TechnicalRequrementsTaskHistoryElementType HistoryElementType => TechnicalRequrementsTaskHistoryElementType.Accept;

        public AcceptCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override IEnumerable<NotificationUnit> GetNotificationUnits()
        {
            if (ViewModel.TechnicalRequrementsTaskWrapper.BackManager != null)
            {
                yield return new NotificationUnit
                {
                    ActionType = NotificationActionType.AcceptTechnicalRequirementsTask,
                    RecipientRole = Role.BackManager,
                    RecipientUser = ViewModel.TechnicalRequrementsTaskWrapper.BackManager.Model,
                    TargetEntityId = ViewModel.TechnicalRequrementsTaskWrapper.Model.Id
                };
            }
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.IsFinished &&
                   !ViewModel.IsAccepted;
        }
    }
}