using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Mvvm;

namespace HVTApp.UI.PriceEngineering.Messages
{
    public class PriceEngineeringTaskMessenger : BindableBase
    {
        private readonly PriceEngineeringTaskViewModel _viewModel;
        public PriceEngineeringTaskMessageWrapper Message { get; }

        public PriceEngineeringTaskMessagesWrapper TaskMessagesWrapper { get; }

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
                    TaskMessagesWrapper.Messages.Add(messageWrapper);

                    //если задача уже сохранена в базе данных
                    if (unitOfWork.Repository<PriceEngineeringTask>().GetById(viewModel.Model.Id) != null)
                    {
                        TaskMessagesWrapper.AcceptChanges();
                        unitOfWork.SaveChanges();
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
        }
    }
}