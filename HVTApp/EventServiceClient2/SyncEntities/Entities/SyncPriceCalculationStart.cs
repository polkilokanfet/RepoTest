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
        public SyncPriceCalculationStart(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId) : base(container, eventServiceHost, appSessionId)
        {
        }

        protected override Action<PriceCalculation> PublishEventAction
        {
            get
            {
                //старт расчета ПЗ интересен только юзеру-расчетчику
                var targetUsersIds =  UnitOfWork.Repository<User>()
                    .Find(user => user.Roles.Any(role => role.Role == Role.Pricer))
                    .Select(user => user.Id);

                return priceCalculation => EventServiceHost.StartPriceCalculationPublishEvent(AppSessionId, priceCalculation.Id, targetUsersIds.ToArray());
            }
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
            foreach (var userId in usersWhoDontResiveAction)
            {
                User user = UnitOfWork.Repository<User>().GetById(userId);
                EventServiceUnit unit = new EventServiceUnit
                {
                    User = user,
                    TargetEntityId = priceCalculation.Id,
                    EventServiceActionType = EventServiceActionType.StartPriceCalculation
                };
                UnitOfWork.Repository<EventServiceUnit>().Add(unit);
            }

            UnitOfWork.SaveChanges();
        }
    }
}