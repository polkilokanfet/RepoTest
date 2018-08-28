using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.Model.POCOs
{
    [Designation("Единица продаж")]
    [DesignationPlural("Единицы продаж")]
    public class SalesUnit : BaseEntity, IUnit
    {
        [Designation("Стоимость")]
        public double Cost { get; set; }


        [Designation("Продукт")]
        public virtual Product Product { get; set; }

        [Designation("Зависимые продукты")]
        public virtual List<ProductDependent> DependentProducts { get; set; } = new List<ProductDependent>();

        public virtual List<Service> Services { get; set; } = new List<Service>();

        [Designation("Объект")]
        public virtual Facility Facility { get; set; }

        [Designation("Условия оплаты")]
        public virtual PaymentConditionSet PaymentConditionSet { get; set; }

        [Designation("Срок производства")]
        public int? ProductionTerm { get; set; }


        #region Проект
        [Designation("Проект")]
        public virtual Project Project { get; set; }

        [Designation("Требуемая дата поставки")]
        public virtual DateTime DeliveryDateExpected { get; set; } = DateTime.Today.AddDays(CommonOptions.ProductionTerm + 30).SkipWeekend(); //требуемая дата поставки

        [Designation("Производитель")]
        public virtual Company Producer { get; set; }

        [Designation("Дата реализации")]
        public virtual DateTime? RealizationDate { get; set; }

        #endregion

        #region Информация о производстве
        [Designation("Заказ")]
        public virtual Order Order { get; set; }

        [Designation("Позиция")]
        public string OrderPosition { get; set; }

        [Designation("Номер")]
        public string SerialNumber { get; set; }

        [Designation("Срок сборки")]
        public int? AssembleTerm { get; set; }

        [Designation("Дата начала производства")]
        public DateTime? StartProductionDate { get; set; }

        [Designation("Дата комплектации")]
        public DateTime? PickingDate { get; set; }

        [Designation("Дата окончания производства")]
        public DateTime? EndProductionDate { get; set; }

        #endregion

        #region Коммерческая информация

        [Designation("Спецификация")]
        public virtual Specification Specification { get; set; }

        [Designation("Совершённые платежи"), NotUpdate(Role.SalesManager | Role.Director)]
        public virtual List<PaymentActual> PaymentsActual { get; set; } = new List<PaymentActual>();

        [Designation("Планируемые платежи")]
        public virtual List<PaymentPlannedList> PaymentsPlannedSaved { get; set; } = new List<PaymentPlannedList>();

        #endregion

        #region Отгрузочная информация
        [Designation("Срок доставки")]
        public int? ExpectedDeliveryPeriod { get; set; }

        [Designation("Адрес доставки")]
        public virtual Address Address { get; set; }

        [Designation("Стоимость доставки")]
        public double CostOfShipment { get; set; } = 0;

        [Designation("Дата отгрузки")]
        public virtual DateTime? ShipmentDate { get; set; }

        [Designation("Дата отгрузки")]
        public virtual DateTime? ShipmentPlanDate { get; set; }

        [Designation("Дата поставки")]
        public virtual DateTime? DeliveryDate { get; set; }

        #endregion

        public override string ToString()
        {
            return $"{Product} для {Facility}";
        }

    }
}