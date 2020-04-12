namespace HVTApp.DataAccess
{
    public partial class DirectumTaskConfiguration
    {
        public DirectumTaskConfiguration()
        {
            HasRequired(x => x.Author).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.Performer).WithMany().WillCascadeOnDelete(false);
            HasMany(x => x.Observers).WithMany();
            HasOptional(x => x.ParentTask).WithMany().WillCascadeOnDelete(false);
            HasOptional(x => x.PreviousTask).WithMany().WillCascadeOnDelete(false);
            Property(x => x.Priority).IsRequired();
            Property(x => x.AttachmentsPath).IsOptional().HasMaxLength(250);
            HasMany(x => x.Messages).WithRequired().WillCascadeOnDelete(true);
        }
    }
}