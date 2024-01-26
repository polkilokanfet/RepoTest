using System;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;

namespace NotificationsReportsService
{
    public class NotificationsReportService : INotificationsReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public NotificationsReportService(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        private bool CanStart(NotificationsReportsSettings settings, DateTime now)
        {
            if (settings == null)
                return false;

            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || 
                DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                return false;

            if (settings.ChiefEngineerReportMoment.AddDays(1) > now)
                return false;

            return true;
        }

        public void Start()
        {
            var settings = _unitOfWork.Repository<NotificationsReportsSettings>().GetAll().FirstOrDefault();
            var now = DateTime.Now;
            if (CanStart(settings, now) == false) return;

            Task.Run(
                    () =>
                    {
                        if (settings != null && settings.ChiefEngineerReportDistributionList.Any())
                            this.GetAndSendChiefEngineerReport(settings, now);

                        this.GetAndSendDeadlineReports(now);
                    })
                .Await(
                () =>
                {
                    settings.ChiefEngineerReportMoment = now;
                    _unitOfWork.SaveChanges();
                    _unitOfWork.Dispose();
                },
                e =>
                {
                    _unitOfWork.Dispose();
                });
        }

        private void GetAndSendChiefEngineerReport(NotificationsReportsSettings settings, DateTime now)
        {
            var subject = $"[Отчёт из УП ВВА] Отчёт для ОГК НВВА ({settings.ChiefEngineerReportMoment} - {now})";
            var chiefEngineerReport =
                new ChiefEngineerReport(_unitOfWork, settings.ChiefEngineerReportMoment, now).GetReport();
            if (string.IsNullOrWhiteSpace(chiefEngineerReport)) return;
            foreach (var user in settings.ChiefEngineerReportDistributionList)
            {
                var email = user.Employee.Email;
                if (string.IsNullOrWhiteSpace(email)) continue;
                Task.Run(() => _emailService.SendMail(email, subject, chiefEngineerReport)).Await();
            }
        }

        private void GetAndSendDeadlineReports(DateTime moment)
        {
            var tasks = _unitOfWork.Repository<PriceEngineeringTask>().Find(task =>
                task.UserConstructor != null &&
                task.IsStarted &&
                task.IsFinishedByConstructor == false &&
                task.GetDeadline(_unitOfWork).Value < moment &&
                task.GetTopPriceEngineeringTask(_unitOfWork).SalesUnits.Any());

            foreach (var task in tasks)
            {
                var email = task.UserConstructor.Employee.Email;
                if (string.IsNullOrWhiteSpace(email)) continue;
                Task.Run(() =>
                {
                    _emailService.SendMail(email, "[Уведомление из УП ВВА] Истек срок проработки блока ТСП", DeadlineReport.GetReport(_unitOfWork, task));
                    _emailService.SendMail("kosolapov_ag@uetm.ru", "[Уведомление из УП ВВА] Истек срок проработки блока ТСП", DeadlineReport.GetReport(_unitOfWork, task));
                }).Await();
            }
        }
    }
}
