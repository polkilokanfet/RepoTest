using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.EventService;

namespace EventService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    public class EventService : IEventService
    {
        /// <summary>
        /// Список приложений, подключенных в настоящий момент к сервису
        /// </summary>
        private readonly List<AppSession> _appSessions = new List<AppSession>();

        public event Action<string> PrintMessageEvent;

        /// <summary>
        /// Подключение к сервису
        /// </summary>
        /// <param name="appSessionId">Id сессии приложения</param>
        /// <param name="userId">Id юзера</param>
        /// <returns></returns>
        public bool Connect(Guid appSessionId, Guid userId)
        {
            //если приложение уже подключено к сервису
            if (_appSessions.Select(appSession => appSession.AppSessionId).Contains(appSessionId))
                return false;

            //подключаем новое приложение к сервису
            _appSessions.Add(new AppSession(appSessionId, userId, OperationContext.Current));
            PrintMessageEvent?.Invoke($"Connected appSession {appSessionId}.");
            return true;
        }

        /// <summary>
        /// Отключение от сервиса
        /// </summary>
        /// <param name="appSessionId">Id сессии приложения</param>
        public void Disconnect(Guid appSessionId)
        {
            var appSession = _appSessions.SingleOrDefault(session => session.AppSessionId == appSessionId);
            if (appSession != null)
            {
                _appSessions.Remove(appSession);
                PrintMessageEvent?.Invoke($"Disconnected appSession {appSessionId}.");
            }
        }

        #region PublishEventsByService

        #region IncomingRequest

        public void SaveIncomingRequestPublishEvent(Guid appSessionId, Guid requestId)
        {
            PublishEventByService(appSessionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSaveIncomingRequestServiceCallback(requestId));
        }

        #endregion

        #region Directum

        public void SaveDirectumTaskPublishEvent(Guid appSessionId, Guid taskId)
        {
            PublishEventByService(appSessionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSaveDirectumTaskServiceCallback(taskId));
        }

        public void StartDirectumTaskPublishEvent(Guid appSessionId, Guid taskId)
        {
            PublishEventByService(appSessionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnStartDirectumTaskServiceCallback(taskId));
        }

        public void StopDirectumTaskPublishEvent(Guid appSessionId, Guid taskId)
        {
            PublishEventByService(appSessionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnStopDirectumTaskServiceCallback(taskId));
        }

        public void PerformDirectumTaskPublishEvent(Guid appSessionId, Guid taskId)
        {
            PublishEventByService(appSessionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnPerformDirectumTaskServiceCallback(taskId));
        }

        public void AcceptDirectumTaskPublishEvent(Guid appSessionId, Guid taskId)
        {
            PublishEventByService(appSessionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnAcceptDirectumTaskServiceCallback(taskId));
        }

        public void RejectDirectumTaskPublishEvent(Guid appSessionId, Guid taskId)
        {
            PublishEventByService(appSessionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnRejectDirectumTaskServiceCallback(taskId));
        }

        #endregion

        #region PriceCalculation

        public void SavePriceCalculationPublishEvent(Guid appSessionId, Guid priceCalculationId)
        {
            PublishEventByService(appSessionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSavePriceCalculationServiceCallback(priceCalculationId));
        }

        public void StartPriceCalculationPublishEvent(Guid appSessionId, Guid priceCalculationId)
        {
            PublishEventByService(appSessionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnStartPriceCalculationServiceCallback(priceCalculationId));
        }

        public void FinishPriceCalculationPublishEvent(Guid appSessionId, Guid priceCalculationId)
        {
            PublishEventByService(appSessionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnFinishPriceCalculationServiceCallback(priceCalculationId));
        }
        public void CancelPriceCalculationPublishEvent(Guid appSessionId, Guid priceCalculationId)
        {
            PublishEventByService(appSessionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnCancelPriceCalculationServiceCallback(priceCalculationId));
        }

        #endregion

        #region IncomingDocument

        public void SaveIncomingDocumentPublishEvent(Guid appSessionId, Guid documentId)
        {
            PublishEventByService(appSessionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSaveIncomingDocumentServiceCallback(documentId));
        }

        #endregion

        #region TechnicalRequarementsTask

        public void SaveTechnicalRequarementsTaskPublishEvent(Guid appSessionId, Guid technicalRequarementsTaskId)
        {
            PublishEventByService(appSessionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSaveTechnicalRequarementsTaskServiceCallback(technicalRequarementsTaskId));
        }

        public void StartTechnicalRequarementsTaskPublishEvent(Guid appSessionId, Guid technicalRequarementsTaskId)
        {
            PublishEventByService(appSessionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnStartTechnicalRequarementsTaskServiceCallback(technicalRequarementsTaskId));
        }

        public void InstructTechnicalRequarementsTaskPublishEvent(Guid appSessionId, Guid technicalRequarementsTaskId)
        {
            PublishEventByService(appSessionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnInstructTechnicalRequarementsTaskServiceCallback(technicalRequarementsTaskId));
        }

        public void CancelTechnicalRequarementsTaskPublishEvent(Guid appSessionId, Guid technicalRequarementsTaskId)
        {
            PublishEventByService(appSessionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnCancelTechnicalRequarementsTaskServiceCallback(technicalRequarementsTaskId));
        }

        public void RejectTechnicalRequarementsTaskPublishEvent(Guid appSessionId, Guid technicalRequarementsTaskId)
        {
            PublishEventByService(appSessionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnRejectTechnicalRequarementsTaskServiceCallback(technicalRequarementsTaskId));
        }

        #endregion

        /// <summary>
        /// Публикация события через сервис синхронизации
        /// </summary>
        /// <param name="appSessionId">Id приложения инициировшего событие</param>
        /// <param name="publishEvent"></param>
        private void PublishEventByService(Guid appSessionId, Action<AppSession> publishEvent)
        {
            //целевые приложения (приложения без того, которое и послало событие).
            var targetAppSessions = _appSessions
                .Where(appSession => appSession.AppSessionId != appSessionId)
                .ToList();

            foreach (var appSession in targetAppSessions)
            {
                try
                {
                    publishEvent.Invoke(appSession);
                }
                //отключаем приложение от сервиса
                catch (CommunicationObjectAbortedException e)
                {
                    PrintMessageEvent?.Invoke($"{e.GetType().FullName}.");
                    this.Disconnect(appSession.AppSessionId);
                }
                catch (TimeoutException e)
                {
                    PrintMessageEvent?.Invoke($"{e.GetType().FullName}.");
                    this.Disconnect(appSession.AppSessionId);
                }
                catch (Exception e)
                {
                    PrintMessageEvent?.Invoke($"!Exception on Invoke {publishEvent.GetMethodInfo().Name} by appSession {appSessionId}. \n{e.GetType().FullName}\n{e.GetAllExceptions()}");
                    this.Disconnect(appSession.AppSessionId);
                }
            }

            PrintMessageEvent?.Invoke($"Invoke {publishEvent.GetMethodInfo().Name} by appSession {appSessionId}");
        }

        #endregion

        public void Close()
        {
            foreach (var appSession in _appSessions)
            {
                try
                {
                    appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnServiceDisposeEvent();
                    PrintMessageEvent?.Invoke($"Succsess on Close() {appSession}.");
                }
                catch (Exception e)
                {
                    PrintMessageEvent?.Invoke($"Exception on Close() {appSession}. {e.GetType().FullName} \n {e.GetAllExceptions()}");
                }
            }
        }



        public void SendMessageToChat(Guid authorId, string message)
        {
            foreach (var appSession in _appSessions.Where(x => x.UserId != authorId).ToList())
            {
                try
                {
                    appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSendMessageToChat(authorId, message);
                }
                catch (TimeoutException timeoutException)
                {
                    _appSessions.Remove(appSession);
                }
                catch (Exception e)
                {
                }
            }
        }

        public void SendMessageToUser(Guid authorId, Guid recipientId, string message)
        {
            foreach (var appSession in _appSessions.Where(x => x.UserId == recipientId).ToList())
            {
                try
                {
                    appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSendMessageToUser(authorId, message);
                }
                catch (TimeoutException timeoutException)
                {
                    _appSessions.Remove(appSession);
                }
                catch (Exception e)
                {
                }
            }
        }
    }
}
