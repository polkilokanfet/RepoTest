using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.EventService;

namespace EventService
{
    public partial class EventService : IEventService
    {
        /// <summary>
        /// Публикация события через сервис синхронизации
        /// </summary>
        /// <param name="appSessionId">Id приложения инициировшего событие</param>
        /// <param name="publishEvent"></param>
        private void PublishEventThroughService(Guid appSessionId, Action<AppSession> publishEvent)
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
                    PrintMessageEvent?.Invoke($"{this.GetType().FullName}. {e.GetType().FullName}.");
                    this.Disconnect(appSession.AppSessionId);
                }
                catch (TimeoutException e)
                {
                    PrintMessageEvent?.Invoke($"{this.GetType().FullName}. {e.GetType().FullName}.");
                    this.Disconnect(appSession.AppSessionId);
                }
                catch (Exception e)
                {
                    PrintMessageEvent?.Invoke($"!Exception on Invoke {publishEvent.GetMethodInfo().Name} ({this.GetType().FullName}) by appSession {appSessionId}. \n{e.GetType().FullName}\n{e.PrintAllExceptions()}");
                    this.Disconnect(appSession.AppSessionId);
                }
            }

            PrintMessageEvent?.Invoke($"Invoke {publishEvent.GetMethodInfo().Name} by appSession {appSessionId}");
        }

        /// <summary>
        /// Публикация события через сервис синхронизации для целевых
        /// </summary>
        /// <param name="targetUserId">Id целевого пользователя</param>
        /// <param name="sourceEventAppSessionId">Id приложения инициировшего событие</param>
        /// <param name="publishEvent"></param>
        /// <returns>Доставлено ли уведомление целевому пользователю</returns>
        private bool PublishEventByServiceForUser(Guid targetUserId, Guid sourceEventAppSessionId, Func<AppSession, bool> publishEvent)
        {
            bool result = false;

            //целевые приложения (без того, которое и послало событие).
            var targetAppSessions = _appSessions
                .Where(appSession => appSession.UserId == targetUserId)
                .Where(appSession => appSession.AppSessionId != sourceEventAppSessionId)
                .ToList();

            if (targetAppSessions.Any() == false)
                return false;

            foreach (var appSession in targetAppSessions)
            {
                try
                {
                    if (publishEvent.Invoke(appSession))
                        result = true;
                }
                //отключаем приложение от сервиса
                catch (CommunicationObjectAbortedException e)
                {
                    PrintMessageEvent?.Invoke($"{this.GetType().FullName}. {e.GetType().FullName}.");
                    this.Disconnect(appSession.AppSessionId);
                }
                catch (TimeoutException e)
                {
                    PrintMessageEvent?.Invoke($"{this.GetType().FullName}. {e.GetType().FullName}.");
                    this.Disconnect(appSession.AppSessionId);
                }
                catch (Exception e)
                {
                    PrintMessageEvent?.Invoke($"!Exception on Invoke {publishEvent.GetMethodInfo().Name} ({this.GetType().FullName}) by appSession {sourceEventAppSessionId}. \n{e.GetType().FullName}\n{e.PrintAllExceptions()}");
                    this.Disconnect(appSession.AppSessionId);
                }
            }

            PrintMessageEvent?.Invoke($"Invoke {publishEvent.GetMethodInfo().Name} by appSession {sourceEventAppSessionId}");
            return result;
        }


        #region IncomingRequest

        public void SaveIncomingRequestPublishEvent(Guid appSessionId, Guid requestId)
        {
            PublishEventThroughService(appSessionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSaveIncomingRequestServiceCallback(requestId));
        }

        #endregion

        #region Directum

        public bool SaveDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid taskId)
        {
            return PublishEventByServiceForUser(targetUserId, eventSourceAppSessionId, 
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSaveDirectumTaskServiceCallback(taskId));
        }

        public bool StartDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid taskId)
        {
            return PublishEventByServiceForUser(targetUserId, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnStartDirectumTaskServiceCallback(taskId));
        }

        public bool StopDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid taskId)
        {
            return PublishEventByServiceForUser(targetUserId, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnStopDirectumTaskServiceCallback(taskId));
        }

        public bool PerformDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid taskId)
        {
            return PublishEventByServiceForUser(targetUserId, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnPerformDirectumTaskServiceCallback(taskId));
        }

        public bool AcceptDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid taskId)
        {
            return PublishEventByServiceForUser(targetUserId, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnAcceptDirectumTaskServiceCallback(taskId));
        }

        public bool RejectDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid taskId)
        {
            return PublishEventByServiceForUser(targetUserId, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnRejectDirectumTaskServiceCallback(taskId));
        }
        #endregion

        #region PriceCalculation
        
        public bool SavePriceCalculationPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid priceCalculationId)
        {
            return PublishEventByServiceForUser(targetUserId, eventSourceAppSessionId, 
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSavePriceCalculationServiceCallback(priceCalculationId));
        }

        public bool StartPriceCalculationPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid priceCalculationId)
        {
            return PublishEventByServiceForUser(targetUserId, eventSourceAppSessionId, 
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnStartPriceCalculationServiceCallback(priceCalculationId));
        }

        public bool FinishPriceCalculationPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid priceCalculationId)
        {
            return PublishEventByServiceForUser(targetUserId, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnFinishPriceCalculationServiceCallback(priceCalculationId));
        }

        public bool CancelPriceCalculationPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid priceCalculationId)
        {
            return PublishEventByServiceForUser(targetUserId, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnCancelPriceCalculationServiceCallback(priceCalculationId));
        }

        public bool RejectPriceCalculationPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid priceCalculationId)
        {
            return PublishEventByServiceForUser(targetUserId, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnRejectPriceCalculationServiceCallback(priceCalculationId));
        }

        #endregion

        public bool SaveIncomingRequestPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid requestId)
        {
            throw new NotImplementedException();
        }

        public bool SaveIncomingDocumentPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid requestId)
        {
            throw new NotImplementedException();
        }


        #region IncomingDocument

        public void SaveIncomingDocumentPublishEvent(Guid appSessionId, Guid documentId)
        {
            PublishEventThroughService(appSessionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSaveIncomingDocumentServiceCallback(documentId));
        }

        #endregion

        #region TechnicalRequarementsTask

        public bool SaveTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid technicalRequarementsTaskId)
        {
            return PublishEventByServiceForUser(targetUserId, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSaveTechnicalRequarementsTaskServiceCallback(technicalRequarementsTaskId));
        }

        public bool StartTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid technicalRequarementsTaskId)
        {
            return PublishEventByServiceForUser(targetUserId, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnStartTechnicalRequarementsTaskServiceCallback(technicalRequarementsTaskId));
        }

        public bool InstructTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid technicalRequarementsTaskId)
        {
            return PublishEventByServiceForUser(targetUserId, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnInstructTechnicalRequarementsTaskServiceCallback(technicalRequarementsTaskId));
        }

        public bool StopTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid technicalRequarementsTaskId)
        {
            return PublishEventByServiceForUser(targetUserId, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnStopTechnicalRequarementsTaskServiceCallback(technicalRequarementsTaskId));
        }

        public bool RejectTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid technicalRequarementsTaskId)
        {
            return PublishEventByServiceForUser(targetUserId, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnRejectTechnicalRequarementsTaskServiceCallback(technicalRequarementsTaskId));
        }

        public bool RejectByFrontManagerTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid technicalRequarementsTaskId)
        {
            return PublishEventByServiceForUser(targetUserId, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnRejectByFrontManagerTechnicalRequarementsTaskServiceCallback(technicalRequarementsTaskId));
        }

        public bool FinishTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid technicalRequarementsTaskId)
        {
            return PublishEventByServiceForUser(targetUserId, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnFinishTechnicalRequarementsTaskServiceCallback(technicalRequarementsTaskId));
        }

        public bool AcceptTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid technicalRequarementsTaskId)
        {
            return PublishEventByServiceForUser(targetUserId, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnAcceptTechnicalRequarementsTaskServiceCallback(technicalRequarementsTaskId));
        }

        #endregion
    }
}
