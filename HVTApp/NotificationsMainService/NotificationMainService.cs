using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Infrastructure.Services;
using Microsoft.Practices.Unity;
using NotificationsMainService.SyncEntities;
using Prism.Events;

namespace NotificationsMainService
{
    public class NotificationMainService : INotificationMainService, IDisposable
    {
        public IEventServiceClient EventServiceClient { get; }

        private readonly SyncUnitsContainer _syncUnitsContainer = new SyncUnitsContainer();

        public NotificationMainService(IUnityContainer container, IEventServiceClient eventServiceClient)
        {
            EventServiceClient = eventServiceClient;
            var types = this.GetType().Assembly.GetTypes().Where(x => x.IsAbstract == false && x.GetInterfaces().Contains(typeof(ISyncUnit)));
            foreach (var unitType in types)
            {
                _syncUnitsContainer.Add((ISyncUnit)container.Resolve(unitType));
            }
        }

        public void Start()
        {
            this.EventServiceClient.Start();
        }

        /// <summary>
        /// Публикация события синхронизации только внутри текущего приложения (для текущего пользователя приложения)
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="model"></param>
        public bool PublishWithinAppForCurrentUser<TModel, TEvent>(TModel model)
            where TModel : BaseEntity
            where TEvent : PubSubEvent<TModel>
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            //поиск целевого контейнера
            var targetSyncUnit = _syncUnitsContainer.GetSyncUnit(typeof(TModel), typeof(TEvent));

            //если пользователь текущего приложения является целевым для этого события
            if (((ITargetUser<TModel>)targetSyncUnit).CurrentUserIsTargetForNotification(model))
            {
                //переводим в основной поток
                System.Windows.Application.Current.Dispatcher.Invoke(
                    () =>
                    {
                        //публикуем событие
                        targetSyncUnit.PublishWithinApp(model);
                    });

                return true;
            }

            return false;
        }

        public void Dispose()
        {
            _syncUnitsContainer.Dispose();
        }
    }
}