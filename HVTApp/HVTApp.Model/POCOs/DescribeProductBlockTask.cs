using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class DescribeProductBlockTask : BaseEntity
    {
        public ProductBlock ProductBlock { get; set; }
        public Product Product { get; set; }
    }
}