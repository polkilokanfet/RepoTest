using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.Events.EventServiceEvents;
using HVTApp.Model.Events.NotificationArgs;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Microsoft.Practices.Unity;
using NotificationsMainService.SyncEntities;
using Prism.Events;

namespace NotificationsMainService
{
    public class NotificationMainService : INotificationMainService, IDisposable
    {
        public IEventServiceClient EventServiceClient { get; }

        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventAggregator _eventAggregator;
        private readonly ISendNotificationThroughApp _sendNotificationThroughApp;
        private readonly INotificationFromDataBaseService _notificationFromDataBaseService;
        private readonly INotificationsReportService _notificationsReportService;
        private readonly INotificationUnitWatcher _notificationUnitWatcher;
        private readonly IEmailService _emailService;
        private readonly SyncUnitsContainer _syncUnitsContainer = new SyncUnitsContainer();

        public NotificationMainService(IUnityContainer container)
        {
            _unitOfWork = container.Resolve<IUnitOfWork>();
            _eventAggregator = container.Resolve<IEventAggregator>();
            _sendNotificationThroughApp = container.Resolve<ISendNotificationThroughApp>();
            _notificationFromDataBaseService = container.Resolve<INotificationFromDataBaseService>();
            _notificationsReportService = container.Resolve<INotificationsReportService>();
            _notificationUnitWatcher = container.Resolve<INotificationUnitWatcher>();
            _emailService = container.Resolve<IEmailService>();
            EventServiceClient = container.Resolve<IEventServiceClient>();

            var types = this.GetType().Assembly.GetTypes().Where(x => x.IsAbstract == false && x.GetInterfaces().Contains(typeof(ISyncUnit)));
            foreach (var unitType in types)
            {
                _syncUnitsContainer.Add((ISyncUnit)container.Resolve(unitType));
            }
        }

        public void Start()
        {
            _notificationsReportService.SendReports();
            _notificationUnitWatcher.Start();
            this.EventServiceClient.Start();

            //подписка на уведомления о событиях в ТСП
            _eventAggregator.GetEvent<NotificationEvent>().Subscribe(OnNotificationEvent, true);

            //подписка на сохранение платежного документа
            _eventAggregator.GetEvent<AfterSavePaymentDocumentEvent>().Subscribe(OnAfterSavePaymentDocumentEvent, true);
        }

        #region OnPriceEngineeringTaskNotificationEvent

        private void OnNotificationEvent(NotificationUnit notification)
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
                    _notificationFromDataBaseService.SaveNotificationInDataBase(notification); //сохраняем уведомление в базе данных
                    //SendNotificationByEmail(notification); //отправляем уведомление по email
                });
        }

        /// <summary>
        /// Отправляем уведомление по email
        /// </summary>
        /// <param name="notification"></param>
        private void SendNotificationByEmail(NotificationAboutPriceEngineeringTaskEventArg notification)
        {
            var recipientEmailAddress = notification.RecipientUser.Employee.Email;
            if (string.IsNullOrEmpty(recipientEmailAddress) == false)
            {
                var subject = $"[Уведомление из УП ВВА] ТСП на новом этапе: {notification.PriceEngineeringTask.Status.Description}";
                var message = GetEmailMessageOnPriceEngineeringTaskNotificationEvent(notification);
                Task.Run(() => _emailService.SendMail(recipientEmailAddress, subject, message)).Await(
                    errorCallback: e =>
                    {
                        var logUnit = new LogUnit()
                        {
                            Author = _unitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id),
                            Head = $"SendNotificationByEmail {recipientEmailAddress}",
                            Message = e.PrintAllExceptions()
                        };
                        _unitOfWork.SaveEntity(logUnit);
                    });
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
            var message = GetEmailMessageOnAfterSavePaymentDocumentEvent(paymentDocument, salesUnits);

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

        private string GetEmailMessageOnAfterSavePaymentDocumentEvent(PaymentDocument paymentDocument, IEnumerable<SalesUnit> salesUnits)
        {
            var units = salesUnits.ToList();
            var sb = new StringBuilder();
            sb.AppendLine($"Платежный документ №{paymentDocument.Number} от {paymentDocument.Date.ToShortDateString()} г.");
            sb.AppendLine($"Сумма с НДС: {paymentDocument.SumWithVat:N} руб.");
            sb.AppendLine($"Менеджер: {units.GroupBy(x => x.Project.Manager).Select(x => x.Key.Employee.Person).ToStringEnum()}");
            sb.AppendLine($"Контрагент: {units.Where(x => x.Specification != null).GroupBy(x => x.Specification).Select(x => x.Key.Contract.Contragent).ToStringEnum()}");
            sb.AppendLine($"Договор: {units.Where(x => x.Specification != null).GroupBy(x => x.Specification).Select(x => x.Key.Contract.Number).ToStringEnum()}");
            sb.AppendLine($"Спецификация: {units.Where(x => x.Specification != null).GroupBy(x => x.Specification).Select(x => $"{x.Key.Number} от {x.Key.Date.ToShortDateString()}").ToStringEnum()}");
            sb.AppendLine("За позиции:");
            foreach (var salesUnit in units)
            {
                sb.AppendLine($" - з/з: {salesUnit.Order?.Number}; поз.: {salesUnit.OrderPosition}; Объект: {salesUnit.Facility}; Наименование: {salesUnit.Product}");
            }

            return sb.ToString();
        }

        #endregion


        ///// <summary>
        ///// Публикация события синхронизации только внутри текущего приложения (для текущего пользователя приложения)
        ///// </summary>
        ///// <typeparam name="TModel"></typeparam>
        ///// <typeparam name="TEvent"></typeparam>
        ///// <param name="model"></param>
        //public bool PublishWithinAppForCurrentUser<TModel, TEvent>(TModel model)
        //    where TModel : BaseEntity
        //    where TEvent : PubSubEvent<TModel>
        //{
        //    if (model == null)
        //    {
        //        throw new ArgumentNullException(nameof(model));
        //    }

        //    //поиск целевого контейнера
        //    var targetSyncUnit = _syncUnitsContainer.GetSyncUnit(typeof(TModel), typeof(TEvent));

        //    //если пользователь текущего приложения является целевым для этого события
        //    if (((ITargetUser<TModel>)targetSyncUnit).CurrentUserIsTargetForNotification(model))
        //    {
        //        //переводим в основной поток
        //        System.Windows.Application.Current.Dispatcher.Invoke(
        //            () =>
        //            {
        //                //публикуем событие
        //                targetSyncUnit.PublishWithinApp(model);
        //            });

        //        return true;
        //    }

        //    return false;
        //}

        public void Dispose()
        {
            _syncUnitsContainer.Dispose();
            _unitOfWork.Dispose();
        }
    }
}