using HVTApp.Model.Events.EventServiceEvents.Args;

namespace HVTApp.Model.Events.EventServiceEvents
{
    /// <summary>
    /// �������� �� ���� ����� ����������
    /// </summary>
    public interface ISendNotificationThroughApp
    {
        bool SendNotification(NotificationAboutPriceEngineeringTaskEventArg item);
    }
}