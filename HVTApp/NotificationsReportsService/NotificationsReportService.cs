using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model.POCOs;

namespace NotificationsReportsService
{
    public class NotificationsReportService : IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public NotificationsReportService(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public void Start()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday ||
                DateTime.Now.DayOfWeek == DayOfWeek.Sunday) return;

            //_unitOfWork.Repository<PriceEngineeringTask>().Find(x => 
            //    x.IsStarted && x.IsFinishedByDesignDepartment == false)
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
