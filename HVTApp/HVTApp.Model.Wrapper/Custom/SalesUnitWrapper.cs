using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace HVTApp.Model.Wrapper
{
    public partial class SalesUnitWrapper
    {
        protected override void RunInConstructor()
        {
            this.PaymentsActual.CollectionChanged += PaymentsActualOnCollectionChanged;

            this.PropertyChanged += MarginalIncomeOnPropertyChanged;
            this.PropertyChanged += OnMarginalIncomeInPercentSingleChanged;
            this.PropertyChanged += OnSpecificationChanged;

            this.CostSingle.PropertyChanged += OnCostChanged;
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
                if (CostSingle != null && Math.Abs(CostSingle.Sum) > 0.00001)
                    MarginalIncomeInPercentSingle = MarginalIncomeSingle/CostSingle.Sum*100;
            }
        }

        private void OnMarginalIncomeInPercentSingleChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MarginalIncomeInPercentSingle))
                CostSingle.Sum = ProductionUnit.Product.GetTotalPrice(MarginalIncomeDate)/(1 - MarginalIncomeInPercentSingle/100);
        }

        private void OnSpecificationChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Specification))
                OnPropertyChanged(this, nameof(OrderInTakeDate));
        }
        #endregion

        #region Payments
        /// <summary>
        /// Совершенные и плановые платежи (упорядочены по дате).
        /// </summary>
        public List<PaymentWrapper> PaymentsAll => PaymentsActual.Concat(PaymentsPlanned).OrderBy(x => x.Date).ToList();

        /// <summary>
        /// Не исполненные платежные условия
        /// </summary>
        public List<PaymentConditionWrapper> PaymentConditionsToDone
        {
            get
            {
                var conditions = PaymentsConditions.OrderBy(x => x.PaymentConditionPoint).ThenBy(x => x.DaysToPoint).ToList();
                var paidSum = SumPaid.Sum;

                while (paidSum > 0)
                {
                    var condition = conditions.First();
                    var conditionSum = condition.PartInPercent/100*CostSingle.Sum;
                    if (paidSum < conditionSum)
                    {
                        conditions.Add(PaymentConditionWrapper.GetWrapper(new PaymentCondition
                        {
                            PartInPercent = (conditionSum - paidSum) / CostSingle.Sum * 100,
                            PaymentConditionPoint = condition.PaymentConditionPoint,
                            DaysToPoint = condition.DaysToPoint
                        }));
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
                var payment = new Payment { SumAndVat = new SumAndVat {Sum = CostSingle.Sum*condition.PartInPercent/100, Vat = CostSingle.Vat} };

                //дата платежа
                if (condition.PaymentConditionPoint == PaymentConditionPoint.ProductionStart) payment.Date = ProductionUnit.ProductionStartDateCalculated.AddDays(condition.DaysToPoint);
                if (condition.PaymentConditionPoint == PaymentConditionPoint.ProductionEnd) payment.Date = ProductionUnit.ProductionEndDateCalculated.AddDays(condition.DaysToPoint);
                if (condition.PaymentConditionPoint == PaymentConditionPoint.Shipment) payment.Date = ShipmentUnit.ShipmentDateCalculated.AddDays(condition.DaysToPoint);
                if (condition.PaymentConditionPoint == PaymentConditionPoint.Delivery) payment.Date = ShipmentUnit.DeliveryDateCalculated.AddDays(condition.DaysToPoint);

                var paymentWrapper = PaymentWrapper.GetWrapper(payment);
                PaymentsPlanned.Add(paymentWrapper);
            }
        }

        /// <summary>
        /// Перезагрузка плановых платежей с сохранением информации о преждних платежах.
        /// </summary>
        public void ReloadPaymentsPlannedLight()
        {
            //если плановых платежей еще нет, либо был удален совершенный платеж.
            if (!PaymentsPlanned.Any() || SumRest.Sum > PaymentsPlanned.Sum(x => x.SumAndVat.Sum))
            {
                ReloadPaymentsPlannedFull();
                return;
            }

            var paymentsToRemove = new List<PaymentWrapper>(PaymentsPlanned);
            var sumRest = SumRest.Sum;
            while (sumRest > 0 && paymentsToRemove.Any())
            {
                var payment = paymentsToRemove.Last();
                if (sumRest < payment.SumAndVat.Sum)
                {
                    payment.SumAndVat.Sum = sumRest;
                    paymentsToRemove.Remove(payment);
                    break;
                }
                sumRest -= payment.SumAndVat.Sum;
                paymentsToRemove.Remove(payment);
            }

            foreach (var paymentWrapper in paymentsToRemove)
                PaymentsPlanned.Remove(paymentWrapper);

        }

        #endregion

        #region Sums

        /// <summary>
        /// Стоимость единицы вместе со всеми ее дочерними единицами.
        /// </summary>
        public SumAndVatWrapper CostTotal => SumAndVatWrapper.GetWrapper(new SumAndVat { Sum = CostSingle.Sum + ChildSalesUnits.Sum(x => x.CostTotal.Sum), Vat = CostSingle.Vat });

        /// <summary>
        /// Оплаченная сумма
        /// </summary>
        public SumAndVatWrapper SumPaid => SumAndVatWrapper.GetWrapper(new SumAndVat { Sum = PaymentsActual.Sum(x => x.SumAndVat.Sum), Vat = CostSingle.Vat });

        /// <summary>
        /// Неоплаченная сумма
        /// </summary>
        public SumAndVatWrapper SumRest => SumAndVatWrapper.GetWrapper(new SumAndVat { Sum = CostSingle.Sum - SumPaid.Sum, Vat = CostSingle.Vat });

        /// <summary>
        /// Сумама, необходимая для начала производства
        /// </summary>
        public SumAndVatWrapper SumToStartProduction
        {
            get
            {
                //условия, связанные с датой запуска производства
                var conditions = PaymentsConditions.Where(x => x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart && x.DaysToPoint <= 0);
                var sum = conditions.Sum(condition => CostSingle.Sum*condition.PartInPercent/100);
                return SumAndVatWrapper.GetWrapper(new SumAndVat {Sum = sum, Vat = CostSingle.Vat});
            }
        }

        /// <summary>
        /// Сумма, необходимая для отгрузки
        /// </summary>
        public SumAndVatWrapper SumToShipping
        {
            get
            {
                //условия, связанные с датой отгрузки
                var conditions = PaymentsConditions.Where(x =>
                    (x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart) ||
                    (x.PaymentConditionPoint == PaymentConditionPoint.ProductionEnd) ||
                    (x.PaymentConditionPoint == PaymentConditionPoint.Shipment && x.DaysToPoint <= 0));
                var sum = conditions.Sum(condition => CostSingle.Sum*condition.PartInPercent/100);
                return SumAndVatWrapper.GetWrapper(new SumAndVat {Sum = sum, Vat = CostSingle.Vat});
            }
        }

        #endregion

        #region Dates

        public DateTime OrderInTakeDate
        {
            get
            {
                //по дате запуска производства
                if (ProductionUnit.StartProductionDate.HasValue)
                    return ProductionUnit.StartProductionDate.Value;
                return ProductionUnit.ProductionStartDateCalculated;
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
                foreach (var payment in PaymentsAll)
                {
                    sum += payment.SumAndVat.Sum;
                    if (SumToStartProduction.Sum <= sum) return payment.Date;
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
                foreach (var payment in PaymentsAll)
                {
                    sum += payment.SumAndVat.Sum;
                    if (SumToShipping.Sum <= sum) return payment.Date;
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
        public double MarginalIncomeSingle => CostSingle.Sum - ProductionUnit.Product.GetTotalPrice(MarginalIncomeDate);
        /// <summary>
        /// Маржинальный доход единицы и всех ее дочерних единиц
        /// </summary>
        public double MarginalIncomeTotal => MarginalIncomeSingle + ChildSalesUnits.Sum(x => x.MarginalIncomeTotal);

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

        /// <summary>
        /// Маржинальный доход единицы и всех ее дочерних единиц в процентах
        /// </summary>        
        public double MarginalIncomeInPercentTotal => MarginalIncomeTotal / CostTotal.Sum * 100;


        #endregion

    }
}