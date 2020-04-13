namespace HVTApp.DataAccess
{
    public partial class DirectumTaskGroupConfiguration
    {
        public DirectumTaskGroupConfiguration()
        {
            HasRequired(x => x.Author).WithMany().WillCascadeOnDelete(false);
            HasMany(x => x.Observers).WithMany();
            Property(x => x.Priority).IsRequired();
            Property(x => x.AttachmentsPath).IsOptional().HasMaxLength(300);
            Property(x => x.AttachmentsPath).IsOptional().HasMaxLength(1000);
        }
    }
}