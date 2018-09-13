using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.Model.POCOs
{
    [Designation("Единица продаж")]
    [DesignationPlural("Единицы продаж")]
    public partial class SalesUnit : BaseEntity, IUnitPoco, ICloneable
    {
        #region Model

        [Designation("Стоимость"), Required, OrderStatus(45)]
        public double Cost { get; set; }


        [Designation("Продукт"), Required, OrderStatus(50)]
        public virtual Product Product { get; set; }

        [Designation("Включенные продукты"), Required]
        public virtual List<ProductIncluded> ProductsIncluded { get; set; } = new List<ProductIncluded>();

        [Designation("Объект"), OrderStatus(51), Required]
        public virtual Facility Facility { get; set; }

        [Designation("Условия оплаты"), Required]
        public virtual PaymentConditionSet PaymentConditionSet { get; set; }

        [Designation("Срок производства")]
        public int? ProductionTerm { get; set; }


        #region Проект
        [Designation("Проект"), OrderStatus(52), Required]
        public virtual Project Project { get; set; }

        [Designation("Требуемая дата поставки"), Required]
        public virtual DateTime DeliveryDateExpected { get; set; } = DateTime.Today.AddDays(CommonOptions.ProductionTerm + 120).SkipWeekend(); //требуемая дата поставки

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

        [Designation("Сигнал менеджера о производстве")]
        public DateTime? SignalToStartProduction { get; set; }

        [Designation("Дата размещения в производстве")]
        public DateTime? SignalToStartProductionDone { get; set; }

        [Designation("Дата начала производства")]
        public DateTime? StartProductionDate { get; set; }

        [Designation("Дата комплектации")]
        public DateTime? PickingDate { get; set; }

        [Designation("Плановая дата окончания производства")]
        public DateTime? EndProductionPlanDate { get; set; }

        [Designation("Дата окончания производства")]
        public DateTime? EndProductionDate { get; set; }

        #endregion

        #region Коммерческая информация

        [Designation("Спецификация")]
        public virtual Specification Specification { get; set; }

        [Designation("Совершённые платежи"), NotUpdate(Role.SalesManager | Role.Director)]
        public virtual List<PaymentActual> PaymentsActual { get; set; } = new List<PaymentActual>();

        [Designation("Планируемые платежи")]
        public virtual List<PaymentPlanned> PaymentsPlanned { get; set; } = new List<PaymentPlanned>();

        #endregion

        #region Отгрузочная информация
        [Designation("Срок доставки")]
        public int? ExpectedDeliveryPeriod { get; set; }

        //берется из сервиса (по местонахождению объекта)
        [Designation("Срок доставки расчетный"), NotMapped]
        public int? ExpectedDeliveryPeriodCalculated { get; set; }

        [Designation("Адрес доставки")]
        public virtual Address Address { get; set; }

        [Designation("Дата отгрузки")]
        public virtual DateTime? ShipmentDate { get; set; }

        [Designation("Дата плановой отгрузки")]
        public virtual DateTime? ShipmentPlanDate { get; set; }

        [Designation("Дата поставки")]
        public virtual DateTime? DeliveryDate { get; set; }

        #endregion

        [NotMapped, Designation("Разрешение на редактирование стоимости")]
        public bool AllowEditCost => Specification == null;

        [NotMapped, Designation("Разрешение на редактирование техники")]
        public bool AllowEditProduct => SignalToStartProduction == null;

        [NotMapped, Designation("Проиграно")]
        public bool IsLoosen => Producer != null && Producer.Id != CommonOptions.OurCompanyId;

        public override string ToString()
        {
            return $"{Product} для {Facility}";
        }


        #endregion

        #region Func

        /// <summary>
        /// Все платежи (совершенные + плановые).
        /// </summary>
        //public IEnumerable<IPayment> Payments => PaymentsActual.Cast<IPayment>().Union(PaymentsPlannedByConditions);

        #region Суммы
        [Designation("Оплачено?"), NotMapped]
        public bool IsPaid => Math.Abs(SumNotPaid) < 0.0000001;

        /// <summary>
        /// Оплаченная сумма
        /// </summary>
        [Designation("Оплачено"), NotMapped]
        public double SumPaid => PaymentsActual.Sum(x => x.Sum);

        /// <summary>
        /// Неоплаченная сумма
        /// </summary>
        [Designation("Неоплачено"), NotMapped]
        public double SumNotPaid => Cost - SumPaid;


        /// <summary>
        /// Сумама, необходимая для начала производства
        /// </summary>
        [Designation("Сумма старта производства"), NotMapped]
        public double SumToStartProduction => PaymentConditionSet.PaymentConditions.Where(x =>
                                              x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart &&
                                              x.DaysToPoint <= 0).Sum(condition => Cost * condition.Part);

        /// <summary>
        /// Сумма, необходимая для отгрузки
        /// </summary>
        [Designation("Сумма отгрузки"), NotMapped]
        public double SumToShipping => PaymentConditionSet.PaymentConditions.Where(x => (
                                        x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart) ||
                                       (x.PaymentConditionPoint == PaymentConditionPoint.ProductionEnd) ||
                                       (x.PaymentConditionPoint == PaymentConditionPoint.Shipment && x.DaysToPoint <= 0)).
                                       Sum(condition => Cost * condition.Part);


        #endregion

        #region Даты

        [Designation("ОИТ"), NotMapped]
        public DateTime OrderInTakeDate => StartProductionDate ?? StartProductionDateCalculated;

        [Designation("Год"), NotMapped]
        public int OrderInTakeYear => OrderInTakeDate.Year;

        [Designation("Месяц"), NotMapped]
        public int OrderInTakeMonth => OrderInTakeDate.Month;

        /// <summary>
        /// Дата достижения суммы
        /// </summary>
        /// <param name="sumToAchive"></param>
        /// <returns></returns>
        private DateTime? AchiveSumDate(double sumToAchive)
        {
            double sum = 0;
            foreach (var payment in PaymentsActual.OrderBy(x => x.Date))
            {
                sum += payment.Sum;
                if (sumToAchive <= sum) return payment.Date;
            }

            var dic = PaymentConditionsDictionary;
            foreach (var payment in PaymentsPlannedActual.OrderBy(x => x.Date))
            {
                //если пропущены какие-то условия перед сохраненными - выйти
                if (!Equals(dic.First(x => x.Value < 1).Key, payment.Condition))
                {
                    return null;
                }

                sum += payment.Part * payment.Condition.Part * Cost;
                if (sumToAchive <= sum) return payment.Date;

                dic[payment.Condition] += payment.Part;
            }
            return null;
        }

        /// <summary>
        /// Дата исполнения условий для запуска производства
        /// </summary>
        [Designation("Дата исполнения условий для начала производства"), NotMapped]
        public DateTime? StartProductionConditionsDoneDate => AchiveSumDate(SumToStartProduction);

        /// <summary>
        /// Дата исполнения условий для осуществления отгрузки
        /// </summary>
        [Designation("Дата исполнения условий для отгрузки"), NotMapped]
        public DateTime? ShippingConditionsDoneDate => AchiveSumDate(SumToShipping);

        /// <summary>
        /// Расчетная дата начала производства.
        /// </summary>
        [Designation("Начало производства (расч.)"), NotMapped]
        public DateTime StartProductionDateCalculated
        {
            get
            {
                if (StartProductionDate.HasValue) return StartProductionDate.Value;

                //по сигналу менеджера
                if (SignalToStartProduction.HasValue) return SignalToStartProduction.Value;

                //по исполнению условий, необходимых для запуска производства
                if (StartProductionConditionsDoneDate.HasValue) return StartProductionConditionsDoneDate.Value;

                //по дате первого платежа
                if (PaymentsActual.Any()) return PaymentsActual.Select(x => x.Date).Min();

                var productionTerm = this.ProductionTerm ?? CommonOptions.ProductionTerm;

                //по дате доставки оборудования на объект
                if (DeliveryDate.HasValue) return DeliveryDate.Value.AddDays(-productionTerm).AddDays(-DeliveryPeriodCalculated).SkipPastAndWeekend();

                //по необходимой дате реализации проекта
                return DeliveryDateExpected.AddDays(-productionTerm).AddDays(-DeliveryPeriodCalculated).SkipPastAndWeekend();
            }
        }

        /// <summary>
        /// Расчетная дата окончания производства.
        /// </summary>
        [Designation("Окончание производства (расч.)"), NotMapped]
        public DateTime EndProductionDateCalculated
        {
            get
            {
                //по дате производства
                if (EndProductionDate.HasValue) return EndProductionDate.Value;

                //по дате комплектации
                if (PickingDate.HasValue)
                {
                    var assembleTerm = this.AssembleTerm ?? CommonOptions.AssembleTerm;
                    return PickingDate.Value.AddDays(assembleTerm).SkipPastAndWeekend();
                }

                //по дате размещения в производстве (план)
                if (EndProductionPlanDate.HasValue) return EndProductionPlanDate.Value;

                //по сроку производства
                var productionTerm = this.ProductionTerm ?? CommonOptions.ProductionTerm;
                return StartProductionDateCalculated.AddDays(productionTerm).SkipPastAndWeekend();
            }
        }

        /// <summary>
        /// Расчетная дата реализации.
        /// </summary>
        [Designation("Расчетная дата реализации"), NotMapped]
        public DateTime RealizationDateCalculated => RealizationDate ?? DeliveryDateCalculated;

        /// <summary>
        /// Расчетная дата отгрузки.
        /// </summary>
        [Designation("Расчетная дата отгрузки"), NotMapped]
        public DateTime ShipmentDateCalculated
        {
            get
            {
                //по реальной дате отгрузки
                if (ShipmentDate.HasValue) return ShipmentDate.Value;

                //по плановой дате отгрузки
                if (ShipmentPlanDate.HasValue)
                {
                    if (ShippingConditionsDoneDate.HasValue)
                    {
                        if (ShipmentPlanDate.Value >= ShippingConditionsDoneDate &&
                            ShipmentPlanDate.Value >= EndProductionDateCalculated)
                            return ShipmentPlanDate.Value;
                    }
                    else
                    {
                        if (ShipmentPlanDate.Value >= EndProductionDateCalculated)
                            return ShipmentPlanDate.Value;
                    }

                }

                //по дате исполнения условий для отгрузки
                if (ShippingConditionsDoneDate.HasValue &&
                    ShippingConditionsDoneDate >= EndProductionDateCalculated)
                    return ShippingConditionsDoneDate.Value.SkipPastAndWeekend();

                //по дате окончания производства
                return EndProductionDateCalculated.SkipPastAndWeekend();
            }
        }

        /// <summary>
        /// Расчетная дата доставки.
        /// </summary>
        [Designation("Расчетная дата доставки"), NotMapped]
        public DateTime DeliveryDateCalculated
        {
            get
            {
                if (DeliveryDate.HasValue) return DeliveryDate.Value;
                return ShipmentDateCalculated.AddDays(DeliveryPeriodCalculated).SkipPastAndWeekend();
            }
        }

        /// <summary>
        /// Расчетный срок доставки.
        /// </summary>
        [Designation("Расчетный срок доставки"), NotMapped]
        public double DeliveryPeriodCalculated
        {
            get
            {
                //по ожидаемому сроку доставки
                if (ExpectedDeliveryPeriod.HasValue) return ExpectedDeliveryPeriod.Value;

                //по расчетному
                if (ExpectedDeliveryPeriodCalculated.HasValue)
                    return ExpectedDeliveryPeriodCalculated.Value;

                return 3;
            }
        }


        #endregion

        #region Платежи

        /// <summary>
        /// Дата по условию.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private DateTime GetPaymentDate(PaymentCondition condition)
        {
            switch (condition.PaymentConditionPoint)
            {
                case PaymentConditionPoint.ProductionStart:
                    return StartProductionDateCalculated.AddDays(condition.DaysToPoint);
                case PaymentConditionPoint.ProductionEnd:
                    return EndProductionDateCalculated.AddDays(condition.DaysToPoint);
                case PaymentConditionPoint.Shipment:
                    return ShipmentDateCalculated.AddDays(condition.DaysToPoint);
                case PaymentConditionPoint.Delivery:
                    return DeliveryDateCalculated.AddDays(condition.DaysToPoint);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Платежное условие и процент его исполнения
        /// </summary>
        private Dictionary<PaymentCondition, double> PaymentConditionsDictionary => GetConditionsDictionary(SumPaid);

        private Dictionary<PaymentCondition, double> GetConditionsDictionary(double sum)
        {
            var result = new Dictionary<PaymentCondition, double>();

            //если стоимость нулевая или нечего платить - выходим
            if (Cost < 0.00001) return result;

            //оплаченный остаток
            var paidRest = sum;

            //берем все условия и упорядочиваем их
            var conditions = PaymentConditionSet.PaymentConditions.OrderBy(x => x).ToList();

            foreach (var condition in conditions)
            {
                double conditionSum = condition.Part * Cost;
                double paidConditionPart = 0;
                //если остатка хватает на покрытие
                if (paidRest >= conditionSum)
                {
                    paidConditionPart = 1;
                    paidRest -= conditionSum;
                }
                else
                {
                    paidConditionPart = paidRest / conditionSum;
                    paidRest = 0;
                }
                result.Add(condition, paidConditionPart);
            }
            return result;
        }

        /// <summary>
        /// Актуальные плановые платежи. 
        /// !!!Важно!!! Возвращет новые сущности!
        /// </summary>
        [NotMapped]
        public List<PaymentPlanned> PaymentsPlannedActual
        {
            get
            {
                var result = new List<PaymentPlanned>();

                //словарь условий и коэффицента их исполнений
                var dictionary = PaymentConditionsDictionary;

                foreach (var payment in PaymentsPlanned)
                {
                    //если связанное условие существует и еще не исполнено
                    if (dictionary.ContainsKey(payment.Condition) && dictionary[payment.Condition] < 1)
                    {
                        double part = payment.Part;
                        //если остатка хватает
                        if (part + dictionary[payment.Condition] <= 1)
                        {
                            dictionary[payment.Condition] += part;
                        }
                        else
                        {
                            part = 1 - dictionary[payment.Condition];
                            dictionary[payment.Condition] = 1;
                        }

                        //добавляем актуальный плановый платеж
                        result.Add(new PaymentPlanned
                        {
                            Date = payment.Date < DateTime.Today ? DateTime.Today : payment.Date,
                            Part = part,

                            Id = payment.Id,
                            Condition = payment.Condition,
                            Comment = payment.Comment,
                            DateCalculated = payment.DateCalculated
                        });
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// Расчетные плановые платежи (без сохраненных).
        /// </summary>
        [Designation("Расчетные плановые платежи"), NotMapped]
        public List<PaymentPlanned> PaymentsPlannedGenerated
        {
            get
            {
                var dictionary = PaymentConditionsDictionary;
                foreach (var paymentPlanned in PaymentsPlannedActual)
                {
                    dictionary[paymentPlanned.Condition] += paymentPlanned.Part;
                }
                
                var result = new List<PaymentPlanned>();
                foreach (var conditions in dictionary)
                {
                    if (conditions.Value >= 1)
                        continue;

                    result.Add(new PaymentPlanned
                    {
                        Condition = conditions.Key,
                        Date = GetPaymentDate(conditions.Key),
                        Part = 1 - conditions.Value,
                        //Comment = "Сгенерированный платеж"
                    });
                }
                return result;
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion


        #endregion
    }
}