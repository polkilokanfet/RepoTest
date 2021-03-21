using System;

namespace HVTApp.Infrastructure.Interfaces.Services.EventService
{
    public interface IEventServiceClient : IEventServiceCallback
    {
        void Start();
        void Stop();

        /// <summary>
        /// Скопировать приложения к проекту
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="projectId"></param>
        /// <param name="targetDirectory">Куда копировать</param>
        /// <returns></returns>
        void CopyProjectAttachmentsRequest(Guid userId, Guid projectId, string targetDirectory);

        bool UserConnected(Guid userId);

        void SendMessageToChat(string message);
        void SendMessageToUser(Guid recipientId, string message);
    }
}