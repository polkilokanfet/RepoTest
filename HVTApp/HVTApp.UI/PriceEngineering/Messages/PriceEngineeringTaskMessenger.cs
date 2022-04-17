using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Mvvm;

namespace HVTApp.UI.PriceEngineering.Messages
{
    public class PriceEngineeringTaskMessenger : BindableBase
    {
        private readonly PriceEngineeringTaskViewModel _viewModel;
        private PriceEngineeringTaskMessagesWrapper _taskMessagesWrapper;
        public PriceEngineeringTaskMessageWrapper Message { get; }

        public ObservableCollection<PriceEngineeringTaskMessageWrapper> MessagesToShow { get; }

        private PriceEngineeringTaskMessagesWrapper TaskMessagesWrapper
        {
            get => _taskMessagesWrapper;
            set
            {
                if (_taskMessagesWrapper != null)
                    _taskMessagesWrapper.Messages.CollectionChanged -= this.MessagesOnCollectionChanged;

                _taskMessagesWrapper = value;

                if (_taskMessagesWrapper != null)
                    _taskMessagesWrapper.Messages.CollectionChanged += this.MessagesOnCollectionChanged;
            }
        }

        /// <summary>
        /// Можно ли вести переписку
        /// </summary>
        public bool AllowTexting
        {
            get
            {
                //статусы при которых разрешена отправка сообщений
                var statuses = new List<PriceEngineeringTaskStatusEnum>
                {
                    PriceEngineeringTaskStatusEnum.Created,
                    PriceEngineeringTaskStatusEnum.Started,
                    PriceEngineeringTaskStatusEnum.FinishedByConstructor,
                    PriceEngineeringTaskStatusEnum.RejectedByConstructor,
                    PriceEngineeringTaskStatusEnum.RejectedByManager
                };

                return statuses.Contains(_viewModel.Status);
            }
        }

        public DelegateLogCommand SendMessageCommand { get; }

        public event Action<Guid, DateTime, string> SendedMessageInNewTask; 

        public PriceEngineeringTaskMessenger(IUnityContainer container, PriceEngineeringTaskViewModel viewModel)
        {
            _viewModel = viewModel;
            var unitOfWork = container.Resolve<IUnitOfWork>();

            var priceEngineeringTask = unitOfWork.Repository<PriceEngineeringTask>().GetById(viewModel.Model.Id) ?? viewModel.Model;
            TaskMessagesWrapper = new PriceEngineeringTaskMessagesWrapper(priceEngineeringTask);

            MessagesToShow = new ObservableCollection<PriceEngineeringTaskMessageWrapper>(TaskMessagesWrapper.Messages.OrderByDescending(x => x.Moment));

            Message = new PriceEngineeringTaskMessageWrapper(new PriceEngineeringTaskMessage
            {
                Author = unitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id),
                Message = string.Empty
            });

            SendMessageCommand = new DelegateLogCommand(
                () =>
                {
                    var message = new PriceEngineeringTaskMessage
                    {
                        Author = this.Message.Author.Model,
                        Message = this.Message.Message
                    };
                    var messageWrapper = new PriceEngineeringTaskMessageWrapper(message);

                    //если задача уже сохранена в базе данных
                    if (unitOfWork.Repository<PriceEngineeringTask>().GetById(viewModel.Model.Id) != null)
                    {
                        TaskMessagesWrapper.Messages.Add(messageWrapper);
                        TaskMessagesWrapper.AcceptChanges();
                        unitOfWork.SaveChanges();

                        container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskSendMessageEvent>().Publish(messageWrapper.Model);
                    }
                    //если задача не сохранена
                    else
                    {
                        this.SendedMessageInNewTask?.Invoke(message.Author.Id, message.Moment, message.Message);
                    }

                    this.Message.Message = string.Empty;
                },
                () => 
                    AllowTexting && 
                    Message != null && 
                    Message.IsValid &&
                    Message.IsChanged && 
                    string.IsNullOrWhiteSpace(Message.Message) == false);

            Message.PropertyChanged += (sender, args) => SendMessageCommand.RaiseCanExecuteChanged();
            viewModel.PropertyChanged += (sender, args) =>
            {
                SendMessageCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged(nameof(AllowTexting));
            };

            //viewModel.TaskIsStarted += () => this.TaskMessagesWrapper = new PriceEngineeringTaskMessagesWrapper(unitOfWork.Repository<PriceEngineeringTask>().GetById(viewModel.Id));

            //синхронизация показа сообщений
            viewModel.Messages.CollectionChanged += MessagesOnCollectionChanged;
        }

        private void MessagesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                var msgs = this.MessagesToShow.ToList();
                MessagesToShow.Clear();
                foreach (var messageWrapper in args.NewItems.Cast<PriceEngineeringTaskMessageWrapper>())
                {
                    msgs.Add(messageWrapper);
                }

                MessagesToShow.AddRange(msgs.OrderByDescending(x => x.Moment));
            }
        }
    }
}