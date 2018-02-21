namespace HVTApp.DataAccess
{
    public partial class DescribeProductBlockTaskConfiguration
    {
        public DescribeProductBlockTaskConfiguration()
        {
            HasRequired(x => x.Product).WithMany().HasForeignKey(x => x.ProductId).WillCascadeOnDelete(false);
            HasRequired(x => x.ProductBlock).WithMany().HasForeignKey(x => x.ProductBlockId).WillCascadeOnDelete(false);
        }
    }
}