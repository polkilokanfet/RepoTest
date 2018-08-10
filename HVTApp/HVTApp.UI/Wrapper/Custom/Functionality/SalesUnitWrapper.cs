using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Wrapper
{

    public partial class SalesUnitWrapper : ProductCostDependentProductsWrapper<SalesUnit>, IUnitGroup
    {
        public int Amount => 1;

        protected override void RunInConstructor()
        {
            PaymentsPlannedSaved.ForEach(x => x.SalesUnit = this);

            this.PropertyChanged += OnSpecificationChanged;

            this.PaymentsActual.CollectionChanged += PaymentsActualOnCollectionChanged;
            this.PaymentsActual.PropertyChanged += PaymentActualOnChanged;

            PriceDate = DateTime.Today;
        }

        // Реакция на изменение какого-либо совершенного платежа.
        private void PaymentActualOnChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(PaymentPlannedWrappers));
        }

        // Реакция на изменение коллекции совершенных платежей.
        private void PaymentsActualOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            OnPropertyChanged(nameof(PaymentPlannedWrappers));
        }

        private void OnSpecificationChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Specification))
            {
                OnPropertyChanged(nameof(OrderInTakeDate));
            }
        }
    }

    //Sums
    public partial class SalesUnitWrapper
    {
        /// <summary>
        /// Оплаченная сумма
        /// </summary>
        public double SumPaid => PaymentsActual.Sum(x => x.Sum);

        /// <summary>
        /// Неоплаченная сумма
        /// </summary>
        public double SumNotPaid => Cost - SumPaid;

        /// <summary>
        /// Сумама, необходимая для начала производства
        /// </summary>
        public double SumToStartProduction => PaymentConditionSet.PaymentConditions.Where(x =>
                                              x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart &&
                                              x.DaysToPoint <= 0).Sum(condition => Cost * condition.Part);

        /// <summary>
        /// Сумма, необходимая для отгрузки
        /// </summary>
        public double SumToShipping => PaymentConditionSet.PaymentConditions.Where(x => (
                                        x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart) ||
                                       (x.PaymentConditionPoint == PaymentConditionPoint.ProductionEnd) ||
                                       (x.PaymentConditionPoint == PaymentConditionPoint.Shipment && x.DaysToPoint <= 0)).
                                       Sum(condition => Cost * condition.Part);
    }

    //MarginalIncome
    public partial class SalesUnitWrapper 
    {
    }

    //Dates
    public partial class SalesUnitWrapper 
    {
        public DateTime OrderInTakeDate => StartProductionDate ?? StartProductionDateCalculated;
        public int OrderInTakeYear => OrderInTakeDate.Year;
        public int OrderInTakeMonth => OrderInTakeDate.Month;

        //дата достижения суммы
        private DateTime? AchiveSumDate(double sumToAchive)
        {
            IEnumerable<IPayment> paymentsActual = PaymentsActual.Select(x => new PaymentActualWrapper(x.Model));
            IEnumerable<IPayment> paymentsPlanned = PaymentsPlannedSaved.SelectMany(x => x.Payments);
            IEnumerable<IPayment> payments = paymentsActual.Concat(paymentsPlanned).OrderBy(x => x.Date);

            double sum = 0;
            foreach (var payment in payments)
            {
                sum += payment.Sum;
                if (sumToAchive <= sum)
                    return payment.Date;
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
        public DateTime EndProductionDateCalculated
        {
            get
            {
                //по дате производства
                if (EndProductionDate.HasValue) return EndProductionDate.Value;

                //по дате комплектации
                if (PickingDate.HasValue)
                {
                    int days = this.AssembleTerm ?? CommonOptions.AssembleTerm; 
                    return PickingDate.Value.AddDays(days).GetTodayIfDateFromPastAndSkipWeekend();
                }

                //по сроку производства
                int productionTerm = AssembleTerm ?? CommonOptions.ProductionTerm;
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
    }

    //Payments
    public partial class SalesUnitWrapper
    {
        /// <summary>
        /// Оставшиеся плановые платежи по условиям оплаты.
        /// </summary>
        public IEnumerable<PaymentPlannedListWrapper> PaymentPlannedListWrappersByConditionsToDone 
            => GetPlannedPayments(this.PaymentConditionsToDone).Select(x => new PaymentPlannedListWrapper(x) {SalesUnit = this});

        public IEnumerable<PaymentPlannedWrapper> PaymentPlannedWrappers
        {
            get
            {
                var paymentListsToDone = PaymentPlannedListWrappersByConditionsToDone.ToList();
                var result = PaymentsPlannedSaved.Concat(paymentListsToDone).ToList();

                foreach (var paymentListSaved in this.PaymentsPlannedSaved)
                {
                    var paymentListToDone = paymentListsToDone.SingleOrDefault(x => x.Condition.Model.Equals(paymentListSaved.Condition.Model));
                    if (paymentListToDone != null) //значит условие еще не исполнено
                    {
                        CheckPaymentPlannedList(paymentListSaved);
                        result.Remove(paymentListToDone);
                    }
                    else
                    {
                        PaymentsPlannedSaved.Remove(paymentListSaved);
                        result.Remove(paymentListSaved);
                    }
                }

                return result.SelectMany(x => x.Payments);
            }
        }

        private void CheckPaymentPlannedList(PaymentPlannedListWrapper paymentPlannedListWrapper)
        {
            var sum = Cost * paymentPlannedListWrapper.Condition.Part;
            var sumToCheck = paymentPlannedListWrapper.Payments.Sum(x => x.Sum);

            foreach (var paymentPlannedWrapper in paymentPlannedListWrapper.Payments)
            {
                if (Math.Abs(sum - sumToCheck) < 0.0001) return;
                    paymentPlannedWrapper.Sum = paymentPlannedWrapper.Sum * sum / sumToCheck;
                paymentPlannedWrapper.Date = paymentPlannedWrapper.Date.GetTodayIfDateFromPastAndSkipWeekend();
            }
        }


        /// <summary>
        /// Не исполненные платежные условия
        /// </summary>
        public IEnumerable<PaymentCondition> PaymentConditionsToDone
        {
            get
            {
                var conditions = PaymentConditionSet.PaymentConditions.Select(x => x.Model).OrderByDescending(x => x).ToList();
                var result = new List<PaymentCondition>();

                if (Math.Abs(Cost) < 0.0001) return result;

                double rest = SumNotPaid / Cost;
                foreach (var condition in conditions)
                {
                    rest -= condition.Part;
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
                    if (newCondition.Part > 0) result.Add(newCondition);
                }
                return result.OrderBy(x => x);
            }
        }

        //вернуть дату исходя из условия
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

        private IEnumerable<PaymentPlannedList> GetPlannedPayments(IEnumerable<PaymentCondition> conditions)
        {
            return conditions.Select(condition => new PaymentPlannedList
            {
                SalesUnitId = Model.Id,
                Payments = new List<PaymentPlanned> { new PaymentPlanned { Sum = Cost * condition.Part, Date = GetPaymentDate(condition)} },
                Condition = condition
            });
        }

        public double MarginalIncome { get; set; }
        public bool HasBlocksWithoutPrice { get; }
    }
}