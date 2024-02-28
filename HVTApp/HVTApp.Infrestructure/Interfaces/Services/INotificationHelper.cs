using System;

namespace HVTApp.Infrastructure.Interfaces.Services
{
    public interface INotificationHelper
    {
        string GetCommonInfo();
        string GetActionInfo();
        Action GetOpenTargetEntityViewAction();
        void RefreshTargetEntityAction();
    }
}