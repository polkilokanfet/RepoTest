using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Конкурс")]
    [DesignationPlural("Конкурсы")]
    public partial class Tender : BaseEntity
    {
        [Designation("Ссылка"), OrderStatus(1)]
        public virtual string Link { get; set; }

        [Designation("Проект"), Required, OrderStatus(4)]
        public virtual Project Project { get; set; }

        [Designation("Типы"), OrderStatus(11), Required]
        public virtual List<TenderType> Types { get; set; } = new List<TenderType>();

        [Designation("Открытие"), OrderStatus(9)]
        public DateTime DateOpen { get; set; }

        [Designation("Закрытие"), OrderStatus(8)]
        public DateTime DateClose { get; set; }

        [Designation("Итоги"), OrderStatus(7)]
        public DateTime? DateNotice { get; set; }

        [Designation("Участники"), OrderStatus(6)]
        public virtual List<Company> Participants { get; set; } = new List<Company>();

        [Designation("Победитель"), OrderStatus(5)]
        public virtual Company Winner { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Конкурс: {Types}");
            Types.ForEach(x => sb.Append($"{x.Name}; "));
            return sb.ToString();
        }
    }

    [Designation("Тип тендера")]
    [DesignationPlural("Типы тендера")]
    public partial class TenderType : BaseEntity
    {
        [Designation("Название"), Required, OrderStatus(4), MaxLength(50)]
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