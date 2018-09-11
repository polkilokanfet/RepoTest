using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("������� ���")]
    [DesignationPlural("������� ���")]
    public partial class OfferUnit : BaseEntity, IUnitPoco
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
        public virtual Facility Facility { get; set; }

        [Designation("������� ������")]
        public virtual PaymentConditionSet PaymentConditionSet { get; set; }

        [Designation("���� ������������")]
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