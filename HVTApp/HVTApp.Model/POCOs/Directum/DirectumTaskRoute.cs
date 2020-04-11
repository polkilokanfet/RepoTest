using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// ������� ������
    /// </summary>
    [Designation("�������")]
    public class DirectumTaskRoute : BaseEntity
    {
        /// <summary>
        /// ����� ��������
        /// </summary>
        [Designation("�����"), OrderStatus(10), Required]
        public virtual List<DirectumTaskRouteItem> Items { get; set; }

        /// <summary>
        /// ������ ������������
        /// </summary>
        [Designation("��������������"), OrderStatus(5)]
        public bool IsParallel { get; set; } = true;
    }
}