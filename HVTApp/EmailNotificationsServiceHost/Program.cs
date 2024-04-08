using System;
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
        private static readonly int SleepTime = 5;

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
                Console.WriteLine("+++++");
                PrintMsg($"success: {unit.RecipientUser?.Employee.Email}; {unit.ActionType}");
            };

            emailNotificationsService.NotSuccessSendNotificationEvent += (unit, exception) =>
            {
                Console.WriteLine("-----");
                PrintMsg($"exception: {unit.RecipientUser?.Employee.Email}; {unit.ActionType}");
                Console.WriteLine(exception.ToString());
            };

            while (true)
            {
                PrintMsg("Start");
                emailNotificationsService.SendNotifications();
                PrintMsg($"Finish. Sleep time: {SleepTime} min zZzZ");
                Thread.Sleep(new TimeSpan(0, 0, SleepTime, 0));
            }
        }

        static void PrintMsg(string msg)
        {
            Console.WriteLine($"[{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}] {msg}");
        }
    }
}
