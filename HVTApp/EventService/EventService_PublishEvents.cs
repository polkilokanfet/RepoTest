using System;
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

        public void FinishTechnicalRequarementsTaskPublishEvent(Guid appSessionId, Guid technicalRequarementsTaskId)
        {
            PublishEventByService(appSessionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnFinishTechnicalRequarementsTaskServiceCallback(technicalRequarementsTaskId));
        }

        public void AcceptTechnicalRequarementsTaskPublishEvent(Guid appSessionId, Guid technicalRequarementsTaskId)
        {
            PublishEventByService(appSessionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnAcceptTechnicalRequarementsTaskServiceCallback(technicalRequarementsTaskId));
        }

        public void StopTechnicalRequarementsTaskPublishEvent(Guid appSessionId, Guid technicalRequarementsTaskId)
        {
            PublishEventByService(appSessionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnStopTechnicalRequarementsTaskServiceCallback(technicalRequarementsTaskId));
        }

        #endregion
    }
}
