using System;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Services
{
    public interface INotificationGeneratorService
    {
        string GetTargetEntityInfo(NotificationUnit unit);
        string GetTargetActionInfo(NotificationUnit unit);
        Action GetOpenTargetEntityViewAction(NotificationUnit unit);
    }
}