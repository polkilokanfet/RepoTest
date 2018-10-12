using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Единица ТКП")]
    [DesignationPlural("Единицы ТКП")]
    public partial class OfferUnit : BaseEntity, IUnitPoco, ICloneable
    {
        [Designation("Стоимость"), Required]
        public double Cost { get; set; }

        [Designation("Стоимость доставки")]
        public double? CostDelivery { get; set; }

        [Designation("Стоимость доставки включена в основную стоимость")]
        public bool CostDeliveryIncluded { get; set; } = true;

        [Designation("ТКП"), Required]
        public virtual Offer Offer { get; set; }

        [Designation("Объект"), Required]
        public virtual Facility Facility { get; set; }

        [Designation("Продукт"), Required]
        public virtual Product Product { get; set; }

        [Designation("Условия оплаты"), Required]
        public virtual PaymentConditionSet PaymentConditionSet { get; set; }

        [Designation("Срок производства"), Required]
        public int ProductionTerm { get; set; }

        [Designation("Включенные продукты")]
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