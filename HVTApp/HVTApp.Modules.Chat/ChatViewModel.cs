using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HVTApp.Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Messenger
{
    public abstract class ChatViewModel : ViewModelBase
    {
        private string _message;
        public ObservableCollection<Message> Messages { get; } = new ObservableCollection<Message>();

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                ((DelegateCommand)SendMessageCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand SendMessageCommand { get; }

        protected ChatViewModel(IUnityContainer container) : base(container)
        {
            SendMessageCommand = new DelegateCommand(SendMessageCommand_Execute, () => !string.IsNullOrWhiteSpace(Message));
        }

        protected abstract void SendMessageCommand_Execute();

        public void ReceiveMessage(string author, DateTime moment, string message)
        {
            Messages.Add(new Message(author, moment, message));
        }

    }
}