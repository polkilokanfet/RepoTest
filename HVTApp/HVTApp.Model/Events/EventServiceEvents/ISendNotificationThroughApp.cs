using HVTApp.Model.Events.NotificationArgs;

namespace HVTApp.Model.Events.EventServiceEvents
{
    /// <summary>
    /// �������� �� ���� ����� ����������
    /// </summary>
    public interface ISendNotificationThroughApp
    {
        bool SendNotification(NotificationUnit unit);
    }
}