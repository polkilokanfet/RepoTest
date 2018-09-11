using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Единица ТКП")]
    [DesignationPlural("Единицы ТКП")]
    public partial class OfferUnit : BaseEntity, IUnitPoco
    {
        [Designation("Стоимость")]
        public double Cost { get; set; }

        [Designation("ТКП")]
        public virtual Offer Offer { get; set; }


        [Designation("Продукт")]
        public virtual Product Product { get; set; }

        [Designation("Включенные продукты")]
        public virtual List<ProductIncluded> ProductsIncluded { get; set; } = new List<ProductIncluded>();

        [Designation("Объект")]
        public virtual Facility Facility { get; set; }

        [Designation("Условия оплаты")]
        public virtual PaymentConditionSet PaymentConditionSet { get; set; }

        [Designation("Срок производства")]
        public int? ProductionTerm { get; set; }
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
    }

}