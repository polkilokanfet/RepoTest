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
        protected readonly ServiceReference1.EventServiceClient EventServiceHost;
        protected readonly Guid AppSessionId;

        public Type ModelType => typeof(TModel);
        public Type EventType => typeof(TAfterSaveEvent);

        protected SyncUnit(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId)
        {
            _eventAggregator = container.Resolve<IEventAggregator>();
            UnitOfWork = container.Resolve<IUnitOfWork>();
            EventServiceHost = eventServiceHost;
            AppSessionId = appSessionId;
            Subscribe();
        }

        private void Subscribe()
        {
            _eventAggregator.GetEvent<TAfterSaveEvent>().Subscribe(PublishByEventServiceClient, true);
        }

        private void Unsubscribe()
        {
            _eventAggregator.GetEvent<TAfterSaveEvent>().Unsubscribe(PublishByEventServiceClient);
        }

        protected virtual void DoPublishAction(TModel model)
        {

        }

        /// <summary>
        /// Публикация события через сервис синхронизации
        /// </summary>
        /// <param name="model"></param>
        private void PublishByEventServiceClient(TModel model)
        {
            try
            {
                //если хост есть и он в рабочем состоянии
                if (EventServiceHost != null && 
                    EventServiceHost.State != CommunicationState.Faulted &&
                    EventServiceHost.State != CommunicationState.Closed)
                {
                    ////публикуем действие
                    //PublishEventAction.Invoke(model);
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

        protected abstract Action<TModel> PublishEventAction { get; }

        public void Publish(object model)
        {
            Unsubscribe();
            _eventAggregator.GetEvent<TAfterSaveEvent>().Publish((TModel)model);
            Subscribe();
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