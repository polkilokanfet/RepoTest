﻿using System;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
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
        /// <param name="targetRole">Роль целевого пользователя</param>
        /// <param name="sourceEventAppSessionId">Id приложения инициировшего событие</param>
        /// <param name="publishEvent"></param>
        /// <returns>Доставлено ли уведомление целевому пользователю</returns>
        private bool PublishEventByServiceForUser(Guid targetUserId, Role targetRole, Guid sourceEventAppSessionId, Func<AppSession, bool> publishEvent)
        {
            bool result = false;

            //целевые приложения (без того, которое и послало событие).
            var targetAppSessions = _appSessions
                .Where(appSession => appSession.UserId == targetUserId)
                .Where(appSession => appSession.UserRole == targetRole)
                .Where(appSession => appSession.AppSessionId != sourceEventAppSessionId)
                .ToList();

            PrintMessageEvent?.Invoke("-------------------");
            PrintMessageEvent?.Invoke($"Invoke {publishEvent.GetMethodInfo().Name} (sourceEventAppSessionId: {sourceEventAppSessionId} targetUserId: {targetUserId} targetRole: {targetRole.ToString()}");

            if (targetAppSessions.Any() == false)
            {
                PrintMessageEvent?.Invoke(" - Service have no target connected user");
            }
            else
            {
                foreach (var appSession in targetAppSessions)
                {
                    try
                    {
                        if (publishEvent.Invoke(appSession))
                        {
                            result = true;
                            PrintMessageEvent?.Invoke($" + Success (appId: {appSession.AppSessionId})");
                        }
                    }
                    //отключаем приложение от сервиса
                    catch (CommunicationObjectAbortedException e)
                    {
                        OnPublishEventByServiceForUserException(e, appSession);
                    }
                    catch (TimeoutException e)
                    {
                        OnPublishEventByServiceForUserException(e, appSession);
                    }
                    catch (Exception e)
                    {
                        PrintMessageEvent?.Invoke($" - Faulted {e.GetType().FullName} ({appSession})");
                        PrintMessageEvent?.Invoke($"!Exception on Invoke {publishEvent.GetMethodInfo().Name} ({this.GetType().FullName}) by appSession {sourceEventAppSessionId}. \n{e.GetType().FullName}\n{e.PrintAllExceptions()}");
                        this.Disconnect(appSession.AppSessionId);
                    }
                }
            }

            PrintMessageEvent?.Invoke("-------------------");

            return result;
        }

        private void OnPublishEventByServiceForUserException(Exception e, AppSession appSession)
        {
            PrintMessageEvent?.Invoke($" - Faulted {e.GetType().FullName} ({appSession})");
            PrintMessageEvent?.Invoke($"{this.GetType().FullName}. {e.GetType().FullName}.");
            this.Disconnect(appSession.AppSessionId);
        }

        #region Directum

        public bool SaveDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid taskId)
        {
            return PublishEventByServiceForUser(targetUserId, targetRole, eventSourceAppSessionId, 
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSaveDirectumTaskServiceCallback(taskId));
        }

        public bool StartDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid taskId)
        {
            return PublishEventByServiceForUser(targetUserId, targetRole, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnStartDirectumTaskServiceCallback(taskId));
        }

        public bool StopDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid taskId)
        {
            return PublishEventByServiceForUser(targetUserId, targetRole, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnStopDirectumTaskServiceCallback(taskId));
        }

        public bool PerformDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid taskId)
        {
            return PublishEventByServiceForUser(targetUserId, targetRole, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnPerformDirectumTaskServiceCallback(taskId));
        }

        public bool AcceptDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid taskId)
        {
            return PublishEventByServiceForUser(targetUserId, targetRole, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnAcceptDirectumTaskServiceCallback(taskId));
        }

        public bool RejectDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid taskId)
        {
            return PublishEventByServiceForUser(targetUserId, targetRole, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnRejectDirectumTaskServiceCallback(taskId));
        }
        #endregion

        #region PriceCalculation

        public bool SavePriceCalculationPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid priceCalculationId)
        {
            return PublishEventByServiceForUser(targetUserId, targetRole, eventSourceAppSessionId, 
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSavePriceCalculationServiceCallback(priceCalculationId));
        }

        public bool StartPriceCalculationPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid priceCalculationId)
        {
            return PublishEventByServiceForUser(targetUserId, targetRole, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnStartPriceCalculationServiceCallback(priceCalculationId));
        }

        public bool FinishPriceCalculationPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid priceCalculationId)
        {
            return PublishEventByServiceForUser(targetUserId, targetRole, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnFinishPriceCalculationServiceCallback(priceCalculationId));
        }

        public bool CancelPriceCalculationPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid priceCalculationId)
        {
            return PublishEventByServiceForUser(targetUserId, targetRole, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnCancelPriceCalculationServiceCallback(priceCalculationId));
        }

        public bool RejectPriceCalculationPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid priceCalculationId)
        {
            return PublishEventByServiceForUser(targetUserId, targetRole, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnRejectPriceCalculationServiceCallback(priceCalculationId));
        }

        #endregion

        #region IncomingRequest

        public bool SaveIncomingRequestPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid requestId)
        {
            return PublishEventByServiceForUser(targetUserId, targetRole, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSaveIncomingRequestServiceCallback(requestId));
        }

        #endregion

        #region IncomingDocument

        public bool SaveIncomingDocumentPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid documentId)
        {
            return PublishEventByServiceForUser(targetUserId, targetRole, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSaveIncomingDocumentServiceCallback(documentId));
        }

        #endregion

        #region TechnicalRequarementsTask

        public bool SaveTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId)
        {
            return PublishEventByServiceForUser(targetUserId, targetRole, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSaveTechnicalRequarementsTaskServiceCallback(technicalRequarementsTaskId));
        }

        public bool StartTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId)
        {
            return PublishEventByServiceForUser(targetUserId, targetRole, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnStartTechnicalRequarementsTaskServiceCallback(technicalRequarementsTaskId));
        }

        public bool InstructTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId)
        {
            return PublishEventByServiceForUser(targetUserId, targetRole, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnInstructTechnicalRequarementsTaskServiceCallback(technicalRequarementsTaskId));
        }

        public bool StopTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId)
        {
            return PublishEventByServiceForUser(targetUserId, targetRole, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnStopTechnicalRequarementsTaskServiceCallback(technicalRequarementsTaskId));
        }

        public bool RejectTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId)
        {
            return PublishEventByServiceForUser(targetUserId, targetRole, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnRejectTechnicalRequarementsTaskServiceCallback(technicalRequarementsTaskId));
        }

        public bool RejectByFrontManagerTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId)
        {
            return PublishEventByServiceForUser(targetUserId, targetRole, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnRejectByFrontManagerTechnicalRequarementsTaskServiceCallback(technicalRequarementsTaskId));
        }

        public bool FinishTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId)
        {
            return PublishEventByServiceForUser(targetUserId, targetRole, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnFinishTechnicalRequarementsTaskServiceCallback(technicalRequarementsTaskId));
        }

        public bool AcceptTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId)
        {
            return PublishEventByServiceForUser(targetUserId, targetRole, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnAcceptTechnicalRequarementsTaskServiceCallback(technicalRequarementsTaskId));
        }

        #endregion

        #region PriceEngineeringTasks

        public bool PriceEngineeringTaskNotificationEvent(Guid eventSourceAppSessionId, Guid userAuthorId, Guid userTargetId, Role userTargetRole, Guid priceEngineeringTaskId, string message)
        {
            bool result = false;

            //целевые приложения (без того, которое и послало событие).
            var targetAppSessions = _appSessions
                .Where(appSession => appSession.UserId == userTargetId)
                .Where(appSession => appSession.UserRole == userTargetRole)
                .Where(appSession => appSession.AppSessionId != eventSourceAppSessionId)
                .ToList();

            PrintMessageEvent?.Invoke("-------------------");
            PrintMessageEvent?.Invoke($"Invoke {nameof(PriceEngineeringTaskNotificationEvent)} (sourceEventAppSessionId: {eventSourceAppSessionId} targetUserId: {userTargetId}");

            if (targetAppSessions.Any() == false)
            {
                PrintMessageEvent?.Invoke(" - Service have no target connected user");
            }
            else
            {
                foreach (var appSession in targetAppSessions)
                {
                    try
                    {
                        if (appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnPriceEngineeringNotificationServiceCallback(priceEngineeringTaskId, message))
                        {
                            result = true;
                            PrintMessageEvent?.Invoke($" + Success ({appSession})");
                        }
                    }
                    //отключаем приложение от сервиса
                    catch (CommunicationObjectAbortedException e)
                    {
                        OnPublishEventByServiceForUserException(e, appSession);
                    }
                    catch (TimeoutException e)
                    {
                        OnPublishEventByServiceForUserException(e, appSession);
                    }
                    catch (Exception e)
                    {
                        PrintMessageEvent?.Invoke($" - Faulted {e.GetType().FullName} ({appSession})");
                        PrintMessageEvent?.Invoke($"!Exception on Invoke {nameof(PriceEngineeringTaskNotificationEvent)} ({this.GetType().FullName}) by appSession {eventSourceAppSessionId}. \n{e.GetType().FullName}\n{e.PrintAllExceptions()}");
                        this.Disconnect(appSession.AppSessionId);
                    }
                }
            }

            PrintMessageEvent?.Invoke("-------------------");

            return result;
        }

        public bool PriceEngineeringTaskSendMessagePublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid messageId)
        {
            return PublishEventByServiceForUser(targetUserId, targetRole, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnPriceEngineeringTaskSendMessageServiceCallback(messageId));
        }

        #endregion

        public bool SavePaymentDocumentPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid paymentDocumentId)
        {
            return PublishEventByServiceForUser(targetUserId, targetRole, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSavePaymentDocumentServiceCallback(paymentDocumentId));
        }
    }
}
