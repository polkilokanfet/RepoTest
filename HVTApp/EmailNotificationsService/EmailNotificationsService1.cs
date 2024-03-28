using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;

namespace EmailNotificationsService
{
    public class EmailNotificationsService1
    {
        private readonly IEmailService _emailService;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly INotificationGeneratorService _notificationGeneratorService;

        public EmailNotificationsService1(
            IEmailService emailService, 
            IUnitOfWorkFactory unitOfWorkFactory, 
            INotificationGeneratorService notificationGeneratorService)
        {
            _emailService = emailService;
            _unitOfWorkFactory = unitOfWorkFactory;
            _notificationGeneratorService = notificationGeneratorService;
        }

        private void SendNotifications()
        {
            using (var unitOfWork = _unitOfWorkFactory.GetUnitOfWork())
            {
                var notificationUnits = unitOfWork.Repository<NotificationUnit>().Find(notificationUnit => notificationUnit.IsSentByEmail == false);
                foreach (var notificationUnit in notificationUnits)
                {
                    //отправляем уведомление по email
                    var emailAddress = notificationUnit.RecipientUser?.Employee.Email;
                    //if (string.IsNullOrEmpty(emailAddress)) continue;
                    var subject = $"[УП ВВА] {_notificationGeneratorService.GetActionInfo(notificationUnit)}";
                    var body = _notificationGeneratorService.GetCommonInfo(notificationUnit);
                    try
                    {
                        _emailService.SendMail(emailAddress, subject, body);
                        notificationUnit.IsSentByEmail = true;
                    }
                    catch (Exception e)
                    {
                    }
                }

                unitOfWork.SaveChanges();
            }
        }
    }
}
