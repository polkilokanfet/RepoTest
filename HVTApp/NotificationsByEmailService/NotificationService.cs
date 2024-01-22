using System;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
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

        private void OnPriceEngineeringTaskNotificationEvent(NotificationAboutPriceEngineeringTaskEventArg notification)
        {
            if (_sendNotificationThroughApp.SendNotification(notification) == false)
            {
                //Если уведомление не дошло внутри приложения,
                SaveNotificationInDataBase(notification);   //сохраняем уведомление в базе данных
                SendNotificationByEmail(notification);      //отправляем уведомление по email
            }
        }

        /// <summary>
        /// Сохранение уведомления в базе данных
        /// </summary>
        /// <param name="args"></param>
        /// <param name="notification"></param>
        private void SaveNotificationInDataBase(NotificationAboutPriceEngineeringTaskEventArg notification)
        {
            var unit = new EventServiceUnit
            {
                User = _unitOfWork.Repository<User>().GetById(notification.RecipientUser.Id),
                Role = notification.RecipientRole,
                Message = notification.GetMessageSimple(),
                TargetEntityId = notification.PriceEngineeringTask.Id,
                EventServiceActionType = EventServiceActionType.PriceEngineeringTaskNotification
            };
            _unitOfWork.SaveEntity(unit);
        }

        /// <summary>
        /// Отправляем уведомление по email
        /// </summary>
        /// <param name="notification"></param>
        private bool SendNotificationByEmail(NotificationAboutPriceEngineeringTaskEventArg notification)
        {
            var result = false;

            var recipientEmailAddress = notification.RecipientUser.Employee.Email;
            if(string.IsNullOrEmpty(recipientEmailAddress) == false)
            {
                Task.Run(() =>
                {
                    _emailService.SendMail(recipientEmailAddress, notification.PriceEngineeringTask.Status.Description, notification.GetMessageEmail());
                }).Await(
                    () =>
                    {
                        result = true;
                    },
                    e =>
                    {

                    });

            }

            return result;
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
