using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Тендер")]
    [DesignationPlural("Тендеры")]
    public partial class Tender : BaseEntity
    {
        [Designation("Проект")]
        public virtual Project Project { get; set; }

        [Designation("Типы")]
        public virtual List<TenderType> Types { get; set; } = new List<TenderType>();

        [Designation("Открытие")]
        public DateTime DateOpen { get; set; }

        [Designation("Закрытие")]
        public DateTime DateClose { get; set; }

        [Designation("Итоги")]
        public DateTime? DateNotice { get; set; }

        [Designation("Участники")]
        public virtual List<Company> Participants { get; set; } = new List<Company>();

        [Designation("Победитель")]
        public virtual Company Winner { get; set; }

        public override string ToString()
        {
            return $"Tender {Types} of {Project}";
        }
    }

    [Designation("Тип тендера")]
    [DesignationPlural("Типы тендера")]
    public partial class TenderType : BaseEntity
    {
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