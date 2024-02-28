using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1.Commands
{
    public class StartCommand : BaseBasePriceCalculationCommandNotifyCommand
    {
        public StartCommand(PriceCalculationViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override string ConfirmationMessage => "¬ы уверены, что хотите стартовать задачу?";
        protected override PriceCalculationHistoryItemType HistoryItemType => PriceCalculationHistoryItemType.Start;

        protected override IEnumerable<NotificationUnit> GetNotificationUnits()
        {
            using (var unitOfWork = Container.Resolve<IUnitOfWork>())
            {
                var users = unitOfWork.Repository<User>().Find(user => user.Roles.Any(role => role.Role == Role.Pricer));

                foreach (var user in users)
                {
                    yield return new NotificationUnit
                    {
                        ActionType = NotificationActionType.StartPriceCalculation,
                        RecipientRole = Role.Pricer,
                        RecipientUser = user,
                        TargetEntityId = ViewModel.PriceCalculationWrapper.Model.Id
                    };
                }
            }
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.IsStarted == false && 
                   ViewModel.PriceCalculationWrapper.IsValid && 
                   GlobalAppProperties.User.Id == ViewModel.PriceCalculationWrapper.Initiator?.Model.Id;
        }
    }
}