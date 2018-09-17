namespace HVTApp.DataAccess
{
    public partial class ParameterConfiguration
    {
        public ParameterConfiguration()
        {
            HasRequired(x => x.ParameterGroup).WithMany();
            HasMany(x => x.ParameterRelations).WithRequired().WillCascadeOnDelete(false);
        }
    }
}