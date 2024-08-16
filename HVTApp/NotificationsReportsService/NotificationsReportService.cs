using System;
using System.Linq;
using System.Threading;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Services;

namespace NotificationsReportsService
{
    public class NotificationsReportService : INotificationsReportService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IEmailService _emailService;
        private readonly INotificationTextService _notificationTextService;


        public NotificationsReportService(IUnitOfWorkFactory unitOfWorkFactory, IEmailService emailService, INotificationTextService notificationTextService)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _emailService = emailService;
            _notificationTextService = notificationTextService;
            //_notificationGeneratorService = notificationGeneratorService;
        }

        private bool CanStart(NotificationsReportsSettings settings, DateTime now)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || 
                DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                return false;

            if (settings.ChiefEngineerReportMoment.AddDays(1) > now)
                return false;

            return true;
        }

        public void SendReports()
        {
            using (var unitOfWork = _unitOfWorkFactory.GetUnitOfWork())
            {
                var settings = unitOfWork.Repository<NotificationsReportsSettings>().GetAll().FirstOrDefault();
                if (settings == null) return;
                var now = DateTime.Now;
                if (CanStart(settings, now) == false) return;

                //if (settings.ChiefEngineerReportDistributionList.Any())
                //    this.GetAndSendChiefEngineerReport(settings, now);

                this.GetAndSendDeadlineReports(now, unitOfWork);

                settings.ChiefEngineerReportMoment = now;
                unitOfWork.SaveChanges();
            }
        }

        public event Action<string> MessageEvent;

        private void GetAndSendChiefEngineerReport(NotificationsReportsSettings settings, DateTime now)
        {
            var subject = $"[УП ВВА] Отчёт для ОГК НВВА ({settings.ChiefEngineerReportMoment} - {now})";
            var report =
                new ChiefEngineerReport(_unitOfWorkFactory.GetUnitOfWork(), settings.ChiefEngineerReportMoment, now).GetReport();
            if (string.IsNullOrWhiteSpace(report)) return;
            foreach (var user in settings.ChiefEngineerReportDistributionList)
            {
                var email = user.Employee.Email;
                if (string.IsNullOrWhiteSpace(email)) continue;
                try
                {
                    _emailService.SendMail(email, subject, report);
                }
                catch
                {
                }
            }
        }

        private void GetAndSendDeadlineReports(DateTime moment, IUnitOfWork unitOfWork)
        {
            var tasks = unitOfWork.Repository<PriceEngineeringTask>().Find(task =>
                task.UserConstructor != null &&
                task.UserConstructor.IsActual &&
                task.IsStarted &&
                task.IsFinishedByConstructor == false &&
                task.GetDeadline(unitOfWork).Value < moment &&
                task.GetTopPriceEngineeringTask(unitOfWork).SalesUnits.Any());

            foreach (var task in tasks)
            {
                var email = task.UserConstructor.Employee.Email;
                if (string.IsNullOrWhiteSpace(email))
                {
                    this.MessageEvent?.Invoke($"e-mail of {task.UserConstructor.Login} is empty");
                    continue;
                }

                var notificationUnit = new NotificationUnit
                {
                    ActionType = NotificationActionType.PriceEngineeringTaskInstructToConstructor,
                    TargetEntityId = task.Id
                };
                var report = _notificationTextService.GetCommonInfo(notificationUnit); //DeadlineReport.GetReport(_unitOfWork, task);

                try
                {
                    _emailService.SendMail(email, "[УП ВВА] Истек срок проработки блока ТСП", report);
                    this.MessageEvent?.Invoke($"success: {email}; SendDeadlineReport");
                }
                catch (Exception e)
                {
                    this.MessageEvent?.Invoke($"exception: {email}; SendDeadlineReport");
                    this.MessageEvent?.Invoke(e.ToString());
                }

                Thread.Sleep(new TimeSpan(0,0,0,5));
            }
        }
    }
}
