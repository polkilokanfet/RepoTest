using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class SalesUnitWrapper
    {
        protected override void RunInConstructor()
        {
            this.PropertyChanged += OnMarginalIncomeOnPropertyChanged;
            this.PropertyChanged += OnMarginalIncomeInPercentChanged;
            this.PropertyChanged += OnSpecificationChanged;

            this.PaymentsActual.CollectionChanged += PaymentsActualOnCollectionChanged;
            this.PaymentsActual.PropertyChanged += PaymentActualOnChanged;
        }


        #region OnEvents
        //реакция на изменение стоимости
        private void OnCostChanged(object sender, PropertyChangedEventArgs e)
        {
            ReloadPaymentsPlannedLight();
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

        private void OnMarginalIncomeOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(MarginalIncomeDate)) return;

            OnPropertyChanged(nameof(MarginalIncome));
            if (Math.Abs(Cost) > 0.00001)
                MarginalIncomeInPercent = MarginalIncome / Cost * 100;
        }

        private void OnMarginalIncomeInPercentChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MarginalIncomeInPercent))
                Cost = ProductionUnit.Product.GetPrice(MarginalIncomeDate) / (1 - MarginalIncomeInPercent / 100);
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
        public double SumToStartProduction => PaymentsConditions.Where(x => x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart && x.DaysToPoint <= 0).Sum(condition => Cost*condition.Part);

        /// <summary>
        /// Сумма, необходимая для отгрузки
        /// </summary>
        public double SumToShipping => PaymentsConditions.Where(x => (
         x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart) || 
        (x.PaymentConditionPoint == PaymentConditionPoint.ProductionEnd) || 
        (x.PaymentConditionPoint == PaymentConditionPoint.Shipment && x.DaysToPoint <= 0)).Sum(condition => Cost*condition.Part);

        #endregion

        #region Dates

        public DateTime OrderInTakeDate => ProductionUnit.StartProductionDate ?? ProductionUnit.StartProductionDateCalculated;

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
        public DateTime RealizationDateCalculated => RealizationDate ?? ShipmentUnit.DeliveryDateCalculated;

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
                OnPropertyChanged(nameof(MarginalIncomeDate));
            }
        }

        /// <summary>
        /// Маржинальный доход единицы
        /// </summary>
        public double MarginalIncome => Cost - ProductionUnit.Product.GetPrice(MarginalIncomeDate);

        private double _marginalIncomeInPercent;

        public double MarginalIncomeInPercent
        {
            get { return _marginalIncomeInPercent; }
            set
            {
                //МД не должен быть 100% и более
                if (value >= 100 || Math.Abs(_marginalIncomeInPercent - value) < 0.00001) return;

                _marginalIncomeInPercent = value;
                OnPropertyChanged(nameof(MarginalIncomeInPercent));
            }
        }

        #endregion
    }
}