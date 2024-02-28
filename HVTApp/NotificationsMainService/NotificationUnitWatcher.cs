using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace NotificationsMainService
{
    /// <summary>
    /// Класс, который следит за актуальностью EventServiceUnit в базе данных
    /// </summary>
    public class NotificationUnitWatcher : INotificationUnitWatcher
    {
        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;

        public NotificationUnitWatcher(IUnityContainer container, IEventAggregator eventAggregator)
        {
            _container = container;
            _eventAggregator = eventAggregator;
        }

        private void M<T>(T entity, NotificationActionType notificationActionType)
            where T : class, IBaseEntity
        {
            //Task.Run(
            //    () =>
            //    {
            //        using (var unitOfWork = _container.Resolve<IUnitOfWork>())
            //        {
            //            try
            //            {
            //                var units = unitOfWork.Repository<EventServiceUnit>()
            //                    .Find(unit => unit.TargetEntityId == entity.Id && 
            //                                  unit.EventServiceActionType == eventServiceActionType);

            //                if (!units.Any()) return;

            //                unitOfWork.Repository<EventServiceUnit>().DeleteRange(units);
            //                unitOfWork.SaveChanges();
            //            }
            //            catch
            //            {
            //            }
            //        }
            //    }).Await();
        }

        public void Start()
        {
            _eventAggregator.GetEvent<AfterInstructTechnicalRequrementsTaskEvent>()
                .Subscribe(technicalRequrementsTask => M(technicalRequrementsTask, NotificationActionType.StartTechnicalRequirementsTask));

            _eventAggregator.GetEvent<AfterStopTechnicalRequrementsTaskEvent>()
                .Subscribe(technicalRequrementsTask => M(technicalRequrementsTask, NotificationActionType.StartTechnicalRequirementsTask));

            _eventAggregator.GetEvent<AfterFinishPriceCalculationEvent>()
                .Subscribe(priceCalculation => M(priceCalculation, NotificationActionType.StartPriceCalculation));

            _eventAggregator.GetEvent<AfterStopPriceCalculationEvent>()
                .Subscribe(priceCalculation => M(priceCalculation, NotificationActionType.StartPriceCalculation));

            _eventAggregator.GetEvent<AfterRejectPriceCalculationEvent>()
                .Subscribe(priceCalculation => M(priceCalculation, NotificationActionType.StartPriceCalculation));
        }
    }
}