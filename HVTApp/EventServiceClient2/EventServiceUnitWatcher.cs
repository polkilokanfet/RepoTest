using System;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace EventServiceClient2
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

        public void Start()
        {
            _eventAggregator.GetEvent<AfterInstructTechnicalRequrementsTaskEvent>().Subscribe(
                technicalRequrementsTask =>
                {
                    var unitOfWork = _container.Resolve<IUnitOfWork>();
                    try
                    {
                        var units = unitOfWork.Repository<EventServiceUnit>()
                            .Find(unit => unit.TargetEntityId == technicalRequrementsTask.Id && unit.EventServiceActionType == EventServiceActionType.StartTechnicalRequrementsTask);

                        if (units.Any())
                        {
                            unitOfWork.Repository<EventServiceUnit>().DeleteRange(units);
                            unitOfWork.SaveChanges();
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        unitOfWork.Dispose();
                    }

                });

            _eventAggregator.GetEvent<AfterStopTechnicalRequrementsTaskEvent>().Subscribe(
                technicalRequrementsTask =>
                {
                    var unitOfWork = _container.Resolve<IUnitOfWork>();
                    try
                    {
                        var units = unitOfWork.Repository<EventServiceUnit>()
                            .Find(unit => unit.TargetEntityId == technicalRequrementsTask.Id && unit.EventServiceActionType == EventServiceActionType.StartTechnicalRequrementsTask);

                        if (units.Any())
                        {
                            unitOfWork.Repository<EventServiceUnit>().DeleteRange(units);
                            unitOfWork.SaveChanges();
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        unitOfWork.Dispose();
                    }

                });

            _eventAggregator.GetEvent<AfterFinishPriceCalculationEvent>().Subscribe(
                priceCalculation =>
                {
                    var unitOfWork = _container.Resolve<IUnitOfWork>();
                    try
                    {
                        var units = unitOfWork.Repository<EventServiceUnit>()
                            .Find(unit => unit.TargetEntityId == priceCalculation.Id && unit.EventServiceActionType == EventServiceActionType.StartPriceCalculation);

                        if (units.Any())
                        {
                            unitOfWork.Repository<EventServiceUnit>().DeleteRange(units);
                            unitOfWork.SaveChanges();
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        unitOfWork.Dispose();
                    }

                });

            _eventAggregator.GetEvent<AfterStopPriceCalculationEvent>().Subscribe(
                priceCalculation =>
                {
                    var unitOfWork = _container.Resolve<IUnitOfWork>();
                    try
                    {
                        var units = unitOfWork.Repository<EventServiceUnit>()
                            .Find(unit => unit.TargetEntityId == priceCalculation.Id && unit.EventServiceActionType == EventServiceActionType.StartPriceCalculation);

                        if (units.Any())
                        {
                            unitOfWork.Repository<EventServiceUnit>().DeleteRange(units);
                            unitOfWork.SaveChanges();
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        unitOfWork.Dispose();
                    }

                });

            _eventAggregator.GetEvent<AfterRejectPriceCalculationEvent>().Subscribe(
                priceCalculation =>
                {
                    var unitOfWork = _container.Resolve<IUnitOfWork>();
                    try
                    {
                        var units = unitOfWork.Repository<EventServiceUnit>()
                            .Find(unit => unit.TargetEntityId == priceCalculation.Id && unit.EventServiceActionType == EventServiceActionType.StartPriceCalculation);

                        if (units.Any())
                        {
                            unitOfWork.Repository<EventServiceUnit>().DeleteRange(units);
                            unitOfWork.SaveChanges();
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        unitOfWork.Dispose();
                    }
                });

        }
    }
}