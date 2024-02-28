using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class FinishCommand : BaseNotifyTechnicalRequrementsTaskViewModelCommand
    {
        protected override string ConfirmationMessage => ViewModel.TechnicalRequrementsTaskWrapper.AnswerFiles.Any() 
                ? "Вы уверены, что хотите завершить проработку задачи?" 
                : "Вы не вложили ни один ответ конструкторов.\nВы уверены, что хотите завершить проработку задачи?";

        protected override TechnicalRequrementsTaskHistoryElementType HistoryElementType => TechnicalRequrementsTaskHistoryElementType.Finish;

        public FinishCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            //если требуется РТЗ
            if (ViewModel.TechnicalRequrementsTaskWrapper.LogisticsCalculationRequired &&
                !ViewModel.TechnicalRequrementsTaskWrapper.ShippingCostFiles.Any())
            {
                MessageService.Message("Информация", "Добавьте в задачу расчет транспортных затрат.");
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
                    ActionType = NotificationActionType.FinishTechnicalRequirementsTask,
                    RecipientRole = Role.SalesManager,
                    RecipientUser = manager,
                    TargetEntityId = ViewModel.TechnicalRequrementsTaskWrapper.Model.Id
                };
            }
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.IsStarted && 
                   !ViewModel.IsFinished && 
                   ViewModel.TechnicalRequrementsTaskWrapper.IsValid;
        }
    }
}