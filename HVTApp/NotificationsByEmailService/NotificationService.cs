using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Events.EventServiceEvents;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace NotificationsService
{
    public class NotificationService : INotificationService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventAggregator _eventAggregator;
        private readonly ISendNotificationThroughApp _sendNotificationThroughApp;
        private readonly IEmailService _emailService;

        public NotificationService(IUnityContainer container)
        {
            _sendNotificationThroughApp = container.Resolve<ISendNotificationThroughApp>();
            _unitOfWork = container.Resolve<IUnitOfWork>();
            _eventAggregator = container.Resolve<IEventAggregator>();
            _emailService = container.Resolve<IEmailService>();
        }

        public void Start()
        {
            _eventAggregator.GetEvent<PriceEngineeringTaskNotificationEvent>()
                .Subscribe(OnPriceEngineeringTaskNotificationEvent, true);
        }

        private void OnPriceEngineeringTaskNotificationEvent(NotificationArgsPriceEngineeringTask args)
        {
            foreach (var item in args.EventServiceItems)
            {
                if (_sendNotificationThroughApp.SendNotification(args, item) == false)
                {
                    //Если уведомление не дошло внутри приложения,
                    //сохраняем уведомление в базе данных
                    this.SaveNotificationInDataBase(args, item);

                    //отправляем уведомление по email
                    var recipientEmailAddress = item.User.Employee.Email;
                    if(string.IsNullOrEmpty(recipientEmailAddress) == false)
                        _emailService.SendMail(recipientEmailAddress, "test", "test");
                }
            }
        }

        /// <summary>
        /// Сохранение уведомления в базе данных
        /// </summary>
        /// <param name="args"></param>
        /// <param name="item"></param>
        private void SaveNotificationInDataBase(NotificationArgsPriceEngineeringTask args, NotificationItem item)
        {
            var unit = new EventServiceUnit
            {
                User = _unitOfWork.Repository<User>().GetById(item.User.Id),
                Role = item.Role,
                Message = item.Message,
                TargetEntityId = args.Entity.Id,
                EventServiceActionType = EventServiceActionType.PriceEngineeringTaskNotification
            };
            _unitOfWork.SaveEntity(unit);
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
