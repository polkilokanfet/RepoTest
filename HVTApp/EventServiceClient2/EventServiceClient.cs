using System;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
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
        private Guid _appSessionId;
        private readonly IUnityContainer _container;
        private readonly Guid _userId = GlobalAppProperties.User.Id;
        /// <summary>
        /// Хост сервиса работает
        /// </summary>
        private bool _hostIsEnabled = false;
        private ServiceReference1.EventServiceClient _eventServiceHost;

        private SyncContainer _syncContainer;

        private readonly EndpointAddress _endpointAddress;
        private readonly NetTcpBinding _netTcpBinding;

        public EventServiceClient(IUnityContainer container)
        {
            _container = container;

            //увеличиваем таймаут бездействия
            _netTcpBinding = new NetTcpBinding(SecurityMode.None, true)
            {
                //SendTimeout = new TimeSpan(7, 0, 0, 0),
                ReceiveTimeout = new TimeSpan(7, 0, 0, 0),
                //OpenTimeout = new TimeSpan(7, 0, 0, 0),
                //CloseTimeout = new TimeSpan(7, 0, 0, 0)
            };

            _endpointAddress = new EndpointAddress(EventServiceAddresses.TcpBaseAddress);
        }

        public void Start()
        {
            Task.Run(
                () =>
                {
                    try
                    {
                        _eventServiceHost = new ServiceReference1.EventServiceClient(new InstanceContext(this), _netTcpBinding, _endpointAddress);
                        _appSessionId = Guid.NewGuid();
                        if (_eventServiceHost.Connect(_appSessionId, _userId))
                        {
                            ConfigureSyncContainer();
                            _hostIsEnabled = true;
                            PingHost();
                        }
                        else
                        {
                            throw new Exception("_eventServiceClient.Connect() вернул false");
                        }
                    }
                    catch (Exception)
                    {
                        this.DisableWaitReStart();
                    }
                }).Await();
        }

        private void Disable()
        {
            _hostIsEnabled = false;

            //сносим старый хост
            if (_eventServiceHost != null)
            {
                _eventServiceHost.Abort();
                _eventServiceHost = null;
            }

            //освобождаем старый контейнер синхронизации
            if (_syncContainer != null)
            {
                _syncContainer.ServiceHostDisabled -= DisableWaitReStart;
                _syncContainer.Dispose();
                _syncContainer = null;
            }
        }

        /// <summary>
        /// Очистить старое, подождать, рестартовать
        /// </summary>
        private void DisableWaitReStart()
        {
            Disable();

            //рестартуем
            Task.Run(
                () =>
                {
                    Thread.Sleep(new TimeSpan(0,0,5,0));
                    this.Start();
                }).Await();
        }

        private void ConfigureSyncContainer()
        {
            _syncContainer = new SyncContainer();

            _syncContainer.Add(new SyncDirectumTask(_container, _eventServiceHost, _appSessionId)); //Задачи из DirectumLite
            _syncContainer.Add(new SyncDirectumTaskStart(_container, _eventServiceHost, _appSessionId)); //Задачи из DirectumLite
            _syncContainer.Add(new SyncDirectumTaskStop(_container, _eventServiceHost, _appSessionId)); //Задачи из DirectumLite
            _syncContainer.Add(new SyncDirectumTaskPerform(_container, _eventServiceHost, _appSessionId)); //Задачи из DirectumLite
            _syncContainer.Add(new SyncDirectumTaskAccept(_container, _eventServiceHost, _appSessionId)); //Задачи из DirectumLite
            _syncContainer.Add(new SyncDirectumTaskReject(_container, _eventServiceHost, _appSessionId)); //Задачи из DirectumLite

            _syncContainer.Add(new SyncTechnicalRequrementsTask(_container, _eventServiceHost, _appSessionId)); //Задачи TCE
            _syncContainer.Add(new SyncTechnicalRequrementsTaskStart(_container, _eventServiceHost, _appSessionId)); //Задачи TCE
            _syncContainer.Add(new SyncTechnicalRequrementsTaskInstruct(_container, _eventServiceHost, _appSessionId)); //Задачи TCE
            _syncContainer.Add(new SyncTechnicalRequrementsTaskCancel(_container, _eventServiceHost, _appSessionId)); //Задачи TCE
            _syncContainer.Add(new SyncTechnicalRequrementsTaskReject(_container, _eventServiceHost, _appSessionId)); //Задачи TCE

            _syncContainer.Add(new SyncPriceCalculation(_container, _eventServiceHost, _appSessionId));       //Калькуляции себестоимости сохранение
            _syncContainer.Add(new SyncPriceCalculationStart(_container, _eventServiceHost, _appSessionId));  //Калькуляции себестоимости старт
            _syncContainer.Add(new SyncPriceCalculationFinish(_container, _eventServiceHost, _appSessionId)); //Калькуляции себестоимости финиш
            _syncContainer.Add(new SyncPriceCalculationCancel(_container, _eventServiceHost, _appSessionId)); //Калькуляции себестоимости остановка

            //_syncContainer.Add(new SyncIncomingRequest(_container, _eventServiceClient, _appSessionId));            //Запросы

            ////Входящие документы
            //_eventAggregator.GetEvent<AfterSaveIncomingDocumentSyncEvent>().Subscribe(document => { SavePublishEvent(
            //    () => _eventServiceClient?.SaveIncomingDocumentPublishEvent(_appSessionId, document.Id)); }, true);

            _syncContainer.ServiceHostDisabled += DisableWaitReStart;
        }

        /// <summary>
        /// Пинг хоста
        /// </summary>
        private void PingHost()
        {
            Task.Run(
                () =>
                {
                    if (_hostIsEnabled == false || _eventServiceHost == null)
                    {
                        return;
                    }

                    try
                    {
                        if (_eventServiceHost.HostIsAlive())
                        {
                            Thread.Sleep(new TimeSpan(0, 0, 2, 0));
                            this.PingHost();
                        }
                    }
                    catch (Exception)
                    {
                        this.DisableWaitReStart();
                    }
                }).Await();
        }

        public void Stop()
        {
            //отключаемся от сервера
            if (_hostIsEnabled 
                && _eventServiceHost != null 
                && _eventServiceHost.State != CommunicationState.Faulted 
                && _eventServiceHost.State != CommunicationState.Closed)
            {
                try
                {
                    _eventServiceHost.Disconnect(_appSessionId);
                }
                catch (TimeoutException e)
                {
                    _container.Resolve<IMessageService>().ShowOkMessageDialog(e.GetType().Name, e.GetAllExceptions());
                }
                catch (Exception e)
                {
                    _container.Resolve<IMessageService>().ShowOkMessageDialog(e.GetType().Name, e.GetAllExceptions());
                }
            }

            this.Disable();
        }

        public void OnServiceDisposeEvent()
        {
            this.DisableWaitReStart();
            //_container.Resolve<IMessageService>().ShowOkMessageDialog("Информация", "Синхронизация прекращена.");
        }

        //действия, когда прилетают события из сервера синхронизации
        #region Service Callback Actions

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
                this._syncContainer.Publish<PriceCalculation, AfterFinishPriceCalculationEvent>(calculation);

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
                this._syncContainer.Publish<PriceCalculation, AfterCancelPriceCalculationEvent>(calculation);

                string message = $"{calculation.Name}";
                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<PriceCalculationView>(new NavigationParameters { { nameof(PriceCalculation), calculation } });
                });
                Popup.Popup.ShowPopup(message, "Остановлен расчет переменных затрат", action);
            }
        }

        #endregion

        #region IncomingRequest

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

        #endregion

        #region TechnicalRequarementsTask

        public void OnSaveTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
        }

        public void OnStartTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            if (GlobalAppProperties.User.RoleCurrent == Role.BackManagerBoss ||
                GlobalAppProperties.User.RoleCurrent == Role.BackManager)
            {
                var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);
                var frontManager = technicalRequrementsTask.GetFrontManager();
                if (frontManager == null) return;

                string message = string.Empty;
                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<TechnicalRequrementsTaskView>(new NavigationParameters { { "technicalRequrementsTask", technicalRequrementsTask } });
                });

                //если текущий пользователь BackManagerBoss
                if (GlobalAppProperties.User.RoleCurrent == Role.BackManagerBoss)
                {
                    if (technicalRequrementsTask.Start.HasValue && technicalRequrementsTask.BackManager == null)
                    {
                        message = $"Поручите кому-нибудь новую задачу ТСЕ (инициатор: {frontManager})";
                    }
                }
                //если текущий пользователь Back-Менеджер
                else if (GlobalAppProperties.User.RoleCurrent == Role.BackManager)
                {
                    if (technicalRequrementsTask.BackManager == null) return;
                    if (technicalRequrementsTask.BackManager.IsAppCurrentUser())
                    {
                        message = $"Рестартована задача (инициатор: {frontManager})";
                    }
                }

                if (message != string.Empty)
                {
                    this._syncContainer.Publish<TechnicalRequrementsTask, AfterSaveTechnicalRequrementsTaskEvent>(technicalRequrementsTask);
                    Popup.Popup.ShowPopup(message, $"Задача в TCE с Id {technicalRequrementsTask.Id}", action);
                }
            }
        }

        public void OnInstructTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            if (GlobalAppProperties.User.RoleCurrent == Role.BackManager)
            {
                var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);
                var frontManager = technicalRequrementsTask.GetFrontManager();

                if (frontManager == null) return;
                if (technicalRequrementsTask.BackManager == null) return;
                if (!technicalRequrementsTask.BackManager.IsAppCurrentUser()) return;

                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<TechnicalRequrementsTaskView>(new NavigationParameters { { "technicalRequrementsTask", technicalRequrementsTask } });
                });

                this._syncContainer.Publish<TechnicalRequrementsTask, AfterSaveTechnicalRequrementsTaskEvent>(technicalRequrementsTask);
                Popup.Popup.ShowPopup($"Вам поручена задача ТСЕ (инициатор: {frontManager})", $"Задача в TCE с Id {technicalRequrementsTask.Id}", action);
            }

            else if (GlobalAppProperties.User.RoleCurrent == Role.SalesManager)
            {
                var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);
                var frontManager = technicalRequrementsTask.GetFrontManager();

                if (frontManager == null) return;
                if (!frontManager.IsAppCurrentUser()) return;
                if (technicalRequrementsTask.BackManager == null) return;

                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<TechnicalRequrementsTaskView>(new NavigationParameters { { "technicalRequrementsTask", technicalRequrementsTask } });
                });

                this._syncContainer.Publish<TechnicalRequrementsTask, AfterSaveTechnicalRequrementsTaskEvent>(technicalRequrementsTask);
                Popup.Popup.ShowPopup($"Ваша задача ТСЕ поручена (back-manager: {technicalRequrementsTask.BackManager})", $"Задача в TCE с Id {technicalRequrementsTask.Id}", action);
            }
        }

        public void OnCancelTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
        }

        public void OnRejectTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            if (GlobalAppProperties.User.RoleCurrent == Role.SalesManager)
            {
                var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);
                var frontManager = technicalRequrementsTask.GetFrontManager();

                if (frontManager == null) return;
                if (!frontManager.IsAppCurrentUser()) return;
                if (technicalRequrementsTask.BackManager == null) return;

                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<TechnicalRequrementsTaskView>(new NavigationParameters { { "technicalRequrementsTask", technicalRequrementsTask } });
                });

                this._syncContainer.Publish<TechnicalRequrementsTask, AfterSaveTechnicalRequrementsTaskEvent>(technicalRequrementsTask);
                Popup.Popup.ShowPopup($"Ваша задача ТСЕ отклонена (back-manager: {technicalRequrementsTask.BackManager})\nПричина отклонения: {technicalRequrementsTask.RejectComment}", $"Задача в TCE с Id {technicalRequrementsTask.Id}", action);
            }
        }

        #endregion

        public bool IsAlive()
        {
            return true;
        }

        #endregion

    }
}
