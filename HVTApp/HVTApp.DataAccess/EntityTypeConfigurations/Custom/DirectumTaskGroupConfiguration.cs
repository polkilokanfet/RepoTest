namespace HVTApp.DataAccess
{
    public partial class DirectumTaskGroupConfiguration
    {
        public DirectumTaskGroupConfiguration()
        {
            HasRequired(directumTaskGroup => directumTaskGroup.Author).WithMany().WillCascadeOnDelete(false);
            HasMany(directumTaskGroup => directumTaskGroup.Observers).WithMany();
            HasMany(directumTaskGroup => directumTaskGroup.Files).WithRequired().HasForeignKey(file => file.DirectumTaskGroupId).WillCascadeOnDelete(false);
            Property(directumTaskGroup => directumTaskGroup.Priority).IsRequired();
            Property(directumTaskGroup => directumTaskGroup.Title).IsRequired().HasMaxLength(250);
            Property(directumTaskGroup => directumTaskGroup.Message).IsRequired().HasMaxLength(5000);
        }
    }
}