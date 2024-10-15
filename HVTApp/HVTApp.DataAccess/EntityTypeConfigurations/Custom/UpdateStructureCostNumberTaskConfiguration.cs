namespace HVTApp.DataAccess
{
    public partial class UpdateStructureCostNumberTaskConfiguration
    {
        public UpdateStructureCostNumberTaskConfiguration()
        {
            HasRequired(x => x.ProductBlock).WithMany().WillCascadeOnDelete(false);
        }
    }
}