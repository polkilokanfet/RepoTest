using System;
using System.ServiceModel;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace EventServiceClient2.SyncEntities
{
    public abstract class Sync<TModel, TAfterSaveEvent> : ISync
        where TAfterSaveEvent : PubSubEvent<TModel>, new()
        where TModel : BaseEntity
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageService _messageService;
        protected readonly ServiceReference1.EventServiceClient EventServiceClient;
        protected readonly Guid AppSessionId;

        public Type ModelType => typeof(TModel);
        public Type EventType => typeof(TAfterSaveEvent);

        protected Sync(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceClient, Guid appSessionId)
        {
            _eventAggregator = container.Resolve<IEventAggregator>();
            _messageService = container.Resolve<IMessageService>();
            EventServiceClient = eventServiceClient;
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

        /// <summary>
        /// Публикация события через сервис синхронизации
        /// </summary>
        /// <param name="model"></param>
        private void PublishByEventServiceClient(TModel model)
        {
            try
            {
                if (EventServiceClient != null && EventServiceClient.State != CommunicationState.Faulted)
                {
                    PublishEventAction.Invoke(model);
                }
            }
            catch (Exception e)
            {
                _messageService.ShowOkMessageDialog(e.GetType().FullName, e.GetAllExceptions());
            }
        }

        protected abstract Action<TModel> PublishEventAction { get; }

        public void Publish(object model)
        {
            Unsubscribe();
            _eventAggregator.GetEvent<TAfterSaveEvent>().Publish((TModel)model);
            Subscribe();
        }

        public void Dispose()
        {
            Unsubscribe();
        }
    }
}