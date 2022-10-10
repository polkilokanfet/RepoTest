using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
using Prism.Mvvm;

namespace HVTApp.UI.PriceEngineering.Messages
{
    public class PriceEngineeringTaskMessenger : BindableBase
    {
        private readonly PriceEngineeringTaskViewModel _viewModel;
        private PriceEngineeringTaskMessagesWrapper _taskMessagesWrapper;
        public PriceEngineeringTaskMessageWrapper1 Message { get; }

        public ObservableCollection<PriceEngineeringTaskMessageWrapper1> MessagesToShow { get; }

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
                switch (_viewModel.Status)
                {
                    case PriceEngineeringTaskStatusEnum.Stopped:
                    case PriceEngineeringTaskStatusEnum.Accepted:
                        return false;
                }

                return true;
            }
        }

        public DelegateLogCommand SendMessageCommand { get; }

        public PriceEngineeringTaskMessenger(IUnityContainer container, PriceEngineeringTaskViewModel viewModel)
        {
            _viewModel = viewModel;
            var unitOfWork = container.Resolve<IUnitOfWork>();

            var priceEngineeringTask = unitOfWork.Repository<PriceEngineeringTask>().GetById(viewModel.Model.Id) ?? viewModel.Model;
            TaskMessagesWrapper = new PriceEngineeringTaskMessagesWrapper(priceEngineeringTask);
            
            MessagesToShow = new ObservableCollection<PriceEngineeringTaskMessageWrapper1>(TaskMessagesWrapper.Messages.OrderByDescending(x => x.Moment));

            Message = new PriceEngineeringTaskMessageWrapper1(new PriceEngineeringTaskMessage
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
                    var messageWrapper = new PriceEngineeringTaskMessageWrapper1(message);

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
                        viewModel.Messages.Add(messageWrapper); 1
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

            //синхронизация показа сообщений
            viewModel.Messages.CollectionChanged += MessagesOnCollectionChanged;

            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskReciveMessageEvent>().Subscribe(
                message =>
                {
                    if (message.PriceEngineeringTaskId == this._viewModel.Model.Id)
                    {
                        if (MessagesToShow.Select(x => x.Model).ContainsById(message) == false)
                        {
                            MessagesToShow.Insert(0, new PriceEngineeringTaskMessageWrapper1(message));
                        }
                    }
                });
        }

        private void MessagesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                var msgs = this.MessagesToShow.ToList();
                MessagesToShow.Clear();
                msgs.AddRange(args.NewItems.Cast<PriceEngineeringTaskMessageWrapper1>());
                MessagesToShow.AddRange(msgs.OrderByDescending(x => x.Moment));
            }
        }
    }
}