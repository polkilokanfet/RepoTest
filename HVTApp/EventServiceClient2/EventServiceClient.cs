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
using HVTApp.UI;
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

        private readonly EndpointAddress _endpointAddress;
        private readonly NetTcpBinding _netTcpBinding;

        /// <summary>
        /// Хост сервиса работает
        /// </summary>
        private bool HostIsEnabled => EventServiceHost != null && EventServiceHost.State != CommunicationState.Faulted
                                                               && EventServiceHost.State != CommunicationState.Closed;

        private ServiceReference1.EventServiceClient EventServiceHost { get; set; }

        private SyncContainer SyncContainer { get; set; }

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
                        //проверка на то стартован ли уже сервис
                        if (HostIsEnabled)
                            return;

                        //инициализация клиента сервиса
                        EventServiceHost = new ServiceReference1.EventServiceClient(new InstanceContext(this), _netTcpBinding, _endpointAddress);

                        //текущая сессия
                        _appSessionId = Guid.NewGuid();

                        //коннектимся к сервису
                        if (EventServiceHost.Connect(_appSessionId, _userId))
                        {
                            //конфигурация контейнера синхронизации
                            ConfigureSyncContainer();

                            //циклический пинг хоста
                            PingHost();
                        }
                        else
                        {
                            throw new Exception("_eventServiceClient.Connect() вернул false");
                        }
                    }
                    catch (Exception)
                    {
                        //очистить следы от предыдущего подключения, подождать и рестартануть
                        this.DisableWaitRestart();
                    }
                }).Await();
        }

        public void Stop()
        {
            //отключаемся от сервера
            if (HostIsEnabled)
            {
                try
                {
                    EventServiceHost.Disconnect(_appSessionId);
                }
                catch (TimeoutException e)
                {
                    _container.Resolve<IMessageService>().ShowOkMessageDialog(e.GetType().Name, e.PrintAllExceptions());
                }
                catch (Exception e)
                {
                    _container.Resolve<IMessageService>().ShowOkMessageDialog(e.GetType().Name, e.PrintAllExceptions());
                }
            }

            this.Disable();
        }

        /// <summary>
        /// Отключение от хоста
        /// </summary>
        private void Disable()
        {
            //сносим старый хост
            if (EventServiceHost != null)
            {
                EventServiceHost.Abort();
                EventServiceHost = null;
            }

            //освобождаем старый контейнер синхронизации
            if (SyncContainer != null)
            {
                SyncContainer.ServiceHostIsDisabled -= DisableWaitRestart;
                SyncContainer.Dispose();
                SyncContainer = null;
            }
        }

        /// <summary>
        /// Очистить старое, подождать, рестартовать
        /// </summary>
        private void DisableWaitRestart()
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
            SyncContainer = new SyncContainer();

            SyncContainer.Add(new SyncDirectumTask(_container, EventServiceHost, _appSessionId)); //Задачи из DirectumLite
            SyncContainer.Add(new SyncDirectumTaskStart(_container, EventServiceHost, _appSessionId)); //Задачи из DirectumLite
            SyncContainer.Add(new SyncDirectumTaskStop(_container, EventServiceHost, _appSessionId)); //Задачи из DirectumLite
            SyncContainer.Add(new SyncDirectumTaskPerform(_container, EventServiceHost, _appSessionId)); //Задачи из DirectumLite
            SyncContainer.Add(new SyncDirectumTaskAccept(_container, EventServiceHost, _appSessionId)); //Задачи из DirectumLite
            SyncContainer.Add(new SyncDirectumTaskReject(_container, EventServiceHost, _appSessionId)); //Задачи из DirectumLite

            SyncContainer.Add(new SyncTechnicalRequrementsTask(_container, EventServiceHost, _appSessionId)); //Задачи TCE
            SyncContainer.Add(new SyncTechnicalRequrementsTaskStart(_container, EventServiceHost, _appSessionId)); //Задачи TCE
            SyncContainer.Add(new SyncTechnicalRequrementsTaskInstruct(_container, EventServiceHost, _appSessionId)); //Задачи TCE
            SyncContainer.Add(new SyncTechnicalRequrementsTaskCancel(_container, EventServiceHost, _appSessionId)); //Задачи TCE
            SyncContainer.Add(new SyncTechnicalRequrementsTaskReject(_container, EventServiceHost, _appSessionId)); //Задачи TCE
            SyncContainer.Add(new SyncTechnicalRequrementsTaskFinish(_container, EventServiceHost, _appSessionId)); //Задачи TCE
            SyncContainer.Add(new SyncTechnicalRequrementsTaskAccept(_container, EventServiceHost, _appSessionId)); //Задачи TCE

            SyncContainer.Add(new SyncPriceCalculation(_container, EventServiceHost, _appSessionId));       //Калькуляции себестоимости сохранение
            SyncContainer.Add(new SyncPriceCalculationStart(_container, EventServiceHost, _appSessionId));  //Калькуляции себестоимости старт
            SyncContainer.Add(new SyncPriceCalculationFinish(_container, EventServiceHost, _appSessionId)); //Калькуляции себестоимости финиш
            SyncContainer.Add(new SyncPriceCalculationCancel(_container, EventServiceHost, _appSessionId)); //Калькуляции себестоимости остановка

            //_syncContainer.Add(new SyncIncomingRequest(_container, _eventServiceClient, _appSessionId));            //Запросы

            ////Входящие документы
            //_eventAggregator.GetEvent<AfterSaveIncomingDocumentSyncEvent>().Subscribe(document => { SavePublishEvent(
            //    () => _eventServiceClient?.SaveIncomingDocumentPublishEvent(_appSessionId, document.Id)); }, true);

            SyncContainer.ServiceHostIsDisabled += DisableWaitRestart;
        }

        /// <summary>
        /// Пинг хоста (циклический при удаче)
        /// </summary>
        private void PingHost()
        {
            Task.Run(
                () =>
                {
                    if (HostIsEnabled)
                    {
                        try
                        {
                            if (EventServiceHost.HostIsAlive())
                            {
                                Thread.Sleep(new TimeSpan(0, 0, 2, 0));
                                this.PingHost();
                            }
                        }
                        catch (Exception)
                        {
                            this.DisableWaitRestart();
                        }
                    }

                }).Await();
        }

        public void CopyProjectAttachmentsRequest(Guid userId, Guid projectId, string targetDirectory)
        {
            if (HostIsEnabled || EventServiceHost != null)
            {
                EventServiceHost.CopyProjectAttachments(userId, projectId, targetDirectory);
            }
        }

        public bool UserConnected(Guid userId)
        {
            if (HostIsEnabled || EventServiceHost != null)
            {
                try
                {
                    return EventServiceHost.UserIsConnected(userId);
                }
                catch (CommunicationObjectFaultedException)
                {
                    return false;
                }
            }

            return false;
        }
    }
}
