namespace HVTApp.DataAccess
{
    public partial class DirectumTaskGroupFileConfiguration
    {
        public DirectumTaskGroupFileConfiguration()
        {
            HasRequired(file => file.Author).WithMany().WillCascadeOnDelete(false);
        }
    }
}