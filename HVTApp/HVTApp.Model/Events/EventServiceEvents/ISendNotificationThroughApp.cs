using HVTApp.Model.POCOs;

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