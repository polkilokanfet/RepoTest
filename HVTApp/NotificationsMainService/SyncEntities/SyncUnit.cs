using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Prism.Events;

namespace NotificationsMainService.SyncEntities
{
    public abstract class SyncUnit<TModel, TAfterSaveEvent> : ISyncUnit//, ITargetUser<TModel>
        where TAfterSaveEvent : PubSubEvent<TModel>, new()
        where TModel : BaseEntity
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly INotificationFromDataBaseService _notificationFromDataBaseService;

        protected readonly IUnitOfWork UnitOfWork;

        protected IEventServiceClient EventServiceClient { get; }
        public Type ModelType => typeof(TModel);
        public Type EventType => typeof(TAfterSaveEvent);

        protected SyncUnit(IEventAggregator eventAggregator, INotificationFromDataBaseService notificationFromDataBaseService, IUnitOfWork unitOfWork, IEventServiceClient eventServiceClient)
        {
            _eventAggregator = eventAggregator;
            _notificationFromDataBaseService = notificationFromDataBaseService;
            UnitOfWork = unitOfWork;
            EventServiceClient = eventServiceClient;
            
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

        /// <summary>
        /// Вычисление пользователей-адресатов этого уведомления
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected virtual IEnumerable<User> GetUsersForNotification(TModel model)
        {
            return UnitOfWork.Repository<User>().Find(user => user.IsActual);
        }

        /// <summary>
        /// Роли пользователей, которым должны быть отправлены уведомления
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<Role> GetRolesForNotification(TModel model)
        {
            return (Role[]) Enum.GetValues(typeof(Role));
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
            var targetUsers = GetUsersForNotification(model).ToList();

            //список Id пользователей, которым не доставлено уведомление
            var usersWhoDidNotReciveNotification = targetUsers.ToList();

            //рассылка уведомлений
            foreach (var targetUser in targetUsers)
            {
                var roles = targetUser.Roles.Select(userRole => userRole.Role).Intersect(this.GetRolesForNotification(model));

                foreach (var role in roles)
                {
                    //пользователь получил уведомление?
                    if (PublishNotificationForUser(targetUser, role, model))
                        usersWhoDidNotReciveNotification.Remove(targetUser);
                }
            }

            //сохранение в базу данных уведомлений, которые не были доставлены адресатам
            foreach (var user in usersWhoDidNotReciveNotification)
            {
                var unit = new EventServiceUnit
                {
                    User = user,
                    TargetEntityId = model.Id,
                    EventServiceActionType = this.EventServiceActionType
                };
                _notificationFromDataBaseService.SaveNotificationInDataBase(unit);
            }
        }

        private bool PublishNotificationForUser(User targetUser, Role targetRole, TModel model)
        {
            try
            {
                //публикуем действие
                return ActionPublishThroughEventServiceForUser.Invoke(targetUser.Id, targetRole, model.Id);
            }
            catch (TimeoutException)
            {
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

        public void Dispose()
        {
            Unsubscribe();
            UnitOfWork.Dispose();
        }
    }

    public delegate bool ActionPublishThroughEventServiceForUserDelegate(Guid targetUserId, Role targetRole, Guid modelId);
}