namespace HVTApp.DataAccess
{
    public partial class DirectumTaskConfiguration
    {
        public DirectumTaskConfiguration()
        {
            HasRequired(x => x.Author).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.Route).WithRequiredPrincipal().WillCascadeOnDelete(true);
            HasMany(x => x.Observers).WithMany();
            HasOptional(x => x.ParentTask).WithMany().WillCascadeOnDelete(true);
            Property(x => x.Priority).IsRequired();
            Property(x => x.AttachmentsPath).IsOptional().HasMaxLength(250);
        }
    }
}