namespace HVTApp.DataAccess
{
    public partial class NotificationsReportsSettingsConfiguration
    {
        public NotificationsReportsSettingsConfiguration()
        {
            HasMany(settings => settings.ChiefEngineerReportDistributionList).WithMany();
            HasMany(settings => settings.SavePaymentDocumentDistributionList).WithMany();
        }
    }
}