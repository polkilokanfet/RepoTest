namespace HVTApp.DataAccess
{
    public partial class DirectumTaskMessageConfiguration
    {
        public DirectumTaskMessageConfiguration()
        {
            HasRequired(x => x.Author).WithMany().WillCascadeOnDelete(false);
            Property(x => x.Message).IsRequired().HasMaxLength(1000);
        }
    }
}