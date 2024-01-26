using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
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
            var notificationSentThroughApp = true;

            Task.Run(
                () =>
                {
                    //отправка уведомления только через приложение
                    notificationSentThroughApp = _sendNotificationThroughApp.SendNotification(notification);
                }).Await(
                () =>
                {
                    if (notificationSentThroughApp) return;
                    
                    //Если уведомление не дошло внутри приложения,
                    SaveNotificationInDataBase(notification); //сохраняем уведомление в базе данных
                    SendNotificationByEmail(notification); //отправляем уведомление по email
                });
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
                Message = $"{notification.Message}: {notification.PriceEngineeringTask}",
                TargetEntityId = notification.PriceEngineeringTask.Id,
                EventServiceActionType = EventServiceActionType.PriceEngineeringTaskNotification
            };
            _unitOfWork.SaveEntity(unit);
        }

        /// <summary>
        /// Отправляем уведомление по email
        /// </summary>
        /// <param name="notification"></param>
        private void SendNotificationByEmail(NotificationAboutPriceEngineeringTaskEventArg notification)
        {
            var recipientEmailAddress = notification.RecipientUser.Employee.Email;
            if(string.IsNullOrEmpty(recipientEmailAddress) == false)
            {
                var subject = $"[Уведомление из УП ВВА] ТСП на новом этапе: {notification.PriceEngineeringTask.Status.Description}";
                var message = GetEmailMessage(notification);
                Task.Run(() =>  _emailService.SendMail(recipientEmailAddress, subject, message)).Await();
            }
        }

        private string GetEmailMessage(NotificationAboutPriceEngineeringTaskEventArg notification)
        {
            var sb = new StringBuilder();
            sb.AppendLine(notification.PriceEngineeringTask.GetInformationForReport(_unitOfWork));
            sb.AppendLine(string.Empty);
            sb.AppendLine(string.Empty);
            sb.AppendLine($"Автор: {notification.SenderUser.Employee}");
            sb.AppendLine("Уведомление:");
            sb.AppendLine(notification.Message);
            return sb.ToString();
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
