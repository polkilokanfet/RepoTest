using System;

namespace HVTApp.Infrastructure.Services
{
    public interface INotificationsReportService
    {
        void SendReports();
        event Action<string> MessageEvent;
    }
}