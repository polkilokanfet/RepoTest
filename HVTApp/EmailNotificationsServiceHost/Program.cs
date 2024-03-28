using System;
using System.Data.Entity;
using System.Threading;
using EmailNotificationsService;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model.Services;
using HVTApp.Services.EmailService;
using Microsoft.Practices.Unity;
using NotificationsService;

namespace EmailNotificationsServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<DbContext, HvtAppContext>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IEmailService, MailKitService>();
            container.RegisterType<INotificationGeneratorService, NotificationGeneratorService>();
            container.RegisterType<IUnitOfWorkFactory, UnitOfWorkFactory>();
            container.RegisterType<IEmailNotificationsService, EmailNotificationsService1>();

            var emailNotificationsService = container.Resolve<IEmailNotificationsService>();
            emailNotificationsService.SuccessSendNotificationEvent += unit =>
            {
                Console.WriteLine($" + success: {unit.RecipientUser?.Employee.Email}; {unit.ActionType}");
            };

            emailNotificationsService.NotSuccessSendNotificationEvent += (unit, exception) =>
            {
                Console.WriteLine($" - exception: {unit.RecipientUser?.Employee.Email}; {unit.ActionType}");
                Console.WriteLine(exception.ToString());
            };

            while (true)
            {
                emailNotificationsService.SendNotifications();
                Thread.Sleep(new TimeSpan(0, 0, 5, 0));
            }
        }

    }
}
