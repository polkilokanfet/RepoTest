using System;
using System.ServiceModel;
using HVTApp.Infrastructure.Enums;

namespace HVTApp.Infrastructure.Interfaces.Services.EventService
{
    /// <summary>
    /// Вызывается на клиенте по запросу сервиса
    /// </summary>
    [ServiceContract]
    public interface IEventServiceCallback
    {
        /// <summary>
        /// Закрыть приложение у пользователя
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void ApplicationShutdown();

        /// <summary>
        /// Реакция клиента на остановку сервиса
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void OnServiceDisposeEvent();

        [OperationContract(IsOneWay = true)]
        void OnSendMessageToChat(Guid authorId, string message);

        [OperationContract(IsOneWay = true)]
        void OnSendMessageToUser(Guid authorId, string message);



        [OperationContract]
        bool OnNotificationCallback(NotificationActionType actionType, Guid targetEntityId);


        #region PriceEngineeringTask

        [OperationContract]
        bool OnPriceEngineeringTaskSendMessageServiceCallback(Guid messageId);

        #endregion

        /// <summary>
        /// Проверка: жив ли клиент
        /// </summary>
        /// <returns>жив?</returns>
        [OperationContract]
        bool IsAlive();
    }
}