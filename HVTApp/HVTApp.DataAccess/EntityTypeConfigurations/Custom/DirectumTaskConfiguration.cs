namespace HVTApp.DataAccess
{
    public partial class DirectumTaskConfiguration
    {
        public DirectumTaskConfiguration()
        {
            HasRequired(x => x.Group).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.Performer).WithMany().WillCascadeOnDelete(false);
            HasOptional(x => x.ParentTask).WithMany().WillCascadeOnDelete(false);
            HasOptional(x => x.PreviousTask).WithMany().WillCascadeOnDelete(false);
            HasMany(x => x.Messages).WithRequired().WillCascadeOnDelete(true);
        }
    }
}