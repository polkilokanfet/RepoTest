using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncPriceCalculationStart : SyncUnit<PriceCalculation, AfterStartPriceCalculationEvent>
    {
        public SyncPriceCalculationStart(IUnityContainer container) : base(container)
        {
        }

        protected override void DoPublishAction(PriceCalculation priceCalculation)
        {
            //старт расчета ПЗ интересен только юзеру-расчетчику
            var targetUsersIds = UnitOfWork.Repository<User>()
                .Find(user => user.Roles.Any(role => role.Role == Role.Pricer))
                .Select(user => user.Id);

            //Запускаем событие на хост
            //Пользователи, которые не получили сообщение
            var usersWhoDontResiveAction = EventServiceHost.StartPriceCalculationPublishEvent(AppSessionId, priceCalculation.Id, targetUsersIds.ToArray());
            if (usersWhoDontResiveAction.Any())
            {
                foreach (var userId in usersWhoDontResiveAction)
                {
                    EventServiceUnit unit = new EventServiceUnit
                    {
                        User = UnitOfWork.Repository<User>().GetById(userId),
                        TargetEntityId = priceCalculation.Id,
                        EventServiceActionType = EventServiceActionType.StartPriceCalculation
                    };
                    UnitOfWork.Repository<EventServiceUnit>().Add(unit);
                }

                UnitOfWork.SaveChanges();
            }
        }

        protected override IEnumerable<Guid> GetTargetUsersIds(PriceCalculation model)
        {
            return UnitOfWork.Repository<User>()
                .Find(user => user.Roles.Any(role => role.Role == Role.Pricer))
                .Select(user => user.Id);
        }

        protected override Func<PriceCalculation, bool> ActionPublishThroughEventService
        {
            get { return p => EventServiceHost.StartPriceCalculationPublishEvent(); }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.StartPriceCalculation;
    }
}