using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("�������")]
    [DesignationPlural("��������")]
    public partial class Tender : BaseEntity
    {
        [Designation("������"), OrderStatus(1)]
        public virtual string Link { get; set; }

        [Designation("������"), Required, OrderStatus(4)]
        public virtual Project Project { get; set; }

        [Designation("����"), OrderStatus(11), Required]
        public virtual List<TenderType> Types { get; set; } = new List<TenderType>();

        [Designation("��������"), OrderStatus(9)]
        public DateTime DateOpen { get; set; }

        [Designation("��������"), OrderStatus(8)]
        public DateTime DateClose { get; set; }

        [Designation("�����"), OrderStatus(7)]
        public DateTime? DateNotice { get; set; }

        [Designation("���������"), OrderStatus(6)]
        public virtual List<Company> Participants { get; set; } = new List<Company>();

        [Designation("����������"), OrderStatus(5)]
        public virtual Company Winner { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"�������: {Types}");
            Types.ForEach(x => sb.Append($"{x.Name}; "));
            return sb.ToString();
        }
    }

    [Designation("��� �������")]
    [DesignationPlural("���� �������")]
    public partial class TenderType : BaseEntity
    {
        [Designation("��������"), Required, OrderStatus(4), MaxLength(50)]
        public string Name { get; set; }

        public TenderTypeEnum Type { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public enum TenderTypeEnum
    {
        ToProject,
        ToSupply,
        ToWork
    }

}