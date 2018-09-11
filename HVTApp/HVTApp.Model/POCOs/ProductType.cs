using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("��� ��������")]
    public partial class ProductType : BaseEntity
    {
        [Designation("��������")]
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}