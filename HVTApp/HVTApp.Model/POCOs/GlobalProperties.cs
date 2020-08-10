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


        [Designation("Признак услуги"), Required, OrderStatus(301)]
        public virtual Parameter ServiceParameter { get; set; }

        [Designation("Признак шеф-монтажа"), Required, OrderStatus(302)]
        public virtual Parameter SupervisionParameter { get; set; }

        [Designation("Группа параметров номинального напряжения"), Required, OrderStatus(303)]
        public virtual ParameterGroup VoltageGroup { get; set; }

        [Designation("Группа параметров материала изоляции"), OrderStatus(304)]
        public virtual ParameterGroup IsolationMaterialGroup { get; set; }

        [Designation("Группа параметров цвета изоляции"), OrderStatus(305)]
        public virtual ParameterGroup IsolationColorGroup { get; set; }

        [Designation("Группа параметров ДПУ изолятора"), OrderStatus(306)]
        public virtual ParameterGroup IsolationDpuGroup { get; set; }



        [Designation("Группа параметров обозначения комплекта или детали"), Required, OrderStatus(-50)]
        public virtual ParameterGroup ComplectDesignationGroup{ get; set; }

        [Designation("Параметр комплекты и детали"), Required, OrderStatus(-50)]
        public virtual Parameter ComplectsParameter { get; set; }

        [Designation("Группа типа комплекта или детали"), Required, OrderStatus(-50)]
        public virtual ParameterGroup ComplectsGroup { get; set; }




        [Designation("Получатель писем по ШМ")]
        public virtual Employee RecipientSupervisionLetterEmployee { get; set; }

        [Designation("Отправитель ТКП"), Required]
        public virtual Employee SenderOfferEmployee { get; set; }

        [Designation("Производитель ВВА"), Required]
        public virtual ActivityField HvtProducersActivityField { get; set; }

        [Designation("Стандартные условия оплаты"), Required]
        public virtual PaymentConditionSet PaymentConditionSet { get; set; }

        [Designation("Путь к папке с запросами")]
        public string IncomingRequestsPath { get; set; }

        [Designation("Путь к папке с приложениями Directum")]
        public string DirectumAttachmentsPath { get; set; }

        [Designation("Разработчик")]
        public virtual User Developer { get; set; }

        [Designation("Дата последнего визита разработчика")]
        public virtual DateTime? LastDeveloperVizit { get; set; }

        [Designation("Дополнительное оборудование")]
        public virtual Product ProductIncludedDefault { get; set; }

    }
}