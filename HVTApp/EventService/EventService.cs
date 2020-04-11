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
            _appSessions.Where(x => x.AppSessionId != appSassionId).ToList()
                .ForEach(x => x.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSaveIncomingRequestPublishEvent(requestId));
        }
    }
}
