using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// �������, ����, ���������� � �.�.
    /// </summary>
    [Designation("������")]
    public partial class Region : BaseEntity
    {
        [Designation("��������"), Required, MaxLength(50), OrderStatus(2)]
        public string Name { get; set; }

        [Designation("�����"), Required, OrderStatus(1)]
        public virtual District District { get; set; }

        public override string ToString()
        {
            return $"{District}, {Name}";
        }
    }
}