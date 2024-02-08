using System;
using HVTApp.Model.Events.NotificationArgs;

namespace HVTApp.Model.Services
{
    public interface INotificationGeneratorService
    {
        string GetTargetEntityInfo(NotificationUnit unit);
        string GetTargetActionInfo(NotificationUnit unit);
        Action GetOpenTargetEntityViewAction(NotificationUnit unit);
    }
}