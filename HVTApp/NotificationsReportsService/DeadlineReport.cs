using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace NotificationsReportsService
{
    internal class DeadlineReport
    {
        public static string GetReport(IUnitOfWork unitOfWork, PriceEngineeringTask task)
        {
            var sb = new StringBuilder();
            //sb.AppendLine(task.GetInformationForReport(unitOfWork));
            //sb.AppendLine(string.Empty);
            //sb.AppendLine(string.Empty);
            //sb.AppendLine("Проработайте, пожалуйста, этот блок.");
            return sb.ToString();
        }
    }
}