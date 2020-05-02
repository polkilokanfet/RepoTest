using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace EventService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    public class EventService : IEventService
    {
        private readonly List<AppSession> _appSessions = new List<AppSession>();

        public bool Connect(Guid appSessionId, Guid userId)
        {
            if (_appSessions.Select(x => x.AppSessionId).Contains(appSessionId))
                return false;

            _appSessions.Add(new AppSession(appSessionId, userId, OperationContext.Current));
            return true;
        }

        public void Disconnect(Guid appSessionId)
        {
            var appSession = _appSessions.SingleOrDefault(x => x.AppSessionId == appSessionId);
            if (appSession != null)
                _appSessions.Remove(appSession);
        }

        #region SavePublishEvent

        public void SaveIncomingRequestPublishEvent(Guid appSessionId, Guid requestId)
        {
            SavePublishEvent(appSessionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSaveIncomingRequestPublishEvent(requestId));
        }

        public void SaveDirectumTaskPublishEvent(Guid appSessionId, Guid taskId)
        {
            SavePublishEvent(appSessionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSaveDirectumTaskPublishEvent(taskId));
        }

        public void SavePriceCalculationPublishEvent(Guid appSessionId, Guid priceCalculationId)
        {
            SavePublishEvent(appSessionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSavePriceCalculationPublishEvent(priceCalculationId));
        }

        public void SaveIncomingDocumentPublishEvent(Guid appSessionId, Guid documentId)
        {
            SavePublishEvent(appSessionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSaveIncomingDocumentPublishEvent(documentId));
        }

        private void SavePublishEvent(Guid appSessionId, Action<AppSession> publishEvent)
        {
            foreach (var appSession in _appSessions.Where(x => x.AppSessionId != appSessionId).ToList())
            {
                try
                {
                    publishEvent.Invoke(appSession);
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

        #endregion

        public void Close()
        {
            foreach (var appSession in _appSessions)
            {
                try
                {
                    appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnServiceDisposeEvent();
                }
                catch (Exception e)
                {
                }
            }
        }
    }
}
