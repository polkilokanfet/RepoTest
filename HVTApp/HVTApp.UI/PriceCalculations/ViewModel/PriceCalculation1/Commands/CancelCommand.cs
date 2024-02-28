using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1.Commands
{
    public class CancelCommand : BaseBasePriceCalculationCommandNotifyCommand
    {
        protected override string ConfirmationMessage => "Вы уверены, что хотите остановить расчёт ПЗ?";
        protected override PriceCalculationHistoryItemType HistoryItemType => PriceCalculationHistoryItemType.Stop;

        public CancelCommand(PriceCalculationViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override IEnumerable<NotificationUnit> GetNotificationUnits()
        {
            using (var unitOfWork = Container.Resolve<IUnitOfWork>())
            {
                var users = unitOfWork.Repository<User>().Find(user => user.Roles.Any(role => role.Role == Role.Pricer));

                foreach (var user in users)
                {
                    yield return new NotificationUnit
                    {
                        ActionType = NotificationActionType.CancelPriceCalculation,
                        RecipientRole = Role.Pricer,
                        RecipientUser = user,
                        TargetEntityId = ViewModel.PriceCalculationWrapper.Model.Id
                    };
                }
            }
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.IsStarted &&
                   GlobalAppProperties.User.Id == ViewModel.PriceCalculationWrapper.Initiator?.Model.Id;
        }
    }
}