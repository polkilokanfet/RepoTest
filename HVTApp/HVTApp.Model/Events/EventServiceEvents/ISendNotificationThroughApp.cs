using System.Threading.Tasks;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Events.EventServiceEvents
{
    /// <summary>
    /// Отправка по сети через приложение
    /// </summary>
    public interface ISendNotificationThroughApp
    {
        Task<bool> SendNotificationAsync(NotificationUnit unit);
    }
}