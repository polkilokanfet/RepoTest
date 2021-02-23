namespace HVTApp.DataAccess
{
    public partial class DirectumTaskMessageConfiguration
    {
        public DirectumTaskMessageConfiguration()
        {
            HasRequired(directumTaskMessage => directumTaskMessage.Author).WithMany().WillCascadeOnDelete(false);
            Property(directumTaskMessage => directumTaskMessage.Message).IsRequired().HasMaxLength(5000);
        }
    }
}