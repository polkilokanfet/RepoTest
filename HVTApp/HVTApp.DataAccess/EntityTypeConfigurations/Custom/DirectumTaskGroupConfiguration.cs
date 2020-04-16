namespace HVTApp.DataAccess
{
    public partial class DirectumTaskGroupConfiguration
    {
        public DirectumTaskGroupConfiguration()
        {
            HasRequired(x => x.Author).WithMany().WillCascadeOnDelete(false);
            HasMany(x => x.Observers).WithMany();
            Property(x => x.Priority).IsRequired();
            Property(x => x.Title).IsRequired().HasMaxLength(250);
            Property(x => x.Message).IsRequired().HasMaxLength(1000);
        }
    }
}