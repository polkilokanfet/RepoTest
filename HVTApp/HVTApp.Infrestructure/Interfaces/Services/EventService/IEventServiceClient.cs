using System;
using System.Threading.Tasks;

namespace HVTApp.Infrastructure.Interfaces.Services.EventService
{
    public interface IEventServiceClient : IEventServiceCallback
    {
        /// <summary>
        /// Сигнал о стопе сервиса
        /// </summary>
        event Action StartEvent;

        /// <summary>
        /// Стартовать сервис синхронизации
        /// </summary>
        Task Start();

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