using System;

namespace HVTApp.Infrastructure.Interfaces.Services
{
    public interface INotificationHelper
    {
        string GetTargetEntityInfo();
        string GetTargetActionInfo();
        Action GetOpenTargetEntityViewAction();
    }
}