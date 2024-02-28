using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1.Commands
{
    public class RejectCommand : BaseBasePriceCalculationCommandNotifyCommand
    {
        public RejectCommand(PriceCalculationViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override string ConfirmationMessage => "Вы уверены, что хотите отклонить задачу?";
        protected override PriceCalculationHistoryItemType HistoryItemType => PriceCalculationHistoryItemType.Reject;

        protected override void ExecuteMethod()
        {
            if (string.IsNullOrWhiteSpace(ViewModel.HistoryItem.Comment))
            {
                Container.Resolve<IMessageService>().Message("Внимание", "Для отклонения заполните комментарий");
                return;
            }

            base.ExecuteMethod();

            ViewModel.CanChangePriceOnPropertyChanged();
        }

        protected override IEnumerable<NotificationUnit> GetNotificationUnits()
        {
            var users = ViewModel.PriceCalculationWrapper.Model.PriceCalculationItems
                .SelectMany(priceCalculationItem => priceCalculationItem.SalesUnits)
                .Select(salesUnit => salesUnit.Project.Manager)
                .Distinct();

            foreach (var user in users)
            {
                yield return new NotificationUnit
                {
                    ActionType = NotificationActionType.RejectPriceCalculation,
                    RecipientRole = Role.SalesManager,
                    RecipientUser = user,
                    TargetEntityId = ViewModel.PriceCalculationWrapper.Model.Id
                };
            }
        }

        protected override bool CanExecuteMethod()
        {
            if (ViewModel.PriceCalculationWrapper == null)
            {
                return false;
            }

            return ViewModel.IsStarted &&
                   !ViewModel.IsFinished &&
                   !ViewModel.IsRejected &&
                   ViewModel.PriceCalculationWrapper.IsValid;
        }
    }
}