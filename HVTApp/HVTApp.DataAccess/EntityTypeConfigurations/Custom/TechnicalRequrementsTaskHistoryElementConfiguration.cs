namespace HVTApp.DataAccess
{
    public partial class TechnicalRequrementsTaskHistoryElementConfiguration
    {
        public TechnicalRequrementsTaskHistoryElementConfiguration()
        {
            HasOptional(x => x.User).WithMany().WillCascadeOnDelete(false);
        }
    }
}