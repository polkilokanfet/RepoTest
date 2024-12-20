using System;
using System.Threading;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;

namespace EmailNotificationsService
{
    public class EmailNotificationsService1 : IEmailNotificationsService
    {
        private readonly IEmailService _emailService;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly INotificationTextService _notificationTextService;

        public EmailNotificationsService1(
            IEmailService emailService, 
            IUnitOfWorkFactory unitOfWorkFactory, 
            INotificationTextService notificationTextService)
        {
            _emailService = emailService;
            _unitOfWorkFactory = unitOfWorkFactory;
            _notificationTextService = notificationTextService;
        }

        public void SendNotifications()
        {
            using (var unitOfWork = _unitOfWorkFactory.GetUnitOfWork())
            {
                var notificationUnits = unitOfWork.Repository<NotificationUnit>().Find(
                    notificationUnit => 
                        notificationUnit.IsSentByEmail == false &&
                        string.IsNullOrWhiteSpace(notificationUnit.RecipientUser.Employee.Email) == false);
                foreach (var notificationUnit in notificationUnits)
                {
                    if (notificationUnit.RecipientUser == null || 
                        notificationUnit.RecipientUser.IsActual == false ||
                        string.IsNullOrWhiteSpace(notificationUnit.RecipientUser.Employee.Email))
                        continue;

                    //отправляем уведомление по email
                    try
                    {
                        var emailAddress = notificationUnit.RecipientUser.Employee.Email;
                        var subject = $"[УП ВВА] {_notificationTextService.GetActionInfo(notificationUnit)}";
                        var body = _notificationTextService.GetCommonInfo(notificationUnit);

                        _emailService.SendMail(emailAddress, subject, body);
                        notificationUnit.IsSentByEmail = true;
                        SuccessSendNotificationEvent?.Invoke(notificationUnit);
                        unitOfWork.SaveChanges();
                    }
                    catch (NotificationUnitHasNoTargetEntityException e)
                    {
                        NotSuccessSendNotificationEvent?.Invoke(notificationUnit, e);
                        unitOfWork.RemoveEntity(notificationUnit);
                        unitOfWork.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        NotSuccessSendNotificationEvent?.Invoke(notificationUnit, e);
                    }

                    Thread.Sleep(new TimeSpan(0, 0, 0, 5));
                }
            }
        }

        public event Action<NotificationUnit> SuccessSendNotificationEvent;
        public event Action<NotificationUnit, Exception> NotSuccessSendNotificationEvent;
    }
}
