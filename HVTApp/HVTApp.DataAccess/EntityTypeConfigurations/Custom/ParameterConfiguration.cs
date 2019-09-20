namespace HVTApp.DataAccess
{
    public partial class ParameterConfiguration
    {
        public ParameterConfiguration()
        {
            HasRequired(x => x.ParameterGroup).WithMany().WillCascadeOnDelete(false);
            HasMany(x => x.ParameterRelations).WithRequired().HasForeignKey(x => x.ParameterId).WillCascadeOnDelete(false);
        }
    }
}