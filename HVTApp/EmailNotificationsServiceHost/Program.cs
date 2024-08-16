using System;
using System.Data.Entity;
using System.Threading;
using EmailNotificationsService;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Services.EmailService;
using HVTApp.Services.ProductDesignationService;
using Microsoft.Practices.Unity;
using NotificationsReportsService;
using NotificationsService;
using Prism.Regions;

namespace EmailNotificationsServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();
            RegisterTypes(container);

            GlobalAppProperties.ProductDesignationService = container.Resolve<IProductDesignationService>();
            GlobalAppProperties.User = new User();

            var emailNotificationsService = container.Resolve<IEmailNotificationsService>();
            emailNotificationsService.SuccessSendNotificationEvent += unit =>
            {
                PrintMsg($"success: {unit.RecipientUser?.Employee.Email}; {unit.ActionType}");
            };

            emailNotificationsService.NotSuccessSendNotificationEvent += (unit, exception) =>
            {
                PrintMsg($"exception: {unit.RecipientUser?.Employee.Email}; {unit.ActionType}");
                PrintMsg(exception.ToString());
            };
            
            var notificationsReportService = container.Resolve<INotificationsReportService>();
            notificationsReportService.MessageEvent += s =>
            {
                PrintMsg(s);
            };

            while (true)
            {
                PrintMsg("");
                PrintMsg("Start");

                emailNotificationsService.SendNotifications();
                PrintMsg("SendNotifications end");
                notificationsReportService.SendReports();
                PrintMsg("SendReports end");

                var sleepTime = GetSleepTime();
                PrintMsg($"Finish. Sleep time: {sleepTime} min zZzZ");
                Thread.Sleep(new TimeSpan(0, 0, sleepTime, 0));
            }
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<DbContext, HvtAppContext>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IEmailService, MailKitService>();
            //container.RegisterType<INotificationGeneratorService, NotificationGeneratorService>();
            container.RegisterType<IUnitOfWorkFactory, UnitOfWorkFactory>(new ContainerControlledLifetimeManager());
            container.RegisterType<INotificationTextService, NotificationTextService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IProductDesignationService, ProductDesignator>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmailNotificationsService, EmailNotificationsService1>();
            container.RegisterType<INotificationsReportService, NotificationsReportService>();
        }

        private static int GetSleepTime()
        {
            var now = DateTime.Now;
            if (now.DayOfWeek == DayOfWeek.Saturday) return 60;
            if (now.DayOfWeek == DayOfWeek.Sunday) return 60;
            if (now.Hour > 19) return 60;
            if (now.Hour < 7) return 60;

            return 10;
        }

        static void PrintMsg(string msg)
        {
            var now = DateTime.Now;
            Console.WriteLine($"[{now.ToShortDateString()} {now.ToShortTimeString()}] {msg}");
        }
    }
}
