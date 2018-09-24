namespace HVTApp.DataAccess
{
    public partial class CommonOptionConfiguration
    {
        public CommonOptionConfiguration()
        {
            HasRequired(x => x.NewProductParameter).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.NewProductParameterGroup).WithMany().WillCascadeOnDelete(false);
        }
    }
}