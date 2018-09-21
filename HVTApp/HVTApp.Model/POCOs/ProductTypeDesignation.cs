using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("����������� ���� ��������")]
    public partial class ProductTypeDesignation : BaseEntity
    {
        [Designation("���"), Required, OrderStatus(10)]
        public virtual ProductType ProductType { get; set; }

        [Designation("���������")]
        public virtual List<Parameter> Parameters { get; set; } = new List<Parameter>();
    }
}