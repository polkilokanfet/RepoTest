using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.Model.POCOs
{
    [Designation("������� ���")]
    [DesignationPlural("������� ���")]
    public class OfferUnit : BaseEntity, IUnit
    {
        [Designation("���������")]
        public double Cost { get; set; }

        [Designation("���")]
        public virtual Offer Offer { get; set; }


        [Designation("�������")]
        public virtual Product Product { get; set; }

        [Designation("���������� ��������")]
        public virtual List<ProductIncluded> ProductsIncluded { get; set; } = new List<ProductIncluded>();

        [Designation("������")]
        public virtual List<Service> Services { get; set; } = new List<Service>();

        [Designation("������")]
        public virtual Facility Facility { get; set; }

        [Designation("������� ������")]
        public virtual PaymentConditionSet PaymentConditionSet { get; set; }

        [Designation("���� ������������")]
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