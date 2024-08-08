using System;
using System.ServiceModel;
using System.Threading.Tasks;
using HVTApp.Infrastructure.Enums;

namespace HVTApp.Infrastructure.Interfaces.Services.EventService
{
    [ServiceContract(CallbackContract = typeof(IEventServiceCallback))]
    public partial interface IEventService
    {
        /// <summary>
        /// ����������� � �������
        /// </summary>
        /// <param name="appSessionId">Id ������ ����������</param>
        /// <param name="userId">Id ������������</param>
        /// <param name="userRole">���� ������������</param>
        /// <returns></returns>
        [OperationContract]
        bool Connect(Guid appSessionId, Guid userId, Role userRole);

        /// <summary>
        /// ���������� �� �������
        /// </summary>
        /// <param name="appSessionId">Id ������ ����������</param>
        [OperationContract]
        void Disconnect(Guid appSessionId);

        /// <summary>
        /// ������ ��������
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool HostIsAlive();

        /// <summary>
        /// ������������ ��������� � �������
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool UserIsConnected(Guid userId);

        #region Chat

        [OperationContract]
        void SendMessageToChat(Guid authorId, string message);

        [OperationContract]
        void SendMessageToUser(Guid authorId, Guid recipientId, string message);

        #endregion

        [OperationContract]
        Task<bool> SendNotificationToServiceAsync(Guid eventSourceAppSessionId, Guid userAuthorId, Guid userTargetId, Role userTargetRole, Guid priceEngineeringTaskId, NotificationActionType actionType);

        [OperationContract]
        bool PriceEngineeringTaskSendMessagePublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid messageId);
    }
}