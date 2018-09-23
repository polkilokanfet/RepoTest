using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// ����� ������.
    /// </summary>
    [Designation("�����")]
    [AllowEdit(Role.SalesManager)]
    public partial class District : BaseEntity
    {
        [Designation("��������"), Required, MaxLength(50), OrderStatus(10)]
        public string Name { get; set; }

        [Designation("������"), Required]
        public virtual Country Country { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}