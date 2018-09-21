using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("����������� ��������")]
    public partial class ProductDesignation : BaseEntity
    {
        [Designation("�����������"), Required, MaxLength(50), OrderStatus(10)]
        public string Designation { get; set; }

        [Designation("���������")]
        public virtual List<Parameter> Parameters { get; set; }
    }
}