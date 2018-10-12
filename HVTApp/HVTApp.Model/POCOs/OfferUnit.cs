using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("������� ���")]
    [DesignationPlural("������� ���")]
    public partial class OfferUnit : BaseEntity, IUnitPoco, ICloneable
    {
        [Designation("���������"), Required]
        public double Cost { get; set; }

        [Designation("��������� ��������")]
        public double? CostDelivery { get; set; }

        [Designation("��������� �������� �������� � �������� ���������")]
        public bool CostDeliveryIncluded { get; set; } = true;

        [Designation("���"), Required]
        public virtual Offer Offer { get; set; }

        [Designation("������"), Required]
        public virtual Facility Facility { get; set; }

        [Designation("�������"), Required]
        public virtual Product Product { get; set; }

        [Designation("������� ������"), Required]
        public virtual PaymentConditionSet PaymentConditionSet { get; set; }

        [Designation("���� ������������"), Required]
        public int ProductionTerm { get; set; }

        [Designation("���������� ��������")]
        public virtual List<ProductIncluded> ProductsIncluded { get; set; } = new List<ProductIncluded>();

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public interface IUnitPoco : IProductCost
    {
        List<ProductIncluded> ProductsIncluded { get; }
        PaymentConditionSet PaymentConditionSet { get; set; }
    }

    public interface IProductCost : IBaseEntity
    {
        Facility Facility { get; set; }
        Product Product { get; set; }
        double Cost { get; set; }
        double? CostDelivery { get; set; }
        bool CostDeliveryIncluded { get; set; }
    }

}