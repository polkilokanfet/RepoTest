using System;
using System.Linq;
using System.ServiceModel;
using EventServiceClient2.SyncEntities;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.BookRegistration.Views;
using HVTApp.UI.Modules.Directum;
using HVTApp.UI.PriceCalculations.View;
using HVTApp.UI.TechnicalRequrementsTasksModule;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace EventServiceClient2
{
    public partial class EventServiceClient : IEventServiceClient, EventServiceClient2.ServiceReference1.IEventServiceCallback
    {
        private readonly Guid _appSessionId = Guid.NewGuid();
        private readonly IUnityContainer _container;
        private readonly Guid _userId = GlobalAppProperties.User.Id;
        /// <summary>
        /// Сервис работает
        /// </summary>
        private bool _isEnabled = false;
        private ServiceReference1.EventServiceClient _eventServiceClient;

        private SyncContainer _syncContainer;

        public EventServiceClient(IUnityContainer container)
        {
            _container = container;
        }

        public void Start()
        {
            try
            {
                var binding = new NetTcpBinding(SecurityMode.None, true);
                Uri tcpBaseAddress = EventServiceAddresses.TcpBaseAddress;
                _eventServiceClient = new ServiceReference1.EventServiceClient(new InstanceContext(this), binding, new EndpointAddress(tcpBaseAddress));
                _syncContainer = new SyncContainer();

                if (_eventServiceClient.Connect(_appSessionId, _userId))
                {
                    _syncContainer.Add(new SyncDirectumTask(_container, _eventServiceClient, _appSessionId));               //Задачи из DirectumLite
                    _syncContainer.Add(new SyncDirectumTaskStart(_container, _eventServiceClient, _appSessionId));          //Задачи из DirectumLite
                    _syncContainer.Add(new SyncDirectumTaskStop(_container, _eventServiceClient, _appSessionId));           //Задачи из DirectumLite
                    _syncContainer.Add(new SyncDirectumTaskPerform(_container, _eventServiceClient, _appSessionId));        //Задачи из DirectumLite
                    _syncContainer.Add(new SyncDirectumTaskAccept(_container, _eventServiceClient, _appSessionId));         //Задачи из DirectumLite
                    _syncContainer.Add(new SyncDirectumTaskReject(_container, _eventServiceClient, _appSessionId));         //Задачи из DirectumLite
                    _syncContainer.Add(new SyncTechnicalRequrementsTask(_container, _eventServiceClient, _appSessionId));   //Задачи TCE
                    _syncContainer.Add(new SyncPriceCalculation(_container, _eventServiceClient, _appSessionId));           //Калькуляции себестоимости сохранение
                    _syncContainer.Add(new SyncPriceCalculationStart(_container, _eventServiceClient, _appSessionId));      //Калькуляции себестоимости старт
                    _syncContainer.Add(new SyncPriceCalculationFinish(_container, _eventServiceClient, _appSessionId));     //Калькуляции себестоимости финиш
                    _syncContainer.Add(new SyncPriceCalculationCancel(_container, _eventServiceClient, _appSessionId));     //Калькуляции себестоимости остановка
                    _syncContainer.Add(new SyncIncomingRequest(_container, _eventServiceClient, _appSessionId));            //Запросы

                    ////Входящие документы
                    //_eventAggregator.GetEvent<AfterSaveIncomingDocumentSyncEvent>().Subscribe(document => { SavePublishEvent(
                    //    () => _eventServiceClient?.SaveIncomingDocumentPublishEvent(_appSessionId, document.Id)); }, true);

                    _isEnabled = true;
                }
            }
            catch (Exception e)
            {
                var message = $"Не удалось подключиться к сервису синхронизации. Вы можете продолжать работу без синхронизации.\nПопросите разработчика запустить сервис синхронизации.\n\n{e.GetAllExceptions()}";
                _container.Resolve<IMessageService>().ShowOkMessageDialog(e.GetType().Name, message);
            }
        }

        public void Stop()
        {
            //отключаемся от сервера
            if (_isEnabled 
                && _eventServiceClient != null 
                && _eventServiceClient.State != CommunicationState.Faulted 
                && _eventServiceClient.State != CommunicationState.Closed)
            {
                try
                {
                    _eventServiceClient.Disconnect(_appSessionId);
                }
                catch (TimeoutException e)
                {
                    _container.Resolve<IMessageService>().ShowOkMessageDialog(e.GetType().Name, e.GetAllExceptions());
                }
            }

            //чистим контейнер
            _syncContainer?.Dispose();
            _isEnabled = false;
        }

        public void OnServiceDisposeEvent()
        {
            //чистим контейнер
            _syncContainer?.Dispose();
            _isEnabled = false;

            _container.Resolve<IMessageService>().ShowOkMessageDialog("Информация", "Синхронизация прекращена.");
        }

        //действия, когда прилетают события из сервера синхронизации
        #region ServiceCallback

        #region Directum

        public void OnSaveDirectumTaskServiceCallback(Guid taskId)
        {
        }

        public void OnStartDirectumTaskServiceCallback(Guid taskId)
        {
            var directumTask = _container.Resolve<IUnitOfWork>().Repository<DirectumTask>().GetById(taskId);

            //можно ли исполнять
            var allowPerform = directumTask.StartResult.HasValue && directumTask.Performer != null && directumTask.Performer.IsAppCurrentUser();
            if (allowPerform)
            {
                this._syncContainer.Publish<DirectumTask, AfterSaveDirectumTaskEvent>(directumTask);

                string title = "Вам поручена задача в DirectumLite";
                string message = $"Инициатор: {directumTask.Group.Author}\nТема: \"{directumTask.Group.Title}\"";

                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<DirectumTaskView>(new NavigationParameters { { "task", directumTask } });
                });

                Popup.Popup.ShowPopup(message, title, action);
            }
        }

        public void OnStopDirectumTaskServiceCallback(Guid taskId)
        {
            var directumTask = _container.Resolve<IUnitOfWork>().Repository<DirectumTask>().GetById(taskId);

            //можно ли исполнять
            var isPerformer = directumTask.Performer != null && directumTask.Performer.IsAppCurrentUser();
            if (isPerformer)
            {
                this._syncContainer.Publish<DirectumTask, AfterSaveDirectumTaskEvent>(directumTask);

                string title = "Остановлена задача в DirectumLite";
                string message = $"Инициатор: {directumTask.Group.Author}\nТема: \"{directumTask.Group.Title}\"";

                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<DirectumTaskView>(new NavigationParameters { { "task", directumTask } });
                });

                Popup.Popup.ShowPopup(message, title, action);
            }

        }

        public void OnPerformDirectumTaskServiceCallback(Guid taskId)
        {
            var directumTask = _container.Resolve<IUnitOfWork>().Repository<DirectumTask>().GetById(taskId);

            //можно ли принимать
            var isAuthor = directumTask.Group.Author.IsAppCurrentUser();

            if (isAuthor)
            {
                this._syncContainer.Publish<DirectumTask, AfterSaveDirectumTaskEvent>(directumTask);

                string title = "Выполнена задача в DirectumLite";
                string message = $"Исполнитель: {directumTask.Performer}\nТема: \"{directumTask.Group.Title}\"";

                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<DirectumTaskView>(new NavigationParameters { { "task", directumTask } });
                });

                Popup.Popup.ShowPopup(message, title, action);
            }
        }

        public void OnAcceptDirectumTaskServiceCallback(Guid taskId)
        {
            var directumTask = _container.Resolve<IUnitOfWork>().Repository<DirectumTask>().GetById(taskId);

            //можно ли исполнять
            var allowPerform = directumTask.StartResult.HasValue && directumTask.Performer != null && directumTask.Performer.IsAppCurrentUser();
            if (allowPerform)
            {
                this._syncContainer.Publish<DirectumTask, AfterSaveDirectumTaskEvent>(directumTask);

                string title = "Принята задача в DirectumLite";
                string message = $"Инициатор: {directumTask.Group.Author}\nТема: \"{directumTask.Group.Title}\"";

                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<DirectumTaskView>(new NavigationParameters { { "task", directumTask } });
                });

                Popup.Popup.ShowPopup(message, title, action);
            }
        }

        public void OnRejectDirectumTaskServiceCallback(Guid taskId)
        {

            var directumTask = _container.Resolve<IUnitOfWork>().Repository<DirectumTask>().GetById(taskId);

            //можно ли исполнять
            var allowPerform = directumTask.StartResult.HasValue && directumTask.Performer != null && directumTask.Performer.IsAppCurrentUser();
            if (allowPerform)
            {
                this._syncContainer.Publish<DirectumTask, AfterSaveDirectumTaskEvent>(directumTask);

                string title = "Не принята задача в DirectumLite";
                string message = $"Инициатор: {directumTask.Group.Author}\nТема: \"{directumTask.Group.Title}\"";

                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<DirectumTaskView>(new NavigationParameters { { "task", directumTask } });
                });

                Popup.Popup.ShowPopup(message, title, action);
            }
        }

        #endregion

        #region PriceCalculation

        public void OnSavePriceCalculationServiceCallback(Guid calculationId)
        {
            var calculation = _container.Resolve<IUnitOfWork>().Repository<PriceCalculation>().GetById(calculationId);

            var frontManager = calculation.GetFrontManager();
            var isProjectManager = frontManager != null && frontManager.IsAppCurrentUser();
            var isInitiator = calculation.Initiator != null && calculation.Initiator.IsAppCurrentUser();

            if (isProjectManager || isInitiator || GlobalAppProperties.User.RoleCurrent == Role.Pricer)
            {
                this._syncContainer.Publish<PriceCalculation, AfterSavePriceCalculationEvent>(calculation);
            }
        }

        /// <summary>
        /// Реакция на старт расчета ПЗ
        /// </summary>
        /// <param name="calculationId"></param>
        public void OnStartPriceCalculationServiceCallback(Guid calculationId)
        {
            var calculation = _container.Resolve<IUnitOfWork>().Repository<PriceCalculation>().GetById(calculationId);

            if (GlobalAppProperties.User.RoleCurrent == Role.Pricer)
            {
                this._syncContainer.Publish<PriceCalculation, AfterSavePriceCalculationEvent>(calculation);

                string message = $"{calculation.Name}";
                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<PriceCalculationView>(new NavigationParameters { { nameof(PriceCalculation), calculation } });
                });
                Popup.Popup.ShowPopup(message, "Запущен расчет переменных затрат", action);
            }
        }

        /// <summary>
        /// Реакция на завершение расчета ПЗ
        /// </summary>
        /// <param name="calculationId"></param>
        public void OnFinishPriceCalculationServiceCallback(Guid calculationId)
        {
            var calculation = _container.Resolve<IUnitOfWork>().Repository<PriceCalculation>().GetById(calculationId);

            var frontManager = calculation.GetFrontManager();
            var isProjectManager = frontManager != null && frontManager.IsAppCurrentUser();
            var isInitiator = calculation.Initiator.IsAppCurrentUser();

            if (isProjectManager || isInitiator)
            {
                this._syncContainer.Publish<PriceCalculation, AfterSavePriceCalculationEvent>(calculation);

                string message = $"{calculation.Name}";
                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<PriceCalculationView>(new NavigationParameters { { nameof(PriceCalculation), calculation } });
                });
                Popup.Popup.ShowPopup(message, "Завершен расчет переменных затрат", action);
            }
        }

        /// <summary>
        /// Реакция на остановку расчета ПЗ
        /// </summary>
        /// <param name="calculationId"></param>
        public void OnCancelPriceCalculationServiceCallback(Guid calculationId)
        {
            var calculation = _container.Resolve<IUnitOfWork>().Repository<PriceCalculation>().GetById(calculationId);

            if (GlobalAppProperties.User.RoleCurrent == Role.Pricer)
            {
                this._syncContainer.Publish<PriceCalculation, AfterSavePriceCalculationEvent>(calculation);

                string message = $"{calculation.Name}";
                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<PriceCalculationView>(new NavigationParameters { { nameof(PriceCalculation), calculation } });
                });
                Popup.Popup.ShowPopup(message, "Остановлен расчет переменных затрат", action);
            }
        }

        #endregion

        public void OnSaveIncomingRequestServiceCallback(Guid requestId)
        {
            var request = _container.Resolve<IUnitOfWork>().Repository<IncomingRequest>().GetById(requestId);

            var canInstruct = GlobalAppProperties.User.RoleCurrent == Role.Admin || GlobalAppProperties.User.RoleCurrent == Role.Director;

            var canPerform = GlobalAppProperties.User.RoleCurrent == Role.SalesManager &&
                             request.Performers.Any(employee => employee.Id == GlobalAppProperties.User.Employee.Id);

            if (canInstruct || canPerform)
            {
                this._syncContainer.Publish<IncomingRequest, AfterSaveIncomingRequestEvent>(request);

                string message = $"{request.Document.Comment}";
                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<IncomingRequestsView>(new NavigationParameters());
                });
                Popup.Popup.ShowPopup(message, "Запрос", action);
            }
        }

        public void OnSaveIncomingDocumentServiceCallback(Guid documentId)
        {
            var unitOfWork = _container.Resolve<IUnitOfWork>();
            var document = unitOfWork.Repository<Document>().GetById(documentId);

            var canInstruct = (GlobalAppProperties.User.RoleCurrent == Role.Admin ||
                              GlobalAppProperties.User.RoleCurrent == Role.Director) &&
                              document.RecipientEmployee.Company.Id == GlobalAppProperties.Actual.OurCompany?.Id;

            if (canInstruct)
            {
                var request = new IncomingRequest { Document = document };
                this._syncContainer.Publish<IncomingRequest, AfterSaveIncomingRequestEvent>(request);

                string message = $"{document.Comment}";
                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<IncomingRequestView>(
                        new NavigationParameters
                        {
                            {"Model", request},
                            {"UnitOfWork", unitOfWork}
                        });

                });
                Popup.Popup.ShowPopup(message, "Запрос", action);
            }
        }

        public void OnSaveTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);
            var frontManager = technicalRequrementsTask.GetFrontManager();

            if (frontManager == null) return;

            string message = string.Empty;
            var action = new Action(() =>
            {
                _container.Resolve<IRegionManager>().RequestNavigateContentRegion<TechnicalRequrementsTaskView>(new NavigationParameters { { "technicalRequrementsTask", technicalRequrementsTask } });
            });

            //если текущий пользователь Front-Менеджер
            if (GlobalAppProperties.User.RoleCurrent == Role.SalesManager)
            {
                if (!frontManager.IsAppCurrentUser()) return;
                message = "Изменения в задаче";
            }

            //если текущий пользователь Back-Менеджер
            else if (GlobalAppProperties.User.RoleCurrent == Role.BackManager)
            {
                if (technicalRequrementsTask.BackManager == null) return;
                if (technicalRequrementsTask.BackManager.IsAppCurrentUser())
                {
                    if (technicalRequrementsTask.Start.HasValue)
                    {
                        if (technicalRequrementsTask.LastOpenBackManagerMoment.HasValue)
                        {
                            if (technicalRequrementsTask.Start.Value > technicalRequrementsTask.LastOpenBackManagerMoment.Value)
                            {
                                message = $"Задача изменена с момента последнего её просмотра (инициатор: {frontManager})";
                            }
                        }
                        else
                        {
                            message = $"Вам поручена новая задача (инициатор: {frontManager})";
                        }
                    }
                    else
                    {
                        message = $"Задача остановлена (инициатор: {frontManager})";
                    }
                }
            }

            //если текущий пользователь BackManagerBoss
            else if (GlobalAppProperties.User.RoleCurrent == Role.BackManagerBoss)
            {
                if (technicalRequrementsTask.Start.HasValue && technicalRequrementsTask.BackManager == null)
                {
                    message = $"Создана новая задача. Её необходимо кому-то поручить (инициатор: {frontManager})";
                }
            }

            if (message != string.Empty)
            {
                this._syncContainer.Publish<TechnicalRequrementsTask, AfterSaveTechnicalRequrementsTaskEvent>(technicalRequrementsTask);
                Popup.Popup.ShowPopup(message, $"Задача в TCE с Id {technicalRequrementsTask.Id}", action);
            }
        }

        #endregion

    }
}
