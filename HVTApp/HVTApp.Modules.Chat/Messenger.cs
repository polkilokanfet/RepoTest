using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Messenger
{
    public class Messenger : IMessenger
    {
        private readonly IUnityContainer _container;
        private readonly User[] _users;
        private readonly List<PersonalChatViewModel> _personalChatViewModels = new List<PersonalChatViewModel>();

        public GeneralChatViewModel GeneralChatViewModel { get; }

        public Messenger(IUnityContainer container)
        {
            _container = container;
            _users = container.Resolve<IUnitOfWork>().Repository<User>().GetAll();
            GeneralChatViewModel = new GeneralChatViewModel(container);
        }

        public void ReceiveChatMessage(Guid authorId, string message)
        {
            GeneralChatViewModel.ReceiveMessage(_users.Single(user => user.Id == authorId).Employee.ToString(), DateTime.Now, message);
        }

        public void ReceivePersonalMessage(Guid authorId, string message)
        {
            var personalChatViewModel = GetPersonalChatViewModel(authorId);
            personalChatViewModel.ReceiveMessage(_users.Single(user => user.Id == authorId).Employee.ToString(), DateTime.Now, message);
        }

        public void SendChatMessage(string message)
        {
            _container.Resolve<IEventServiceClient>().SendMessageToChat(message);
        }

        public void SendPersonalMessage(Guid recipientId, string message)
        {
            _container.Resolve<IEventServiceClient>().SendMessageToUser(recipientId, message);
        }


        public PersonalChatViewModel GetPersonalChatViewModel(Guid interlocutorId)
        {
            var result = _personalChatViewModels.SingleOrDefault(x => x.InterlocutorId == interlocutorId);
            if (result == null)
            {
                result = new PersonalChatViewModel(_container, interlocutorId);
                _personalChatViewModels.Add(result);
            }
            return result;
        }
    }
}