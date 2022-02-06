using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace EventServiceClient2.SyncEntities
{
    public abstract class SyncUnit<TModel, TAfterSaveEvent> : ISyncUnit
        where TAfterSaveEvent : PubSubEvent<TModel>, new()
        where TModel : BaseEntity
    {
        private readonly IEventAggregator _eventAggregator;
        protected readonly IUnitOfWork UnitOfWork;
        protected Guid AppSessionId { get; private set; }

        protected ServiceReference1.EventServiceClient EventServiceHost { get; private set; }
        public Type ModelType => typeof(TModel);
        public Type EventType => typeof(TAfterSaveEvent);

        protected SyncUnit(IUnityContainer container)
        {
            _eventAggregator = container.Resolve<IEventAggregator>();
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

        protected abstract void DoPublishAction(TModel model);

        /// <summary>
        /// Вычисление пользователей-адресатов этого уведомления
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected abstract IEnumerable<Guid> GetTargetUsersIds(TModel model);

        protected abstract Func<TModel, bool> ActionPublishThroughEventService { get; }

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
                if (PublishNotification(model))
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

        private bool PublishNotification(TModel model)
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
                    return ActionPublishThroughEventService.Invoke(model);
                    DoPublishAction(model);asd
                    return true;
                }
                else
                {
                    //кидаем событие
                    ServiceHostDisabled?.Invoke();
                    return false;
                }
            }
            //хост недоступен
            catch (TimeoutException)
            {
                //кидаем событие
                ServiceHostDisabled?.Invoke();
                return false;
            }
#if DEBUG
#else
            catch (Exception e)
            {
                _messageService.ShowOkMessageDialog(e.GetType().FullName, e.PrintAllExceptions());
                return false;
            }
#endif
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
}