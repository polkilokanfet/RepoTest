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
using HVTApp.UI.Modules.BookRegistration.Views;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;
using HVTApp.UI.Modules.Directum;
using HVTApp.UI.PriceCalculations.View;

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
                    //Задачи
                    _eventAggregator.GetEvent<AfterSaveDirectumTaskSyncEvent>().Subscribe(task => { SavePublishEvent(
                        () => _service.SaveDirectumTaskPublishEvent(_appSessionId, task.Id)); }, true);

                    //Калькуляции себестоимости
                    _eventAggregator.GetEvent<AfterSavePriceCalculationSyncEvent>().Subscribe(priceCalculation => { SavePublishEvent(
                        () => _service.SavePriceCalculationPublishEvent(_appSessionId, priceCalculation.Id)); }, true);

                    //Запросы
                    _eventAggregator.GetEvent<AfterSaveIncomingRequestSyncEvent>().Subscribe(request => { SavePublishEvent(
                        () => _service.SaveIncomingRequestPublishEvent(_appSessionId, request.Id)); }, true);

                    //Входящие документы
                    _eventAggregator.GetEvent<AfterSaveIncomingDocumentSyncEvent>().Subscribe(document => { SavePublishEvent(
                        () => _service.SaveIncomingDocumentPublishEvent(_appSessionId, document.Id)); }, true);
                }
            }
            catch (Exception e)
            {
                var message = $"Не удалось подключиться к сервису синхронизации. Вы можете продолжать работу без синхронизации.\nПопросите разработчика запустить сервис синхронизации.\n\n{e.GetAllExceptions()}";
                _container.Resolve<IMessageService>().ShowOkMessageDialog("Exception", message);
            }
        }

        private void SavePublishEvent(Action publishEvent)
        {
            try
            {
                if (_service != null && _service.State != CommunicationState.Faulted)
                    publishEvent.Invoke();
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

        public void OnSaveDirectumTaskPublishEvent(Guid taskId)
        {
            var task = _container.Resolve<IUnitOfWork>().Repository<DirectumTask>().GetById(taskId);

            var allowAccept =
                task.Group.Author.Id == GlobalAppProperties.User.Id &&
                task.FinishPerformer.HasValue &&
                !task.FinishAuthor.HasValue;

            var allowPerform = 
                task.StartResult.HasValue && 
                task.Performer.Id == GlobalAppProperties.User.Id;

            if (allowPerform || allowAccept)
            {
                _eventAggregator.GetEvent<AfterSaveDirectumTaskEvent>().Publish(task);

                string message = $"Задача от: {task.Group.Author}\nТема: \"{task.Group.Title}\"";
                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<DirectumTaskView>(new NavigationParameters { { "task", task } });
                });
                Popup.Popup.ShowPopup(message, action);
            }
        }

        public void OnSavePriceCalculationPublishEvent(Guid calculationId)
        {
            var calculation = _container.Resolve<IUnitOfWork>().Repository<PriceCalculation>().GetById(calculationId);

            var author = calculation.PriceCalculationItems.FirstOrDefault()?.SalesUnits.FirstOrDefault()?.Project.Manager;
            var canWath = author?.Id == GlobalAppProperties.User.Id && calculation.TaskCloseMoment.HasValue;
            var canCalc = calculation.TaskOpenMoment.HasValue &&
                          (GlobalAppProperties.User.RoleCurrent == Role.Admin ||
                           GlobalAppProperties.User.RoleCurrent == Role.Pricer);

            if (canWath || canCalc)
            {
                _eventAggregator.GetEvent<AfterSavePriceCalculationEvent>().Publish(calculation);

                string message = $"Калькуляция: \"{calculation.Name}\"";
                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<PriceCalculationView>(new NavigationParameters { { nameof(PriceCalculation), calculation } });
                });
                Popup.Popup.ShowPopup(message, action);
            }
        }

        public void OnSaveIncomingRequestPublishEvent(Guid requestId)
        {
            var request = _container.Resolve<IUnitOfWork>().Repository<IncomingRequest>().GetById(requestId);

            var canInstruct = GlobalAppProperties.User.RoleCurrent == Role.Admin ||
                              GlobalAppProperties.User.RoleCurrent == Role.Director;

            var canPerform = GlobalAppProperties.User.RoleCurrent == Role.SalesManager &&
                             request.Performers.Any(x => x.Id == GlobalAppProperties.User.Employee.Id);

            if (canInstruct || canPerform)
            {
                _eventAggregator.GetEvent<AfterSaveIncomingRequestEvent>().Publish(request);

                string message = $"Запрос: \"{request.Document.Comment}\"";
                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<IncomingRequestsView>(new NavigationParameters());
                });
                Popup.Popup.ShowPopup(message, action);
            }
        }

        public void OnSaveIncomingDocumentPublishEvent(Guid documentId)
        {
            var unitOfWork = _container.Resolve<IUnitOfWork>();
            var document = unitOfWork.Repository<Document>().GetById(documentId);

            var canInstruct = (GlobalAppProperties.User.RoleCurrent == Role.Admin ||
                              GlobalAppProperties.User.RoleCurrent == Role.Director) &&
                              document.RecipientEmployee.Company.Id == GlobalAppProperties.Actual.OurCompany?.Id;

            if (canInstruct)
            {
                var request = new IncomingRequest {Document = document};
                _eventAggregator.GetEvent<AfterSaveIncomingRequestEvent>().Publish(request);

                string message = $"Запрос: \"{document.Comment}\"";
                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<IncomingRequestView>(
                        new NavigationParameters
                        {
                            {"Model", request},
                            {"UnitOfWork", unitOfWork}
                        });

                });
                Popup.Popup.ShowPopup(message, action);
            }
        }
    }
}
