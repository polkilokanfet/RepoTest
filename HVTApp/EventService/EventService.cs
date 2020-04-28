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

        public bool Connect(Guid appSassionId)
        {
            if (_appSessions.Select(x => x.AppSessionId).Contains(appSassionId))
                return false;

            _appSessions.Add(new AppSession(appSassionId, OperationContext.Current));
            return true;
        }

        public void Disconnect(Guid appSassionId)
        {
            var user = _appSessions.SingleOrDefault(x => x.AppSessionId == appSassionId);
            if (user != null)
                _appSessions.Remove(user);
        }

        public void SaveIncomingRequestPublishEvent(Guid appSassionId, Guid requestId)
        {
            SavePublishEvent(appSassionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSaveIncomingRequestPublishEvent(requestId));
        }

        public void SaveDirectumTaskPublishEvent(Guid appSassionId, Guid taskId)
        {
            SavePublishEvent(appSassionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSaveDirectumTaskPublishEvent(taskId));
        }

        public void SavePriceCalculationPublishEvent(Guid appSassionId, Guid priceCalculationId)
        {
            SavePublishEvent(appSassionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSavePriceCalculationPublishEvent(priceCalculationId));
        }

        public void SaveIncomingDocumentPublishEvent(Guid appSassionId, Guid documentId)
        {
            SavePublishEvent(appSassionId, appSession => appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSaveIncomingDocumentPublishEvent(documentId));
        }


        private void SavePublishEvent(Guid appSessionId, Action<AppSession> publishEvent)
        {
            foreach (var appSession in _appSessions.Where(x => x.AppSessionId != appSessionId).ToList())
            {
                try
                {
                    publishEvent.Invoke(appSession);
                }
                catch (Exception e)
                {
                }
            }
        }
    }
}
