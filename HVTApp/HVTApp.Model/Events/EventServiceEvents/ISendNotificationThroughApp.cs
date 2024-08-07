using System.Threading.Tasks;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Events.EventServiceEvents
{
    /// <summary>
    /// �������� �� ���� ����� ����������
    /// </summary>
    public interface ISendNotificationThroughApp
    {
        Task<bool> SendNotificationAsync(NotificationUnit unit);
    }
}