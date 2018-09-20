using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Общие настройки")]
    public class CommonOption : BaseEntity
    {
        [Designation("Дата настроек"), Required]
        public DateTime Date { get; set; } = DateTime.Today;

        [Designation("Наша компания"), Required]
        public virtual Company OurCompany { get; set; }

        [Designation("Срок актуальности себестоимости"), Required]
        public int ActualPriceTerm { get; set; } = 90;

        [Designation("Стандартный срок производства"), Required]
        public int StandartTermFromStartToEndProduction { get; set; } = 120;

        [Designation("Стандартный срок сборки"), Required]
        public int StandartTermFromPickToEndProduction { get; set; } = 7;

        [Designation("Стандартные условия оплаты"), Required]
        public virtual PaymentConditionSet StandartPaymentsConditionSet { get; set; }
    }
}