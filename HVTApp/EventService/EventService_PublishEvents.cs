using System;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces.Services.EventService;

namespace EventService
{
    public partial class EventService : IEventService
    {
        /// <summary>
        /// Закрыть все приложения пользователей
        /// </summary>
        public void ApplicationsShutdown()
        {
            foreach (var appSession in _appSessions)
            {
                Task.Run(() => {appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().ApplicationShutdown();}).Await();
            }
        }

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
            var result = false;

            //целевые приложения (без того, которое и послало событие).
            var targetAppSessions = _appSessions
                .Where(appSession => appSession.UserId == targetUserId)
                .Where(appSession => appSession.UserRole == targetRole)
                .Where(appSession => appSession.AppSessionId != sourceEventAppSessionId)
                .ToList();

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

            return result;
        }

        private void OnPublishEventByServiceForUserException(Exception e, AppSession appSession)
        {
            PrintMessageEvent?.Invoke($" - Faulted {e.GetType().FullName} ({appSession})");
            PrintMessageEvent?.Invoke($"{this.GetType().FullName}. {e.GetType().FullName}.");
            this.Disconnect(appSession.AppSessionId);
        }

        //public bool SendNotificationToService(Guid eventSourceAppSessionId, Guid userAuthorId, Guid userTargetId, Role userTargetRole, Guid priceEngineeringTaskId, NotificationActionType actionType)
        //{
        //    bool result = false;

        //    //целевые приложения (без того, которое и послало событие).
        //    var targetAppSessions = _appSessions
        //        .Where(appSession => appSession.UserId == userTargetId && 
        //                             appSession.UserRole == userTargetRole && 
        //                             appSession.AppSessionId != eventSourceAppSessionId)
        //        .ToList();

        //    PrintMessageEvent?.Invoke("-------------------");
        //    PrintMessageEvent?.Invoke($"Invoke {nameof(SendNotificationToService)} {actionType} (sourceEventAppSessionId: {eventSourceAppSessionId} targetUserId: {userTargetId}");

        //    if (targetAppSessions.Any() == false)
        //    {
        //        PrintMessageEvent?.Invoke($" - Service have no target connected user (role: {userTargetRole}, userId: {userTargetId})");
        //    }
        //    else
        //    {
        //        foreach (var appSession in targetAppSessions)
        //        {
        //            new Task(() =>
        //            {
        //                result = appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnNotificationCallback(actionType, priceEngineeringTaskId);
        //            }).Await();

        //            try
        //            {
        //                if (appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnNotificationCallback(actionType, priceEngineeringTaskId))
        //                {
        //                    result = true;
        //                    PrintMessageEvent?.Invoke($" + Success ({appSession})");
        //                }
        //            }
        //            //отключаем приложение от сервиса
        //            catch (CommunicationObjectAbortedException e)
        //            {
        //                OnPublishEventByServiceForUserException(e, appSession);
        //            }
        //            catch (TimeoutException e)
        //            {
        //                OnPublishEventByServiceForUserException(e, appSession);
        //            }
        //            catch (Exception e)
        //            {
        //                PrintMessageEvent?.Invoke($" - Faulted {e.GetType().FullName} ({appSession})");
        //                PrintMessageEvent?.Invoke($"!Exception on Invoke {nameof(SendNotificationToService)} ({this.GetType().FullName}) by appSession {eventSourceAppSessionId}. \n{e.GetType().FullName}\n{e.PrintAllExceptions()}");
        //                this.Disconnect(appSession.AppSessionId);
        //            }
        //        }
        //    }

        //    PrintMessageEvent?.Invoke("-------------------");

        //    return result;
        //}

        #region PriceEngineeringTasks

        public async Task<bool> SendNotificationToServiceAsync(Guid eventSourceAppSessionId, Guid userAuthorId, Guid userTargetId, Role userTargetRole, Guid priceEngineeringTaskId, NotificationActionType actionType)
        {
            bool result = false;

            //целевые приложения (без того, которое и послало событие).
            var targetAppSessions = _appSessions
                .Where(appSession => appSession.UserId == userTargetId &&
                                     appSession.UserRole == userTargetRole &&
                                     appSession.AppSessionId != eventSourceAppSessionId)
                .ToList();

            var s = $"Invoke {nameof(SendNotificationToServiceAsync)} {actionType} (sourceEventAppSessionId: {eventSourceAppSessionId} targetUserId: {userTargetId}";
            if (targetAppSessions.Any() == false)
            {
                PrintMessageEvent?.Invoke($"- [{s}]: Service have no target connected user (role: {userTargetRole}, userId: {userTargetId})");
            }
            else
            {
                foreach (var appSession in targetAppSessions)
                {
                    try
                    {
                        await Task.Run(() =>
                        {
                            result = appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnNotificationCallback(actionType, priceEngineeringTaskId);
                        });

                        if (result == true)
                        {
                            PrintMessageEvent?.Invoke($"+ [{s}]: Success ({appSession})");
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
                        PrintMessageEvent?.Invoke($"- [{s}] Faulted {e.GetType().FullName} ({appSession})");
                        PrintMessageEvent?.Invoke($"!Exception on Invoke {nameof(SendNotificationToServiceAsync)} ({this.GetType().FullName}) by appSession {eventSourceAppSessionId}. \n{e.GetType().FullName}\n{e.PrintAllExceptions()}");
                        this.Disconnect(appSession.AppSessionId);
                    }
                }
            }

            return result;

        }

        public bool PriceEngineeringTaskSendMessagePublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid messageId)
        {
            return PublishEventByServiceForUser(targetUserId, targetRole, eventSourceAppSessionId,
                appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnPriceEngineeringTaskSendMessageServiceCallback(messageId));
        }

        #endregion

        //public bool SavePaymentDocumentPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid paymentDocumentId)
        //{
        //    return PublishEventByServiceForUser(targetUserId, targetRole, eventSourceAppSessionId,
        //        appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSavePaymentDocumentServiceCallback(paymentDocumentId));
        //}
    }
}
