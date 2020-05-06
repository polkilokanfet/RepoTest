using System;
using HVTApp.Infrastructure.Interfaces.Services;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Messenger
{
    public class PersonalChatViewModel : ChatViewModel
    {
        /// <summary>
        /// Id собеседника
        /// </summary>
        public Guid InterlocutorId { get; }

        public PersonalChatViewModel(IUnityContainer container, Guid interlocutorId) : base(container)
        {
            InterlocutorId = interlocutorId;
        }

        protected override void SendMessageCommand_Execute()
        {
            Container.Resolve<IMessenger>().SendPersonalMessage(InterlocutorId, Message);
        }
    }
}