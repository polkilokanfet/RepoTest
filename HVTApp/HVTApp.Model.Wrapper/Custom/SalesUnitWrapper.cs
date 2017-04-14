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
            this.PropertyChanged += OnMarginalIncomeInPercentChanged;
            this.PropertyChanged += OnSpecificationChanged;

            this.Cost.PropertyChanged += OnCostChanged;
        }

        private void OnCostChanged(object sender, PropertyChangedEventArgs e)
        {
            ReloadPaymentsPlannedLight();
        }

        private void MarginalIncomeOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MarginalIncomeDate))
            {
                OnPropertyChanged(this, nameof(MarginalIncome));
                if (Cost != null && Math.Abs(Cost.Sum) > 0.00001)
                    MarginalIncomeInPercent = MarginalIncome/Cost.Sum*100;
            }
        }

        private void PaymentsActualOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            ReloadPaymentsPlannedLight();
        }

        /// <summary>
        /// Оплаченная часть
        /// </summary>
        public SumAndVatWrapper SumPaid => SumAndVatWrapper.GetWrapper(new SumAndVat()
        {
            Sum = PaymentsActual.Sum(x => x.SumAndVat.Sum),
            Vat = Cost.Vat
        });

        /// <summary>
        /// Неоплаченная часть
        /// </summary>
        public SumAndVatWrapper SumRest => SumAndVatWrapper.GetWrapper(new SumAndVat()
        {
            Sum = Cost.Sum - SumPaid.Sum,
            Vat = Cost.Vat
        });

        /// <summary>
        /// Еще не исполненные платежные условия
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
                    var conditionSum = condition.PartInPercent/100*Cost.Sum;
                    if (paidSum < conditionSum)
                    {
                        conditions.Add(PaymentConditionWrapper.GetWrapper(new PaymentCondition
                        {
                            PartInPercent = (conditionSum - paidSum) / Cost.Sum * 100,
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

        Dictionary<PaymentWrapper, PaymentConditionWrapper> _paymentConditionDictionary = new Dictionary<PaymentWrapper, PaymentConditionWrapper>();  

        /// <summary>
        /// Полная перезагрузка плановых платежей
        /// </summary>
        public void ReloadPaymentsPlannedFull()
        {
            PaymentsPlanned.Clear();
            _paymentConditionDictionary.Clear();
            foreach (var condition in PaymentConditionsToDone)
            {
                var payment = new Payment { SumAndVat = new SumAndVat {Sum = Cost.Sum*condition.PartInPercent/100, Vat = Cost.Vat} };

                //дата платежа
                if (condition.PaymentConditionPoint == PaymentConditionPoint.ProductionStart) payment.Date = ProductionUnit.StartProductionDateCalculated.AddDays(condition.DaysToPoint);
                if (condition.PaymentConditionPoint == PaymentConditionPoint.ProductionEnd) payment.Date = ProductionUnit.EndProductionDateCalculated.AddDays(condition.DaysToPoint);
                if (condition.PaymentConditionPoint == PaymentConditionPoint.Shipment) payment.Date = ShipmentUnit.ShipmentDateCalculated.AddDays(condition.DaysToPoint);
                if (condition.PaymentConditionPoint == PaymentConditionPoint.Delivery) payment.Date = ShipmentUnit.DeliveryDateCalculated.AddDays(condition.DaysToPoint);

                var paymentWrapper = PaymentWrapper.GetWrapper(payment);
                _paymentConditionDictionary.Add(paymentWrapper, condition);
                PaymentsPlanned.Add(paymentWrapper);
            }
        }

        /// <summary>
        /// Перезагрузка плановых платежей с сохранением информации о преждних платежах.
        /// </summary>
        public void ReloadPaymentsPlannedLight()
        {
            if (!PaymentsPlanned.Any())
            {
                ReloadPaymentsPlannedFull();
                return;
            }

            var paymentsToRemove = new List<PaymentWrapper>(PaymentsPlanned);
            var sumRest = SumRest.Sum;
            while (sumRest > 0)
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

        #region Dates

        public DateTime OrderInTakeDate
        {
            get
            {
                //по дате запуска производства
                if (ProductionUnit.StartProductionDate.HasValue)
                    return ProductionUnit.StartProductionDate.Value;
                return ProductionUnit.StartProductionDateCalculated;
            }
        }

        public double SumToStartProduction
        {
            get
            {
                //условия, связанные с датой запуска производства
                var conditions =
                    PaymentsConditions.Where(
                        x => x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart && x.DaysToPoint <= 0);
                return conditions.Sum(condition => Cost.Sum*condition.PartInPercent/100);
            }
        }

        public double SumToShipping
        {
            get
            {
                //условия, связанные с датой отгрузки
                var conditions = PaymentsConditions.Where(x =>
                    (x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart) ||
                    (x.PaymentConditionPoint == PaymentConditionPoint.ProductionEnd) ||
                    (x.PaymentConditionPoint == PaymentConditionPoint.Shipment && x.DaysToPoint <= 0));
                return conditions.Sum(condition => Cost.Sum*condition.PartInPercent/100);
            }
        }

        public List<PaymentWrapper> PaymentsAll => PaymentsActual.Concat(PaymentsPlanned).OrderBy(x => x.Date).ToList();

        public DateTime? StartProductionConditionsDoneDate
        {
            get
            {
                double sum = 0;
                foreach (var payment in PaymentsAll)
                {
                    sum += payment.SumAndVat.Sum;
                    if (SumToStartProduction <= sum) return payment.Date;
                }
                return null;
            }
        }

        public DateTime? ShippingConditionsDoneDate
        {
            get
            {
                double sum = 0;
                foreach (var payment in PaymentsAll)
                {
                    sum += payment.SumAndVat.Sum;
                    if (SumToShipping <= sum) return payment.Date;
                }
                return null;
            }
        }

        private void OnSpecificationChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Specification))
                OnPropertyChanged(this, nameof(OrderInTakeDate));
        }

        #endregion

        #region MarginalIncome

        private double _marginalIncomeInPercent;
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

        public double MarginalIncome => Cost.Sum - ProductionUnit.Product.GetTotalPrice(MarginalIncomeDate);

        public double MarginalIncomeInPercent
        {
            get { return _marginalIncomeInPercent; }
            set
            {
                if (value >= 100 || Math.Abs(_marginalIncomeInPercent - value) < 0.00001)
                    return;

                _marginalIncomeInPercent = value;
                OnPropertyChanged(this, nameof(MarginalIncomeInPercent));
            }
        }

        private void OnMarginalIncomeInPercentChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MarginalIncomeInPercent))
                Cost.Sum = ProductionUnit.Product.GetTotalPrice(MarginalIncomeDate)/(1 - MarginalIncomeInPercent/100);
        }

        #endregion

    }
}
