using HVTApp.Infrastructure.Interfaces.Services;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Messenger
{
    public class GeneralChatViewModel : ChatViewModel
    {
        public GeneralChatViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override void SendMessageCommand_Execute()
        {
            Container.Resolve<IMessenger>().SendChatMessage(Message);
        }
    }
}