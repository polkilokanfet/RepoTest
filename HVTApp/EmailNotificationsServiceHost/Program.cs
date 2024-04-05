using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Threading;
using EmailNotificationsService;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Services.EmailService;
using HVTApp.Services.ProductDesignationService;
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
            container.RegisterType<IUnitOfWorkFactory, UnitOfWorkFactory>(new ContainerControlledLifetimeManager());
            container.RegisterType<INotificationTextService, NotificationTextService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmailNotificationsService, EmailNotificationsService1>();
            container.RegisterType<IProductDesignationService, ProductDesignator>(new ContainerControlledLifetimeManager());

            GlobalAppProperties.ProductDesignationService = container.Resolve<IProductDesignationService>();
            GlobalAppProperties.User = new User();

            var emailNotificationsService = container.Resolve<IEmailNotificationsService>();
            emailNotificationsService.SuccessSendNotificationEvent += unit =>
            {
                Console.WriteLine($"[{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}] + success: {unit.RecipientUser?.Employee.Email}; {unit.ActionType}");
            };

            emailNotificationsService.NotSuccessSendNotificationEvent += (unit, exception) =>
            {
                Console.WriteLine($"[{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}] - exception: {unit.RecipientUser?.Employee.Email}; {unit.ActionType}");
                Console.WriteLine(exception.ToString());
            };

            while (true)
            {
                emailNotificationsService.SendNotifications();
                Console.WriteLine($"[{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}] sleep 5 min zZzZ");
                Thread.Sleep(new TimeSpan(0, 0, 5, 0));
            }
        }

    }
}
