using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Контракт")]
    [DesignationPlural("Контракты")]
    public partial class Contract : BaseEntity
    {
        [Designation("№"), Required, MaxLength(50)]
        public string Number { get; set; }

        [Designation("Дата"), Required]
        public DateTime Date { get; set; } = DateTime.Today;

        [Designation("Контрагент"), Required]
        public virtual Company Contragent { get; set; }

        public override string ToString()
        {
            return Number;
        }
    }
}