using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering.Messages
{
    public class PriceEngineeringTaskMessenger : ReadOnlyObservableCollection<IMessage>, IDisposable
    {
        private readonly PriceEngineeringTaskViewModel _viewModel;
        private readonly IUnityContainer _container;
        private IUnitOfWork _unitOfWork;
        private string _messageText;
        private IUnitOfWork UnitOfWork => _unitOfWork ?? (_unitOfWork = _container.Resolve<IUnitOfWork>());

        /// <summary>
        /// Можно ли вести переписку
        /// </summary>
        public bool AllowTexting
        {
            get
            {
                switch (_viewModel.Status)
                {
                    case PriceEngineeringTaskStatusEnum.Created:
                    case PriceEngineeringTaskStatusEnum.Stopped:
                    case PriceEngineeringTaskStatusEnum.Accepted:
                        return false;
                }

                return true;
            }
        }

        public DelegateLogCommand SendMessageCommand { get; }

        public string MessageText
        {
            get => _messageText;
            set
            {
                if (Equals(_messageText, value))
                    return;

                _messageText = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(MessageText)));
                SendMessageCommand.RaiseCanExecuteChanged();
            }
        }

        public PriceEngineeringTaskMessenger(PriceEngineeringTaskViewModel viewModel, IUnityContainer container) 
            : base(new ObservableCollection<IMessage>())
        {
            var messages = new List<IMessage>(viewModel.Model.Messages);
            messages.AddRange(viewModel.Model.Statuses.Select(PriceEngineeringTaskStatusMessage.Convert));
            this.Items.AddRange(messages.OrderByDescending(x => x.Moment));

            _viewModel = viewModel;
            _container = container;

            SendMessageCommand = new DelegateLogCommand(
                () =>
                {
                    var message = this.SendMessage(MessageText);
                    container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskSendMessageEvent>().Publish(message);
                    this.MessageText = string.Empty;
                },
                () => AllowTexting && string.IsNullOrWhiteSpace(MessageText) == false);

            _viewModel.Statuses.CollectionChanged += (sender, args) =>
            {
                this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(AllowTexting)));
                SendMessageCommand.RaiseCanExecuteChanged();

                foreach (var statusWrapper in args.NewItems.Cast<PriceEngineeringTaskStatusEmptyWrapper>())
                {
                    this.Items.Insert(0, PriceEngineeringTaskStatusMessage.Convert(statusWrapper.Model));
                }
            };


            _container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskReciveMessageEvent>().Subscribe(OnReciveMessageEvent);
        }

        public PriceEngineeringTaskMessage SendMessage(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return null;

            var message = new PriceEngineeringTaskMessage
            {
                Message = text,
                Author = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id),
                PriceEngineeringTaskId = _viewModel.Model.Id
            };
            UnitOfWork.Repository<PriceEngineeringTaskMessage>().Add(message);
            UnitOfWork.SaveChanges();

            this.Items.Insert(0, message);

            return message;
        }

        private void OnReciveMessageEvent(PriceEngineeringTaskMessage message)
        {
            if (message.PriceEngineeringTaskId == this._viewModel.Model.Id)
            {
                if (this.Where(x => x is PriceEngineeringTaskMessage).Cast<PriceEngineeringTaskMessage>().ContainsById(message) == false)
                {
                    this.Items.Insert(0, message);
                }
            }
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
            _container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskReciveMessageEvent>().Unsubscribe(OnReciveMessageEvent);
        }
    }

    public class PriceEngineeringTaskStatusMessage : IMessage
    {
        public DateTime Moment { get; }
        public string Message { get; }

        private PriceEngineeringTaskStatusMessage(DateTime moment, string message)
        {
            Moment = moment;
            Message = message;
        }

        public static PriceEngineeringTaskStatusMessage Convert(PriceEngineeringTaskStatus status)
        {
            var sb = new StringBuilder();

            switch (status.StatusEnum)
            {
                case PriceEngineeringTaskStatusEnum.Created:
                    sb.Append("Менеджер создал задачу");
                    break;
                case PriceEngineeringTaskStatusEnum.Started:
                    sb.Append("Менеджер запустил задачу на проработку");
                    break;
                case PriceEngineeringTaskStatusEnum.Stopped:
                    sb.Append("Менеджер остановил проработку задачи");
                    break;
                case PriceEngineeringTaskStatusEnum.RejectedByManager:
                    sb.Append("Менеджер вернул задачу на доработку исполнителю");
                    break;
                case PriceEngineeringTaskStatusEnum.RejectedByConstructor:
                    sb.Append("Исполнитель отклонил задачу");
                    break;
                case PriceEngineeringTaskStatusEnum.FinishedByConstructor:
                    sb.Append("Исполнитель завершил работу над задачей");
                    break;
                case PriceEngineeringTaskStatusEnum.Accepted:
                    sb.Append("Менеджер принял проработку задачи");
                    break;
                case PriceEngineeringTaskStatusEnum.FinishedByConstructorGoToVerification:
                    sb.Append("Исполнитель направил задачу на проверку руководителю");
                    break;
                case PriceEngineeringTaskStatusEnum.VerificationAcceptedByHead:
                    sb.Append("Руководитель согласовал исполнителю проработку");
                    break;
                case PriceEngineeringTaskStatusEnum.VerificationRejectededByHead:
                    sb.Append("Руководитель отклонил исполнителю проработку");
                    break;
            }

            if (string.IsNullOrWhiteSpace(status.Comment) == false)
            {
                sb.AppendLine(status.Comment);
            }

            return new PriceEngineeringTaskStatusMessage(status.Moment, sb.ToString());
        }
    }
}