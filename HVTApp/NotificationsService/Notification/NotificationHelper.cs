using System;
using HVTApp.Infrastructure;
using Prism.Regions;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model.POCOs;
using Prism.Events;

namespace NotificationsService
{
    internal abstract class NotificationHelper<TTargetEntity, TEventType> : INotificationHelper 
        where  TTargetEntity : class, IBaseEntity
        where TEventType : PubSubEvent<TTargetEntity>, new()
    {
        protected IUnitOfWork UnitOfWork { get; }
        protected readonly IRegionManager RegionManager;
        private readonly IEventAggregator _eventAggregator;

        protected NotificationUnit Unit { get; }

        public TTargetEntity TargetUnit => UnitOfWork.Repository<TTargetEntity>().GetById(Unit.TargetEntityId);

        protected NotificationHelper(IUnitOfWork unitOfWork, NotificationUnit unit, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            UnitOfWork = unitOfWork;
            Unit = unit;
            RegionManager = regionManager;
            _eventAggregator = eventAggregator;
        }

        public abstract string GetCommonInfo();

        public string GetActionInfo()
        {
            return this.Unit.GetActionString();
        }

        public abstract Action GetOpenTargetEntityViewAction();

        public void RefreshTargetEntityAction()
        {
            _eventAggregator.GetEvent<TEventType>().Publish(this.TargetUnit);
        }
    }
}