using System;
using System.Collections.Generic;
using System.ServiceModel;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
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
            _eventAggregator.GetEvent<TAfterSaveEvent>().Subscribe(PublishThroughEventServiceClient, true);
        }

        private void Unsubscribe()
        {
            _eventAggregator.GetEvent<TAfterSaveEvent>().Unsubscribe(PublishThroughEventServiceClient);
        }

        protected abstract void DoPublishAction(TModel model);

        /// <summary>
        /// Публикация события через сервис синхронизации
        /// </summary>
        /// <param name="model"></param>
        private void PublishThroughEventServiceClient(TModel model)
        {
            if (EventServiceHost == null)
                return;

            try
            {
                //если хост есть и он в рабочем состоянии
                if (EventServiceHost.State != CommunicationState.Faulted &&
                    EventServiceHost.State != CommunicationState.Closed)
                {
                    //публикуем действие
                    DoPublishAction(model);
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
                _messageService.ShowOkMessageDialog(e.GetType().FullName, e.PrintAllExceptions());
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