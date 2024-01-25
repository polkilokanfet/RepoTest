using System;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Services;

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
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                return false;

            if (settings == null || settings.ChiefEngineerReportDistributionList.Any() == false)
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
                    var subject = $"[Отчёт из УП ВВА] Отчёт для ОГК НВВА ({settings.ChiefEngineerReportMoment} - {now})";
                    var chiefEngineerReport = new ChiefEngineerReport(_unitOfWork, settings.ChiefEngineerReportMoment, now).GetReport();
                    if (string.IsNullOrWhiteSpace(chiefEngineerReport)) return;
                    foreach (var user in settings.ChiefEngineerReportDistributionList)
                    {
                        var email = user.Employee.Email;
                        if (string.IsNullOrWhiteSpace(email)) continue;
                        Task.Run(() => _emailService.SendMail(email, subject, chiefEngineerReport)).Await();
                    }
                }).Await(
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
    }
}
