using System;
using System.Threading.Tasks;

namespace HVTApp.Infrastructure.Interfaces.Services.EventService
{
    public interface IEventServiceClient : IEventServiceCallback
    {
        /// <summary>
        /// Сигнал о начале старта сервиса
        /// </summary>
        event Action StartActionInProgressEvent;

        /// <summary>
        /// Стартовать сервис синхронизации
        /// </summary>
        Task<bool> Start();

        /// <summary>
        /// Остановить сервис синхронизации
        /// </summary>
        Task Stop();

        bool UserConnected(Guid userId);

        #region MessageToChat

        void SendMessageToChat(string message);
        void SendMessageToUser(Guid recipientId, string message);

        #endregion


        #region PriceEngineeringTasks

        bool PriceEngineeringTaskSendMessagePublishEvent(Guid targetUserId, Role targetRole, Guid messageId);

        #endregion
    }
}