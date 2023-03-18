using System;
using HVTApp.Infrastructure.Interfaces.Services;
using Microsoft.Practices.Unity;

namespace EventServiceClient2
{
    public partial class EventServiceClient
    {
        #region Chat

        public void SendMessageToChat(string message)
        {
            EventServiceHost?.SendMessageToChat(_userId, message);
        }

        public void SendMessageToUser(Guid recipientId, string message)
        {
            EventServiceHost?.SendMessageToUser(_userId, recipientId, message);
        }



        public void OnSendMessageToChat(Guid authorId, string message)
        {
            _container.Resolve<IMessenger>().ReceiveChatMessage(authorId, message);
        }

        public void OnSendMessageToUser(Guid authorId, string message)
        {
            _container.Resolve<IMessenger>().ReceivePersonalMessage(authorId, message);
        }

        #endregion
    }
}