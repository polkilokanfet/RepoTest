using System;

namespace HVTApp.Infrastructure.Interfaces.Services.EventService
{
    public interface IEventServiceClient : IEventServiceCallback
    {
        void Start();
        void Stop();

        void SendMessageToChat(string message);
        void SendMessageToUser(Guid recipientId, string message);
    }
}