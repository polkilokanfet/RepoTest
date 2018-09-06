using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Lookup
{
    public partial class SalesUnitLookup : IUnitLookup
    {
        private List<PaymentConditionPart> _paymentConditionParts;

        public override async Task LoadOther(IUnitOfWork unitOfWork)
        {
            ProductsIncluded = Entity.ProductsIncluded.Select(x => new ProductIncludedLookup(x)).ToList();
            foreach (var productIncluded in ProductsIncluded)
                await productIncluded.LoadOther(unitOfWork);

            PaymentsActual = Entity.PaymentsActual.Select(x => new PaymentActualLookup(x)).ToList();
            foreach (var paymentActualLookup in PaymentsActual)
                await paymentActualLookup.LoadOther(unitOfWork);

            PaymentsPlanned = Entity.PaymentsPlanned.Select(x => new PaymentPlannedLookup(x)).ToList();
            foreach (var paymentPlannedLookup in PaymentsPlanned)
                await paymentPlannedLookup.LoadOther(unitOfWork);
            //проставляем в сохраненных платежах суммы
            PaymentsPlanned.ForEach(x => x.Sum = Cost * x.Part * x.Condition.Part);

            await PaymentConditionSet.LoadOther(unitOfWork);
        }

        [Designation("Совершённые платежи")]
        public List<PaymentActualLookup> PaymentsActual { get; private set; }

        [Designation("Планируемые платежи")]
        public List<PaymentPlannedLookup> PaymentsPlanned { get; private set; }

        [Designation("Включенные в стоимость продукты")]
        public List<ProductIncludedLookup> ProductsIncluded { get; private set; }

        /// <summary>
        /// Все платежи (совершенные + плановые).
        /// </summary>
        public IEnumerable<IPayment> Payments => PaymentsActual.Cast<IPayment>().Union(PaymentsPlannedByConditions);

        #region Суммы

        /// <summary>
        /// Оплаченная сумма
        /// </summary>
        [Designation("Оплачено")]
        public double SumPaid => PaymentsActual.Sum(x => x.Sum);

        /// <summary>
        /// Неоплаченная сумма
        /// </summary>
        [Designation("Неоплачено")]
        public double SumNotPaid => Cost - SumPaid;


        /// <summary>
        /// Сумама, необходимая для начала производства
        /// </summary>
        [Designation("Сумма старта производства")]
        public double SumToStartProduction => PaymentConditionSet.PaymentConditions.Where(x =>
                                              x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart &&
                                              x.DaysToPoint <= 0).Sum(condition => Cost * condition.Part);

        /// <summary>
        /// Сумма, необходимая для отгрузки
        /// </summary>
        [Designation("Сумма отгрузки")]
        public double SumToShipping => PaymentConditionSet.PaymentConditions.Where(x => (
                                        x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart) ||
                                       (x.PaymentConditionPoint == PaymentConditionPoint.ProductionEnd) ||
                                       (x.PaymentConditionPoint == PaymentConditionPoint.Shipment && x.DaysToPoint <= 0)).
                                       Sum(condition => Cost * condition.Part);


        #endregion

        #region Даты

        [Designation("ОИТ")]
        public DateTime OrderInTakeDate => StartProductionDate ?? StartProductionDateCalculated;
        [Designation("Год")]
        public int OrderInTakeYear => OrderInTakeDate.Year;
        [Designation("Месяц")]
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
            return null;
        }

        /// <summary>
        /// Дата исполнения условий для запуска производства
        /// </summary>
        public DateTime? StartProductionConditionsDoneDate => AchiveSumDate(SumToStartProduction);

        /// <summary>
        /// Дата исполнения условий для осуществления отгрузки
        /// </summary>
        public DateTime? ShippingConditionsDoneDate => AchiveSumDate(SumToShipping);

        /// <summary>
        /// Расчетная дата начала производства.
        /// </summary>
        [Designation("Начало произчодства (расч.)")]
        public DateTime StartProductionDateCalculated
        {
            get
            {
                if (StartProductionDate.HasValue) return StartProductionDate.Value;

                //по исполнению условий, необходимых для запуска производства
                if (StartProductionConditionsDoneDate.HasValue) return StartProductionConditionsDoneDate.Value;

                //по дате первого платежа
                if (PaymentsActual.Any()) return PaymentsActual.OrderBy(x => x.Date).First().Date;

                var productionTerm = this.ProductionTerm ?? CommonOptions.ProductionTerm;

                //по дате доставки оборудования на объект
                if (DeliveryDate.HasValue) return DeliveryDate.Value.AddDays(-productionTerm).AddDays(-DeliveryPeriodCalculated).GetTodayIfDateFromPastAndSkipWeekend();

                //по необходимой дате реализации проекта
                return DeliveryDateExpected.AddDays(-productionTerm).AddDays(-DeliveryPeriodCalculated).GetTodayIfDateFromPastAndSkipWeekend();
            }
        }

        /// <summary>
        /// Расчетная дата окончания производства.
        /// </summary>
        [Designation("Окончание произчодства (расч.)")]
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
                    return PickingDate.Value.AddDays(assembleTerm).GetTodayIfDateFromPastAndSkipWeekend();
                }

                //по сроку производства
                var productionTerm = this.ProductionTerm ?? CommonOptions.ProductionTerm;
                return StartProductionDateCalculated.AddDays(productionTerm).GetTodayIfDateFromPastAndSkipWeekend();
            }
        }

        /// <summary>
        /// Расчетная дата реализации.
        /// </summary>
        public DateTime RealizationDateCalculated => RealizationDate ?? DeliveryDateCalculated;

        /// <summary>
        /// Расчетная дата отгрузки.
        /// </summary>
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
                    return ShippingConditionsDoneDate.Value.GetTodayIfDateFromPastAndSkipWeekend();

                //по дате окончания производства
                return EndProductionDateCalculated.GetTodayIfDateFromPastAndSkipWeekend();
            }
        }

        /// <summary>
        /// Расчетная дата доставки.
        /// </summary>
        public DateTime DeliveryDateCalculated
        {
            get
            {
                if (DeliveryDate.HasValue) return DeliveryDate.Value;
                return ShipmentDateCalculated.AddDays(DeliveryPeriodCalculated).GetTodayIfDateFromPastAndSkipWeekend();
            }
        }

        /// <summary>
        /// Расчетный срок доставки.
        /// </summary>
        public double DeliveryPeriodCalculated
        {
            get
            {
                //по ожидаемому сроку доставки
                if (ExpectedDeliveryPeriod.HasValue) return ExpectedDeliveryPeriod.Value;

                //по стандартному сроку доставки до адреса разгрузки
                if (Address?.Locality.StandartDeliveryPeriod != null) return Address.Locality.StandartDeliveryPeriod.Value;

                //по стандартному сроку доставки до адреса объекта
                if (Facility.Address?.Locality.StandartDeliveryPeriod != null) return Facility.Address.Locality.StandartDeliveryPeriod.Value;

                ////по стандартному сроку доставки до столицы региона
                //if (Address.Locality.Region.Capital.StandartDeliveryPeriod.HasValue) return Address.Locality.Region.Capital.StandartDeliveryPeriod.Value;

                ////по стандартному сроку доставки до столицы федерального округа
                //if (Address.Locality.Region.District.Capital?.StandartDeliveryPeriod != null) return Address.Locality.Region.District.Capital.StandartDeliveryPeriod.Value;

                ////по стандартному сроку доставки до столицы страны
                //if (Address.Locality.Region.District.Country.Capital?.StandartDeliveryPeriod != null) return Address.Locality.Region.District.Country.Capital.StandartDeliveryPeriod.Value;

                return 7;
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
        /// Неисполненные платежные условия
        /// </summary>
        [Designation("Неисполненные платежные условия")]
        private IEnumerable<PaymentCondition> PaymentConditionsToDone => GeneratePaymentConditionsToDone();

        private IEnumerable<PaymentCondition> GeneratePaymentConditionsToDone()
        {
            var result = new List<PaymentCondition>();

            if (_paymentConditionParts == null)
            {
                _paymentConditionParts = new List<PaymentConditionPart>();
            }
            _paymentConditionParts.Clear();

            //если стоимость нулевая или нечего платить - выходим
            if (Cost < 0.00001 || SumNotPaid < 0.00001) return result;

            //берем все условия и упорядочиваем их
            var conditions = PaymentConditionSet.PaymentConditions.Select(x => x.Entity).OrderByDescending(x => x).ToList();

            //неоплаченная часть
            var rest = SumNotPaid / Cost;

            //добавление в результат неисполненных условий
            foreach (var condition in conditions)
            {
                rest -= condition.Part;
                //если осталось неоплаченное - условие проходит
                if (rest >= 0)
                {
                    result.Add(condition);
                    continue;
                }

                var newCondition = new PaymentCondition
                {
                    DaysToPoint = condition.DaysToPoint,
                    PaymentConditionPoint = condition.PaymentConditionPoint,
                    Part = condition.Part + rest
                };
                if (newCondition.Part > 0)
                {
                    result.Add(newCondition);
                    _paymentConditionParts.Add(new PaymentConditionPart(condition, newCondition));
                }
            }
            //возвращаем упорядоченные условия
            return result.OrderBy(x => x);
        }

        /// <summary>
        /// Актуальные плановые платежи
        /// </summary>
        public IEnumerable<PaymentPlanned> PaymentsPlannedActual
        {
            get
            {
                var remove = new List<PaymentPlanned>();
                foreach (var payment in Entity.PaymentsPlanned)
                {
                    if (PaymentConditionsToDone.Contains(payment.Condition))
                    {
                        yield return payment;
                        continue;
                    }
                    if (PaymentConditionParts.Select(x => x.Origin).Contains(payment.Condition))
                    {
                        var payment1 = payment;
                        var pcp = PaymentConditionParts.Single(x => Equals(x.Origin, payment1.Condition));
                        payment.Part = payment.Part * pcp.Part.Part;
                        yield return payment;
                        continue;
                    }
                    remove.Add(payment);
                }
                remove.ForEach(x => Entity.PaymentsPlanned.Remove(x));
            }
        }

        public IEnumerable<PaymentPlannedLookup> PaymentsPlannedActualLookups
            => PaymentsPlanned.Where(x => PaymentsPlannedActual.Contains(x.Entity));

        /// <summary>
        /// частично неисполненные условия
        /// </summary>
        private List<PaymentConditionPart> PaymentConditionParts
        {
            get
            {
                if (_paymentConditionParts == null)
                {
                    _paymentConditionParts = new List<PaymentConditionPart>();
                    GeneratePaymentConditionsToDone();
                }
                return _paymentConditionParts;
                
            }
        }

        /// <summary>
        /// Связка: платежное условие + насколько оно не исполнено
        /// </summary>
        class PaymentConditionPart
        {
            public PaymentCondition Origin { get; }
            public PaymentCondition Part { get; }

            public PaymentConditionPart(PaymentCondition origin, PaymentCondition part)
            {
                Origin = origin;
                Part = part;
            }
        }

        /// <summary>
        /// Плановые платежи по условиям (расчетные).
        /// </summary>
        [Designation("Расчетные остаточные платежи")]
        public IEnumerable<PaymentPlannedLookup> PaymentsPlannedByConditions
        {
            get
            {
                var paymentsPlanned = PaymentConditionsToDone.Select(x => new PaymentPlanned
                {
                    Date = GetPaymentDate(x),
                    Condition = x,
                    Part = x.Part
                });
                return paymentsPlanned.Select(x => new PaymentPlannedLookup(x) {Sum = Cost * x.Part});
            }
        }

        /// <summary>
        /// Плановые платежи (сохраненные + сгенерированные)
        /// </summary>
        public IEnumerable<PaymentPlannedLookup> PaymentPlannedWithSaved
        {
            get
            {
                //генерируем плановые платежи по условиям контракта
                //исключаем из них платежи с условиями, содержащимися в сохраненных
                var conditions = PaymentsPlannedActual.Select(x => x.Condition.Id);
                var generated = PaymentsPlannedByConditions.Where(x => !conditions.Contains(x.Condition.Id));

                //возвращаем упорядоченное объединение последовательностей
                return PaymentsPlannedActualLookups.Union(generated).OrderBy(x => x.Date);
            }
            
        }

        #endregion

    }
}