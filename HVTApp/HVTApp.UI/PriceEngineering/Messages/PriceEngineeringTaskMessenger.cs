using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
        private readonly TaskViewModel _viewModel;
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

        public PriceEngineeringTaskMessenger(TaskViewModel viewModel, IUnityContainer container) 
            : base(new ObservableCollection<IMessage>())
        {
            this.Items.AddRange(viewModel.Model.GetMessages().OrderByDescending(x => x.Moment));

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

}