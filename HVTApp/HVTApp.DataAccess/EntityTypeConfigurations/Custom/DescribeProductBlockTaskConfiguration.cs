namespace HVTApp.DataAccess
{
    public partial class DescribeProductBlockTaskConfiguration
    {
        public DescribeProductBlockTaskConfiguration()
        {
            HasRequired(x => x.Product).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.ProductBlock).WithMany().WillCascadeOnDelete(false);
        }
    }
}