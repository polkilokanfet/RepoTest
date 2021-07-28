namespace HVTApp.DataAccess
{
    public partial class LogUnitConfiguration
    {
        public LogUnitConfiguration()
        {
            HasRequired(logUnit => logUnit.Author).WithMany().WillCascadeOnDelete(false);
        }
    }
}