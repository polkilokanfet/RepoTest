using System;
using HVTApp.Infrastructure;
using Prism.Regions;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model.POCOs;

namespace NotificationsService
{
    internal abstract class Notification<TTargetEntity> : INotificationHelper where  TTargetEntity : class, IBaseEntity
    {
        protected IUnitOfWork UnitOfWork { get; }
        protected readonly IRegionManager RegionManager;

        protected NotificationUnit Unit { get; }

        public TTargetEntity TargetUnit => UnitOfWork.Repository<TTargetEntity>().GetById(Unit.TargetEntityId);

        protected Notification(IUnitOfWork unitOfWork, NotificationUnit unit, IRegionManager regionManager)
        {
            UnitOfWork = unitOfWork;
            Unit = unit;
            RegionManager = regionManager;
        }

        public abstract string GetCommonInfo();
        public abstract string GetActionInfo();
        public abstract Action GetOpenTargetEntityViewAction();
    }
}