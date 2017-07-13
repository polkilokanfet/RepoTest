using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Wrappers
{
    public partial class SalesUnitWrapper
    {
        protected override void RunInConstructor()
        {
            this.PropertyChanged += MarginalIncomeOnPropertyChanged;
            this.PropertyChanged += OnMarginalIncomeInPercentSingleChanged;
            this.PropertyChanged += OnSpecificationChanged;
        }


        #region OnEvents
        private void OnCostChanged(object sender, PropertyChangedEventArgs e)
        {
            ReloadPaymentsPlannedLight();
        }

        private void PaymentsActualOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            ReloadPaymentsPlannedLight();
        }

        private void MarginalIncomeOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MarginalIncomeDate))
            {
                OnPropertyChanged(this, nameof(MarginalIncomeSingle));
                if (Math.Abs(Cost) > 0.00001)
                    MarginalIncomeInPercentSingle = MarginalIncomeSingle / Cost * 100;
            }
        }

        private void OnMarginalIncomeInPercentSingleChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MarginalIncomeInPercentSingle))
                Cost = ProductionUnit.Product.GetTotalPrice(MarginalIncomeDate) / (1 - MarginalIncomeInPercentSingle / 100);
        }

        private void OnSpecificationChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(POCOs.Specification))
                OnPropertyChanged(this, nameof(OrderInTakeDate));
        }
        #endregion

        #region PaymentsPlaned

        /// <summary>
        /// Не исполненные платежные условия
        /// </summary>
        public List<PaymentCondition> PaymentConditionsToDone
        {
            get
            {
                var conditions = PaymentsConditions.
                                    OrderBy(x => x.PaymentConditionPoint).
                                    ThenBy(x => x.DaysToPoint).Select(x => x.Model).ToList();

                var paidSum = SumPaid;

                while (paidSum > 0)
                {
                    var condition = conditions.First();
                    var conditionSum = condition.Part * Cost;
                    if (paidSum < conditionSum)
                    {
                        conditions.Add(new PaymentCondition
                        {
                            Part = (conditionSum - paidSum) / Cost,
                            PaymentConditionPoint = condition.PaymentConditionPoint,
                            DaysToPoint = condition.DaysToPoint
                        });
                    }

                    paidSum -= conditionSum;
                    conditions.Remove(condition);
                }

                return conditions.OrderBy(x => x.PaymentConditionPoint).ThenBy(x => x.DaysToPoint).ToList();
            }
        }

        /// <summary>
        /// Полная перезагрузка плановых платежей
        /// </summary>
        public void ReloadPaymentsPlannedFull()
        {
            PaymentsPlanned.Clear();
            foreach (var condition in PaymentConditionsToDone)
            {
                var payment = new PaymentPlanned { Sum = Cost * condition.Part };

                //дата платежа
                if (condition.PaymentConditionPoint == PaymentConditionPoint.ProductionStart) payment.Date = ProductionUnit.StartProductionDateCalculated.AddDays(condition.DaysToPoint);
                if (condition.PaymentConditionPoint == PaymentConditionPoint.ProductionEnd) payment.Date = ProductionUnit.EndProductionDateCalculated.AddDays(condition.DaysToPoint);
                if (condition.PaymentConditionPoint == PaymentConditionPoint.Shipment) payment.Date = ShipmentUnit.ShipmentDateCalculated.AddDays(condition.DaysToPoint);
                if (condition.PaymentConditionPoint == PaymentConditionPoint.Delivery) payment.Date = ShipmentUnit.DeliveryDateCalculated.AddDays(condition.DaysToPoint);

                var paymentWrapper = WrappersFactory.GetWrapper<PaymentPlannedWrapper>(payment);
                PaymentsPlanned.Add(paymentWrapper);
            }
        }

        /// <summary>
        /// Перезагрузка плановых платежей с сохранением информации о преждних платежах.
        /// </summary>
        public void ReloadPaymentsPlannedLight()
        {

        }

        #endregion

        #region Sums

        /// <summary>
        /// Оплаченная сумма
        /// </summary>
        public double SumPaid => PaymentsActual.Sum(x => x.Cost);

        /// <summary>
        /// Неоплаченная сумма
        /// </summary>
        public double SumNotPaid => Cost - SumPaid;

        /// <summary>
        /// Сумама, необходимая для начала производства
        /// </summary>
        public double SumToStartProduction => PaymentsConditions.Where(x => x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart && 
                                                x.DaysToPoint <= 0).Sum(condition => Cost * condition.Part);

        /// <summary>
        /// Сумма, необходимая для отгрузки
        /// </summary>
        public double SumToShipping => PaymentsConditions.Where(x =>
                    (x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart) ||
                    (x.PaymentConditionPoint == PaymentConditionPoint.ProductionEnd) ||
                    (x.PaymentConditionPoint == PaymentConditionPoint.Shipment && x.DaysToPoint <= 0))
                    .Sum(condition => Cost * condition.Part);

        #endregion

        #region Dates

        public DateTime OrderInTakeDate
        {
            get
            {
                //по дате запуска производства
                if (ProductionUnit.StartProductionDate.HasValue) return ProductionUnit.StartProductionDate.Value;
                return ProductionUnit.StartProductionDateCalculated;
            }
        }

        /// <summary>
        /// Дата исполнения условий для запуска производства
        /// </summary>
        public DateTime? StartProductionConditionsDoneDate
        {
            get
            {
                double sum = 0;
                foreach (var payment in Payments)
                {
                    sum += payment.Sum;
                    if (SumToStartProduction <= sum) return payment.Date;
                }
                return null;
            }
        }

        /// <summary>
        /// Дата исполнения условий для осуществления отгрузки
        /// </summary>
        public DateTime? ShippingConditionsDoneDate
        {
            get
            {
                double sum = 0;
                foreach (var payment in Payments)
                {
                    sum += payment.Sum;
                    if (SumToShipping <= sum) return payment.Date;
                }
                return null;
            }
        }

        /// <summary>
        /// Расчетная дата реализации.
        /// </summary>
        public DateTime RealizationDateCalculated
        {
            get
            {
                if (RealizationDate.HasValue) return RealizationDate.Value;
                return ShipmentUnit.DeliveryDateCalculated;
            }
        }

        #endregion

        #region MarginalIncomeSingle

        private double _marginalIncomeInPercentSingle;
        private DateTime? _marginalIncomeDate;

        public DateTime? MarginalIncomeDate
        {
            get { return _marginalIncomeDate; }
            set
            {
                if (Equals(_marginalIncomeDate, value))
                    return;
                _marginalIncomeDate = value;
                OnPropertyChanged(this, nameof(MarginalIncomeDate));
            }
        }

        /// <summary>
        /// Маржинальный доход единицы
        /// </summary>
        public double MarginalIncomeSingle => Cost - ProductionUnit.Product.GetTotalPrice(MarginalIncomeDate);

        public double MarginalIncomeInPercentSingle
        {
            get { return _marginalIncomeInPercentSingle; }
            set
            {
                if (value >= 100 || Math.Abs(_marginalIncomeInPercentSingle - value) < 0.00001)
                    return;

                _marginalIncomeInPercentSingle = value;
                OnPropertyChanged(this, nameof(MarginalIncomeInPercentSingle));
            }
        }

        #endregion

        public void ReGeneratePlanPaymentsHard()
        {
            PaymentsPlanned.ToList().ForEach(x => Payments.Remove(x));
            foreach (var condition in PaymentConditionsToDone)
            {
                var payment = new PaymentActual { Sum = new Cost { Sum = Cost.Sum * condition.Part } };

                //дата платежа
                if (condition.PaymentConditionPoint == PaymentConditionPoint.ProductionStart) payment.Date = StartProductionDateCalculated.AddDays(condition.DaysToPoint);
                if (condition.PaymentConditionPoint == PaymentConditionPoint.ProductionEnd) payment.Date = EndProductionDateCalculated.AddDays(condition.DaysToPoint);
                if (condition.PaymentConditionPoint == PaymentConditionPoint.Shipment) payment.Date = ShipmentUnit.ShipmentDateCalculated.AddDays(condition.DaysToPoint);
                if (condition.PaymentConditionPoint == PaymentConditionPoint.Delivery) payment.Date = ShipmentUnit.DeliveryDateCalculated.AddDays(condition.DaysToPoint);

                var paymentWrapper = WrappersFactory.GetWrapper<PaymentWrapper>(payment);
                Payments.Add(paymentWrapper);
            }
        }

        public void ReGeneratePlanPaymentsLight()
        {
            //если плановых платежей еще нет, либо был удален совершенный платеж.
            if (!PaymentsPlanned.Any() || SumNotPaid.Sum > PaymentsPlanned.Sum(x => x.Cost.Sum))
            {
                ReloadPaymentsPlannedFull();
                return;
            }

            var paymentsToRemove = new List<PaymentWrapper>(PaymentsPlanned);
            var sumRest = SumNotPaid.Sum;
            while (sumRest > 0 && paymentsToRemove.Any())
            {
                var payment = paymentsToRemove.Last();
                if (sumRest < payment.Cost.Sum)
                {
                    payment.Cost.Sum = sumRest;
                    paymentsToRemove.Remove(payment);
                    break;
                }
                sumRest -= payment.Cost.Sum;
                paymentsToRemove.Remove(payment);
            }

            foreach (var paymentWrapper in paymentsToRemove)
                Payments.Remove(paymentWrapper);
        }
    }
}