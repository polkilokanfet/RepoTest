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
                Task.Run(() =>
                {
                    var subject = $"[Уведомление из УП ВВА] ТСП на новом этапе: {notification.PriceEngineeringTask.Status.Description}";
                    _emailService.SendMail(recipientEmailAddress, subject, GetEmailMessage(notification));
                }).Await();
            }
        }

        private string GetEmailMessage(NotificationAboutPriceEngineeringTaskEventArg notification)
        {
            var task = notification.PriceEngineeringTask;
            var tasks = task.GetPriceEngineeringTasks(_unitOfWork);
            var taskTop = task.GetTopPriceEngineeringTask(_unitOfWork);
            var salesUnit = taskTop.SalesUnits.FirstOrDefault();

            var sb = new StringBuilder();
            sb.AppendLine($"Номер сборки в УП ВВА: {tasks.NumberFull}");
            sb.AppendLine($"Номер задачи в УП ВВА: {task.Number}");
            sb.AppendLine($"Номер задачи в Team Center: {tasks.TceNumber}");
            sb.AppendLine(string.Empty);

            sb.AppendLine($"Проект: {salesUnit?.Project}");
            sb.AppendLine($"Объект: {salesUnit?.Facility}");
            sb.AppendLine($"Оборудование: {taskTop.ProductBlock}");
            sb.AppendLine($"Блок оборудования: {task.ProductBlock}");
            sb.AppendLine(string.Empty);

            sb.AppendLine($"Бюро ОГК: {task.DesignDepartment}");
            sb.AppendLine($"Исполнитель: {task.UserConstructor}");
            sb.AppendLine($"Менеджер: {tasks.UserManager}");
            sb.AppendLine($"Back-менеджер: {tasks.BackManager}");
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
