using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.ViewModels;

namespace HVTApp.UI.Wrapper
{
    public partial class SalesUnitWrapper : IProjectUnit
    {
        public int Amount => 1;

        protected override void RunInConstructor()
        {
            this.PropertyChanged += OnSpecificationChanged;

            this.PaymentsActual.CollectionChanged += PaymentsActualOnCollectionChanged;
            this.PaymentsActual.PropertyChanged += PaymentActualOnChanged;

            PriceDate = DateTime.Today;
            this.PropertyChanged += OnCostChanged;
            this.PropertyChanged += OnProductChanged;
        }

        #region OnEvents

        private void OnProductChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Product))
            {
                OnPropertyChanged(nameof(MarginalIncome));
                OnPropertyChanged(nameof(PriceErrors));
            }
        }

        //реакция на изменение стоимости
        private void OnCostChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Cost))
            {
                OnPropertyChanged(nameof(MarginalIncome));
                ReloadPaymentsPlannedLight();
            }

        }

        /// <summary>
        /// Реакция на изменение какого-либо совершенного платежа.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PaymentActualOnChanged(object sender, PropertyChangedEventArgs e)
        {
            ReloadPaymentsPlannedLight();
        }

        /// <summary>
        /// Реакция на изменение коллекции совершенных платежей.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void PaymentsActualOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            ReloadPaymentsPlannedLight();
        }

        private void OnSpecificationChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Specification))
                OnPropertyChanged(nameof(OrderInTakeDate));
        }
        #endregion

        #region PaymentsPlaned

        /// <summary>
        /// Не исполненные платежные условия
        /// </summary>
        public IEnumerable<PaymentCondition> PaymentConditionsToDone
        {
            get
            {
                var conditions = PaymentsConditions.Select(x => x.Model).OrderByDescending(x => x).ToList();
                var result = new List<PaymentCondition>();

                if (Math.Abs(Cost) < 0.0001) return result;

                double rest = SumNotPaid/Cost;
                foreach (var condition in conditions)
                {
                    rest -= condition.Part;
                    if (rest >= 0)
                    {
                        result.Add(condition);
                        continue;
                    }

                    var newCondition = new PaymentCondition { DaysToPoint = condition.DaysToPoint,
                        PaymentConditionPoint = condition.PaymentConditionPoint, Part = condition.Part + rest};
                    if (newCondition.Part > 0) result.Add(newCondition);
                }
                return result.OrderBy(x => x);
            }
        }

        DateTime GetPaymentDate(PaymentCondition condition)
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
        /// Полная перезагрузка плановых платежей
        /// </summary>
        public void ReloadPaymentsPlannedFull()
        {
            PaymentsPlanned.Clear();
            foreach (var payment in GetPlannedPayments(PaymentConditionsToDone))
            {
                PaymentsPlanned.Add(new PaymentPlannedWrapper(payment));
            }
        }

        private IEnumerable<PaymentPlanned> GetPlannedPayments(IEnumerable<PaymentCondition> conditions)
        {
            return conditions.Select(condition => new PaymentPlanned {Sum = Cost*condition.Part, Date = GetPaymentDate(condition), Condition = condition});
        }

        /// <summary>
        /// Перезагрузка плановых платежей с сохранением информации о преждних платежах.
        /// </summary>
        public void ReloadPaymentsPlannedLight()
        {
            //если еще нет плановых платежей
            if (!PaymentsPlanned.Any())
            {
                ReloadPaymentsPlannedFull();
                return;
            }

            //сортируем существующие плановые платежи в соответствии с датой платежа.
            var paymentsPlanned = PaymentsPlanned.OrderByDescending(x => x.Date);
            double rest = SumNotPaid;
            foreach (var paymentPlanned in paymentsPlanned)
            {
                rest -= paymentPlanned.Sum;
                if (rest >= 0) continue;

                paymentPlanned.Sum += rest;
                if (paymentPlanned.Sum <= 0) PaymentsPlanned.Remove(paymentPlanned);
            }

            //если закончились плановые платежи, а остаток оплаты еще не исчерпан
            if (rest > 0)
            {
                var payments = GetPlannedPayments(PaymentConditionsToDone.OrderBy(x => x));
                foreach (var paymentPlanned in payments)
                {
                    rest -= paymentPlanned.Sum;
                    if (rest >= 0)
                    {
                        PaymentsPlanned.Add( new PaymentPlannedWrapper(paymentPlanned));
                        continue;
                    }

                    paymentPlanned.Sum += rest;
                    PaymentsPlanned.Add(new PaymentPlannedWrapper(paymentPlanned));
                    break;
                }
            }
        }

        #endregion

        #region Sums

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
        public double SumToStartProduction => PaymentsConditions.Where(x => 
        x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart && 
        x.DaysToPoint <= 0).Sum(condition => Cost * condition.Part);

        /// <summary>
        /// Сумма, необходимая для отгрузки
        /// </summary>
        public double SumToShipping => PaymentsConditions.Where(x => (
         x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart) || 
        (x.PaymentConditionPoint == PaymentConditionPoint.ProductionEnd) || 
        (x.PaymentConditionPoint == PaymentConditionPoint.Shipment && x.DaysToPoint <= 0)).Sum(condition => Cost*condition.Part);

        #endregion

        #region Dates

        public DateTime OrderInTakeDate => StartProductionDate ?? StartProductionDateCalculated;

        //дата достижения суммы
        private DateTime? AchiveSumDate(double sumToAchive)
        {
            IEnumerable<IPayment> paymentsActual = PaymentsActual.Select(x => x.Model as IPayment);
            IEnumerable<IPayment> paymentsPlanned = PaymentsPlanned.Select(x => x.Model as IPayment);
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
        /// Расчетная дата реализации.
        /// </summary>
        public DateTime RealizationDateCalculated => RealizationDate ?? DeliveryDateCalculated;

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
                //по дате спецификации
                if (Specification != null) return Specification.Date;
                //по дате реализации проекта
                if (DeliveryDate != null)
                    return DeliveryDate.Value.AddDays(-CommonOptions.StandartTermFromStartToEndProduction).GetTodayIfDateFromPastAndSkipWeekend();
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Расчетная дата окончания производства.
        /// </summary>
        public DateTime EndProductionDateCalculated
        {
            get
            {
                //    //по дате производства
                //    if (EndProductionDate.HasValue) return EndProductionDate.Value;
                //    //по дате комплектации
                //    if (PickingDate.HasValue) return PickingDate.Value.AddDays(StandartTermFromPickToEndProduction);
                //    //по сроку производства
                //    return StartProductionDateCalculated.AddDays(StandartTermFromStartToEndProduction).GetTodayIfDateFromPastAndSkipWeekend();
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Расчетная дата отгрузки.
        /// </summary>
        public DateTime ShipmentDateCalculated
        {
            get
            {
                ////по реальной дате отгрузки
                //if (ShipmentDate.HasValue) return ShipmentDate.Value;

                ////по плановой дате отгрузки
                //if (ShipmentPlanDate.HasValue)
                //{
                //    if (SalesUnit.ShippingConditionsDoneDate.HasValue )
                //    {
                //        if (ShipmentPlanDate.Value >= SalesUnit.ShippingConditionsDoneDate &&
                //            ShipmentPlanDate.Value >= SalesUnit.ProductionUnit.EndProductionDateCalculated)
                //            return ShipmentPlanDate.Value;
                //    }
                //    else
                //    {
                //        if (ShipmentPlanDate.Value >= SalesUnit.ProductionUnit.EndProductionDateCalculated)
                //            return ShipmentPlanDate.Value;
                //    }

                //}

                ////по дате исполнения условий для отгрузки
                //if (SalesUnit.ShippingConditionsDoneDate.HasValue && 
                //    SalesUnit.ShippingConditionsDoneDate >= SalesUnit.ProductionUnit.EndProductionDateCalculated)
                //    return SalesUnit.ShippingConditionsDoneDate.Value.GetTodayIfDateFromPastAndSkipWeekend();

                ////по дате окончания производства
                //return SalesUnit.ProductionUnit.EndProductionDateCalculated.GetTodayIfDateFromPastAndSkipWeekend();
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Расчетная дата доставки.
        /// </summary>
        public DateTime DeliveryDateCalculated
        {
            get
            {
                //if (DeliveryDate.HasValue) return DeliveryDate.Value;
                //return ShipmentDateCalculated.AddDays(DeliveryPeriodCalculated).GetTodayIfDateFromPastAndSkipWeekend();
                throw new NotImplementedException();
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
                //if (ExpectedDeliveryPeriod.HasValue) return ExpectedDeliveryPeriod.Value;

                ////по стандартному сроку доставки до адреса
                //if (Address.Locality.StandartDeliveryPeriod.HasValue) return Address.Locality.StandartDeliveryPeriod.Value;

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

        #region MarginalIncome

        private DateTime _priceDate;
        public DateTime PriceDate
        {
            get { return _priceDate; }
            set
            {
                if (Equals(_priceDate, value)) return;
                _priceDate = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Price));
                OnPropertyChanged(nameof(MarginalIncome));
                OnPropertyChanged(nameof(PriceErrors));
            }
        }

        public double Price => Product.GetPrice(PriceDate);

        public string PriceErrors
        {
            get
            {
                var blocks = Product.GetBlocksWithoutActualPriceOnDate(PriceDate);
                string result = string.Empty;
                foreach (var block in blocks)
                {
                    result += $"{block.DisplayMember}; ";
                }
                return result;
            }
        }

        public double MarginalIncome
        {
            get { return (Math.Abs(Cost) > 0.0001) ? 100 * (Cost - Price) / Cost: 0; }
            set
            {
                if (Equals(MarginalIncome, value)) return;
                if (Math.Abs(value - 100) < 0.001) return;
                Cost = Price / (100 - value) * 100;
                OnPropertyChanged();
            }
        }

        #endregion


    }
}