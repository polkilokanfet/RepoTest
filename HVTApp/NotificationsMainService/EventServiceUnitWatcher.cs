using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace NotificationsMainService
{
    /// <summary>
    /// Класс, который следит за актуальностью EventServiceUnit в базе данных
    /// </summary>
    public class EventServiceUnitWatcher
    {
        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;

        public EventServiceUnitWatcher(IUnityContainer container, IEventAggregator eventAggregator)
        {
            _container = container;
            _eventAggregator = eventAggregator;
        }

        private void M<T>(T entity, EventServiceActionType eventServiceActionType)
            where T : class, IBaseEntity
        {
            Task.Run(
                () =>
                {
                    using (var unitOfWork = _container.Resolve<IUnitOfWork>())
                    {
                        try
                        {
                            var units = unitOfWork.Repository<EventServiceUnit>()
                                .Find(unit => unit.TargetEntityId == entity.Id && 
                                              unit.EventServiceActionType == eventServiceActionType);

                            if (!units.Any()) return;

                            unitOfWork.Repository<EventServiceUnit>().DeleteRange(units);
                            unitOfWork.SaveChanges();
                        }
                        catch
                        {
                        }
                    }
                }).Await();
        }

        public void Start()
        {
            _eventAggregator.GetEvent<AfterInstructTechnicalRequrementsTaskEvent>()
                .Subscribe(technicalRequrementsTask => M(technicalRequrementsTask, EventServiceActionType.StartTechnicalRequrementsTask));

            _eventAggregator.GetEvent<AfterStopTechnicalRequrementsTaskEvent>()
                .Subscribe(technicalRequrementsTask => M(technicalRequrementsTask, EventServiceActionType.StartTechnicalRequrementsTask));

            _eventAggregator.GetEvent<AfterFinishPriceCalculationEvent>()
                .Subscribe(priceCalculation => M(priceCalculation, EventServiceActionType.StartPriceCalculation));

            _eventAggregator.GetEvent<AfterStopPriceCalculationEvent>()
                .Subscribe(priceCalculation => M(priceCalculation, EventServiceActionType.StartPriceCalculation));

            _eventAggregator.GetEvent<AfterRejectPriceCalculationEvent>()
                .Subscribe(priceCalculation => M(priceCalculation, EventServiceActionType.StartPriceCalculation));
        }
    }
}