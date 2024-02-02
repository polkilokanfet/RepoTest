using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.Events.EventServiceEvents;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Microsoft.Practices.ObjectBuilder2;
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
            //подписка на уведомления о событиях в ТСП
            _eventAggregator.GetEvent<PriceEngineeringTaskNotificationEvent>()
                .Subscribe(OnPriceEngineeringTaskNotificationEvent, true);

            //подписка на сохранение платежного документа
            _eventAggregator.GetEvent<AfterSavePaymentDocumentEvent>()
                .Subscribe(OnAfterSavePaymentDocumentEvent, true);
        }

        #region OnAfterSavePaymentDocumentEvent

        private void OnAfterSavePaymentDocumentEvent(PaymentDocument paymentDocument)
        {
            if (paymentDocument.Payments.Any() == false) return;

            var salesUnits = new List<SalesUnit>();
            paymentDocument.Payments
                .ForEach(paymentActual => salesUnits.Add(_unitOfWork.Repository<SalesUnit>().GetById(paymentActual.SalesUnitId)));

            var manager = salesUnits.First().Project.Manager;
            var email = manager.Employee.Email;
            var subject = "[Уведомление из УП ВВА] Пришла денюжка!";
            var message = GetEmailMessageOnAfterSavePaymentDocumentEvent(paymentDocument, salesUnits, manager);

            var emails = _unitOfWork.Repository<NotificationsReportsSettings>().GetAll().First().SavePaymentDocumentDistributionList
                .Where(user => string.IsNullOrEmpty(user.Employee.Email) == false)
                .Select(user => user.Employee.Email)
                .ToList();
            if (string.IsNullOrEmpty(email) == false) emails.Add(email);

            foreach (var email1 in emails)
            {
                Task.Run(() => _emailService.SendMail(email1, subject, message)).Await();
            }
        }

        private string GetEmailMessageOnAfterSavePaymentDocumentEvent(PaymentDocument paymentDocument, IEnumerable<SalesUnit> salesUnits, User manager)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Платежный документ №{paymentDocument.Number} от {paymentDocument.Date.ToShortDateString()} г.");
            sb.AppendLine($"Сумма с НДС: {paymentDocument.SumWithVat:N} руб.");
            sb.AppendLine($"Менеджер: {manager.Employee.Person}");
            sb.AppendLine("За позиции:");
            foreach (var salesUnit in salesUnits)
            {
                sb.AppendLine($" - Контрагент:{salesUnit.Specification?.Contract.Contragent}; Договор: {salesUnit.Specification?.Contract.Number}; Спецификация: {salesUnit.Specification?.Number}; Объект: {salesUnit.Facility}; Наименование: {salesUnit.Product}");
            }

            return sb.ToString();
        }

        #endregion

        #region OnPriceEngineeringTaskNotificationEvent

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
                var message = GetEmailMessageOnPriceEngineeringTaskNotificationEvent(notification);
                Task.Run(() =>  _emailService.SendMail(recipientEmailAddress, subject, message)).Await();
            }
        }

        private string GetEmailMessageOnPriceEngineeringTaskNotificationEvent(NotificationAboutPriceEngineeringTaskEventArg notification)
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

        #endregion

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
