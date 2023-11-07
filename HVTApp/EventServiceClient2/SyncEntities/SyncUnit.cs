using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace EventServiceClient2.SyncEntities
{
    public abstract class SyncUnit<TModel, TAfterSaveEvent> : ISyncUnit, ITargetUser<TModel>
        where TAfterSaveEvent : PubSubEvent<TModel>, new()
        where TModel : BaseEntity
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageService _messageService;
        protected readonly IUnitOfWork UnitOfWork;
        protected Guid AppSessionId { get; private set; }

        protected ServiceReference1.EventServiceClient EventServiceHost { get; private set; }
        public Type ModelType => typeof(TModel);
        public Type EventType => typeof(TAfterSaveEvent);

        protected SyncUnit(IUnityContainer container)
        {
            _eventAggregator = container.Resolve<IEventAggregator>();
            _messageService = container.Resolve<IMessageService>();
            UnitOfWork = container.Resolve<IUnitOfWork>();
            Subscribe();
        }

        private void Subscribe()
        {
            _eventAggregator.GetEvent<TAfterSaveEvent>().Subscribe(PublishThroughEventService, true);
        }

        private void Unsubscribe()
        {
            _eventAggregator.GetEvent<TAfterSaveEvent>().Unsubscribe(PublishThroughEventService);
        }

        public abstract bool IsTargetUser(User user, TModel model);

        public virtual bool CurrentUserIsTargetForNotification(TModel model)
        {
            return IsTargetUser(GlobalAppProperties.User, model) && 
                   GetRolesForNotification().Contains(GlobalAppProperties.User.RoleCurrent);
        }

        protected virtual IEnumerable<Role> GetRolesForNotification()
        {
            return (Role[]) Enum.GetValues(typeof(Role));
        }

        /// <summary>
        /// Вычисление пользователей-адресатов этого уведомления
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected virtual IEnumerable<Guid> GetTargetUsersIds(TModel model)
        {
            return UnitOfWork.Repository<User>()
                .Find(user => user.IsActual && this.IsTargetUser(user, model))
                .Select(user => user.Id)
                .Distinct();
        }

        protected abstract ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser { get; }

        protected abstract EventServiceActionType EventServiceActionType { get; }

        /// <summary>
        /// Публикация события через сервис синхронизации
        /// </summary>
        /// <param name="model"></param>
        private void PublishThroughEventService(TModel model)
        {
            //список Id пользователей, которым адресовано уведомление
            var targetUsersIds = GetTargetUsersIds(model).Distinct().ToList();

            //список Id пользователей, которым не доставлено уведомление
            var usersIdsWhoDidNotReciveNotification = targetUsersIds.ToList();

            //рассылка уведомлений
            foreach (var targetUsersId in targetUsersIds)
            {
                //пользователь получил уведомление?
                if (PublishNotificationForUser(targetUsersId, model))
                {
                    usersIdsWhoDidNotReciveNotification.Remove(targetUsersId);
                }
            }

            //сохранение в базу данных уведомлений, которые не были доставлены адресатам
            if (usersIdsWhoDidNotReciveNotification.Any())
            {
                foreach (var userId in usersIdsWhoDidNotReciveNotification)
                {
                    EventServiceUnit unit = new EventServiceUnit
                    {
                        User = UnitOfWork.Repository<User>().GetById(userId),
                        TargetEntityId = model.Id,
                        EventServiceActionType = this.EventServiceActionType
                    };
                    UnitOfWork.Repository<EventServiceUnit>().Add(unit);
                }

                UnitOfWork.SaveChanges();
            }
        }

        private bool PublishNotificationForUser(Guid targetUserId, TModel model)
        {
            if (EventServiceHost == null)
                return false;

            try
            {
                //если хост есть и он в рабочем состоянии
                if (EventServiceHost.State != CommunicationState.Faulted &&
                    EventServiceHost.State != CommunicationState.Closed)
                {
                    //публикуем действие
                    return ActionPublishThroughEventServiceForUser.Invoke(this.AppSessionId, targetUserId, model.Id);
                }
                else
                {
                    //кидаем событие
                    ServiceHostDisabled?.Invoke();
                }
            }
            //хост недоступен
            catch (TimeoutException)
            {
                //кидаем событие
                ServiceHostDisabled?.Invoke();
            }
#if DEBUG
#else
            catch (Exception e)
            {
                _messageService.Message(e.GetType().FullName, e.PrintAllExceptions());
            }
#endif
            return false;
        }

        /// <summary>
        /// Публикация события только внутри текущего приложения
        /// </summary>
        /// <param name="model"></param>
        public void PublishWithinApp(object model)
        {
            Unsubscribe();
            _eventAggregator.GetEvent<TAfterSaveEvent>().Publish((TModel)model);
            Subscribe();
        }

        public void Connect(ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId)
        {
            this.EventServiceHost = eventServiceHost;
            this.AppSessionId = appSessionId;
        }

        public void Disconnect()
        {
            this.EventServiceHost = null;
        }

        /// <summary>
        /// Хост сервиса недоступен
        /// </summary>
        public event Action ServiceHostDisabled;

        public void Dispose()
        {
            Unsubscribe();
        }
    }

    public delegate bool ActionPublishThroughEventServiceForUserDelegate(Guid appId, Guid targetUserId, Guid modelId);
}