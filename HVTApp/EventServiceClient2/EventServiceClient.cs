using System;
using System.Linq;
using System.ServiceModel;
using EventServiceClient2.ServiceReference1;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace EventServiceClient2
{
    public class EventServiceClient : ServiceReference1.IEventServiceCallback
    {
        private readonly Guid _appSessionId = Guid.NewGuid();
        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private readonly Guid _userId = GlobalAppProperties.User.Id;
        private ServiceReference1.EventServiceClient _service;

        public EventServiceClient(IUnityContainer container)
        {
            _container = container;
            _eventAggregator = container.Resolve<IEventAggregator>();
        }

        public void Start()
        {
            try
            {
                _service = new ServiceReference1.EventServiceClient(new InstanceContext(this));
                if (_service.Connect(_appSessionId))
                {
                    _eventAggregator.GetEvent<AfterSaveIncomingRequestEvent>().Subscribe(
                        request =>
                        {
                            _service.SaveIncomingRequestPublishEvent(_appSessionId, request.Id);
                        }, true);
                }
            }
            catch (Exception e)
            {
                _container.Resolve<IMessageService>().ShowOkMessageDialog("Exception", e.GetAllExceptions());
            }
        }

        public void Stop()
        {
            if (_service != null && _service.State != CommunicationState.Faulted)
            {
                _service.Disconnect(_appSessionId);
            }
            
        }

        public void OnSaveIncomingRequestPublishEvent(Guid requestId)
        {
            var request = _container.Resolve<IUnitOfWork>().Repository<IncomingRequest>().GetById(requestId);
            
            //если в исполнителях есть текущий пользователь
            if (request.Performers.Select(x => x.Id).Contains(_userId))
                _eventAggregator.GetEvent<AfterSaveIncomingRequestEvent>().Publish(request);
        }
    }
}
