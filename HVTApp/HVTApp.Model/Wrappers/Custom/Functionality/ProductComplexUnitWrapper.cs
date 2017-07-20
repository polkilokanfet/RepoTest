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
            this.PropertyChanged += OnMarginalIncomeInPercentChanged;
            this.PropertyChanged += OnSpecificationChanged;

            this.PaymentsActual.CollectionChanged += PaymentsActualOnCollectionChanged;
        }


        #region OnEvents
        //реакция на изменение стоимости
        private void OnCostChanged(object sender, PropertyChangedEventArgs e)
        {
            ReloadPaymentsPlannedLight();
        }

        //реакция на изменение совершенных платежей
        private void PaymentsActualOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            ReloadPaymentsPlannedLight();
        }

        private void MarginalIncomeOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(MarginalIncomeDate)) return;

            OnPropertyChanged(this, nameof(MarginalIncome));
            if (Math.Abs(Cost) > 0.00001)
                MarginalIncomeInPercent = MarginalIncome / Cost * 100;
        }

        private void OnMarginalIncomeInPercentChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MarginalIncomeInPercent))
                Cost = ProductionUnit.Product.GetTotalPrice(MarginalIncomeDate) / (1 - MarginalIncomeInPercent / 100);
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

        DateTime GetPaymentDate(PaymentCondition condition)
        {
                switch (condition.PaymentConditionPoint)
                {
                    case PaymentConditionPoint.ProductionStart:
                        return ProductionUnit.StartProductionDateCalculated.AddDays(condition.DaysToPoint);
                    case PaymentConditionPoint.ProductionEnd:
                        return ProductionUnit.EndProductionDateCalculated.AddDays(condition.DaysToPoint);
                    case PaymentConditionPoint.Shipment:
                        return ShipmentUnit.ShipmentDateCalculated.AddDays(condition.DaysToPoint);
                    case PaymentConditionPoint.Delivery:
                        return ShipmentUnit.DeliveryDateCalculated.AddDays(condition.DaysToPoint);
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
            foreach (var condition in PaymentConditionsToDone)
            {
                var payment = new PaymentPlanned {Sum = Cost*condition.Part, Date = GetPaymentDate(condition)};
                var paymentWrapper = GetWrapper<PaymentPlannedWrapper, PaymentPlanned>(payment);
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
        public double SumPaid => PaymentsActual.Sum(x => x.Sum);

        /// <summary>
        /// Неоплаченная сумма
        /// </summary>
        public double SumNotPaid => Cost - SumPaid;

        /// <summary>
        /// Сумама, необходимая для начала производства
        /// </summary>
        public double SumToStartProduction => PaymentsConditions.Where(x => x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart && x.DaysToPoint <= 0).Sum(condition => Cost*condition.Part);

        /// <summary>
        /// Сумма, необходимая для отгрузки
        /// </summary>
        public double SumToShipping => PaymentsConditions.Where(x => (x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart) || (x.PaymentConditionPoint == PaymentConditionPoint.ProductionEnd) || (x.PaymentConditionPoint == PaymentConditionPoint.Shipment && x.DaysToPoint <= 0)).Sum(condition => Cost*condition.Part);

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

        //дата достижения суммы
        private DateTime? AchiveSumDate(double sumToAchive)
        {
            IEnumerable<IPayment> paymentsActual = PaymentsActual.Select(x => x.Model as IPayment);
            IEnumerable<IPayment> paymentsPlanned = PaymentsPlanned.Select(x => x.Model as IPayment);
            IEnumerable<IPayment> payments = paymentsActual.Concat(paymentsPlanned);

            double sum = 0;
            foreach (var payment in payments)
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

        #region MarginalIncome

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
        public double MarginalIncome => Cost - ProductionUnit.Product.GetTotalPrice(MarginalIncomeDate);

        private double _marginalIncomeInPercent;

        public double MarginalIncomeInPercent
        {
            get { return _marginalIncomeInPercent; }
            set
            {
                //МД не должен быть 100% и более
                if (value >= 100 || Math.Abs(_marginalIncomeInPercent - value) < 0.00001) return;

                _marginalIncomeInPercent = value;
                OnPropertyChanged(this, nameof(MarginalIncomeInPercent));
            }
        }

        #endregion
    }
}