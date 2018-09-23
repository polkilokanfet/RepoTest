using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// ��� ����������� ������.
    /// </summary>
    [Designation("��� ����������� ������")]
    [AllowEdit(Role.SalesManager)]
    public partial class LocalityType : BaseEntity
    {
        [Designation("������ ��������"), Required, MaxLength(50), OrderStatus(2)]
        public string FullName { get; set; }

        [Designation("�����������"), Required, MaxLength(10), OrderStatus(1)]
        public string ShortName { get; set; }

        public override string ToString()
        {
            return $"{FullName}, ({ShortName})";
        }
    }
}