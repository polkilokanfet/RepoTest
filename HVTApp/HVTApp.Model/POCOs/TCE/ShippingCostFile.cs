using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("���� ������� ������������ ������")]
    public partial class ShippingCostFile : BaseEntity
    {
        public virtual Guid TechnicalRequrementsTaskId { get; set; }

        [Designation("������"), Required, OrderStatus(90)]
        public DateTime Moment { get; set; } = DateTime.Now;
    }
}