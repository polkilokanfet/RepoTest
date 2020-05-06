using System;

namespace HVTApp.Infrastructure.Interfaces.Services
{
    public interface IMessenger
    {
        void ReceiveChatMessage(Guid authorId, string message);
        void ReceivePersonalMessage(Guid authorId, string message);

        void SendChatMessage(string message);
        void SendPersonalMessage(Guid recipientId, string message);

    }
}