using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Общие настройки")]
    public class GlobalProperties : BaseEntity
    {
        [Designation("Дата настроек"), Required, OrderStatus(20)]
        public DateTime Date { get; set; } = DateTime.Today;

        [Designation("Наша компания"), Required, OrderStatus(19)]
        public virtual Company OurCompany { get; set; }

        [Designation("Срок актуальности себестоимости"), Required]
        public int ActualPriceTerm { get; set; } = 90;

        [Designation("Стандартный срок производства"), Required]
        public int StandartTermFromStartToEndProduction { get; set; } = 120;

        [Designation("Стандартный срок сборки"), Required]
        public int StandartTermFromPickToEndProduction { get; set; } = 7;

        [Designation("Стандартные условия оплаты"), Required]
        public virtual PaymentConditionSet StandartPaymentsConditionSet { get; set; }

        [Designation("НДС"), Required]
        public double Vat { get; set; } = 20;

        [Designation("Родительский параметр новых параметров"), Required]
        public virtual Parameter NewProductParameter { get; set; }

        [Designation("Группа новых параметров"), Required]
        public virtual ParameterGroup NewProductParameterGroup { get; set; }


        [Designation("Группа параметров номинального напряжения"), Required]
        public virtual ParameterGroup VoltageGroup { get; set; }

        [Designation("Признак услуги"), Required]
        public virtual Parameter ServiceParameter { get; set; }

        [Designation("Признак шеф-монтажа"), Required]
        public virtual Parameter SupervisionParameter { get; set; }

        [Designation("Отправитель ТКП"), Required]
        public virtual Employee SenderOfferEmployee { get; set; }

        [Designation("Производитель ВВА"), Required]
        public virtual ActivityField HvtProducersActivityField { get; set; }

        [Designation("Стандартные условия оплаты"), Required]
        public virtual PaymentConditionSet PaymentConditionSet { get; set; }

        [Designation("Путь к папке с запросами")]
        public string IncomingRequestsPath { get; set; }

        [Designation("Разработчик")]
        public virtual User Developer { get; set; }

        [Designation("Дата последнего визита разработчика")]
        public virtual DateTime? LastDeveloperVizit { get; set; }

        [Designation("Дополнительное оборудование")]
        public virtual Product ProductIncludedDefault { get; set; }

    }
}