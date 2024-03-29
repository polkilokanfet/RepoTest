using HVTApp.Model.POCOs;

namespace HVTApp.Model.Services
{
    public interface INotificationTextService
    {
        string GetActionInfo(NotificationUnit notificationUnit);
        string GetCommonInfo(NotificationUnit notificationUnit);
    }
}