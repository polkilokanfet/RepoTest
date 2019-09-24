using System.ComponentModel.DataAnnotations;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("�����")]
    [DesignationPlural("������")]
    [AllowEdit(Role.SalesManager)]
    public partial class Address : BaseEntity
    {
        [Designation("��������"), Required, MaxLength(100)]
        public string Description { get; set; }

        [Designation("���������� �����"), Required, OrderStatus(5)]
        public virtual Locality Locality { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(this.Locality.Region.District.Country.Name).Append(", ");
            sb.Append(this.Locality.Region.District.Name).Append(", ");
            sb.Append(this.Locality.Region.Name).Append(", ");
            sb.Append(this.Locality.Name).Append(", ");
            sb.Append(Description);

            return sb.ToString();
        }

    }
}