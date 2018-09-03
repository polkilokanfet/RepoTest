using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.Model.POCOs
{
    [Designation("Единица ТКП")]
    [DesignationPlural("Единицы ТКП")]
    public class OfferUnit : BaseEntity, IUnit
    {
        [Designation("Стоимость")]
        public double Cost { get; set; }

        [Designation("ТКП")]
        public virtual Offer Offer { get; set; }


        [Designation("Продукт")]
        public virtual Product Product { get; set; }

        [Designation("Включенные продукты")]
        public virtual List<ProductIncluded> ProductsIncluded { get; set; } = new List<ProductIncluded>();

        [Designation("Услуги")]
        public virtual List<Service> Services { get; set; } = new List<Service>();

        [Designation("Объект")]
        public virtual Facility Facility { get; set; }

        [Designation("Условия оплаты")]
        public virtual PaymentConditionSet PaymentConditionSet { get; set; }

        [Designation("Срок производства")]
        public int? ProductionTerm { get; set; }
    }

    public interface IUnit : IProductCost
    {
        List<ProductIncluded> ProductsIncluded { get; }
        List<Service> Services { get; }
    }

    public interface IProductCost : IBaseEntity
    {
        Product Product { get; set; }
        double Cost { get; set; }
    }

}