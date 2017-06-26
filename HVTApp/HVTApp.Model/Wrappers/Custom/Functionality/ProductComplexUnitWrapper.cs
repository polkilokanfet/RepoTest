using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Wrappers
{
    public partial class ProductComplexUnitWrapper
    {
        protected override void RunInConstructor()
        {
            this.PropertyChanged += MarginalIncomeOnPropertyChanged;
            this.PropertyChanged += OnMarginalIncomeInPercentSingleChanged;
            this.PropertyChanged += OnSpecificationChanged;

            this.Cost.PropertyChanged += OnCostChanged;
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
                if (Cost!= null && Math.Abs(Cost.Sum) > 0.00001)
                    MarginalIncomeInPercentSingle = MarginalIncomeSingle/Cost.Sum*100;
            }
        }

        private void OnMarginalIncomeInPercentSingleChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MarginalIncomeInPercentSingle))
                Cost.Sum = Product.GetTotalPrice(MarginalIncomeDate)/(1 - MarginalIncomeInPercentSingle/100);
        }

        private void OnSpecificationChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(POCOs.Specification))
                OnPropertyChanged(this, nameof(OrderInTakeDate));
        }
        #endregion

        #region Payments

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
                    var conditionSum = condition.Part*Cost.Sum;
                    if (paidSum < conditionSum)
                    {
                        conditions.Add(WrappersFactory.GetWrapper<PaymentConditionWrapper>(new PaymentCondition
                        {
                            Part = (conditionSum - paidSum) / Cost.Sum,
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
            //PaymentsPlanned.Clear();
            //foreach (var condition in PaymentConditionsToDone)
            //{
            //    var payment = new PaymentPlan { Cost = new Cost { Cost = Cost.Cost * condition.Part / 100, Vat = Cost.Vat } };

            //    //дата платежа
            //    if (condition.PaymentConditionPoint == PaymentConditionPoint.ProductionStart) payment.Date = ProductionStartDateCalculated.AddDays(condition.DaysToPoint);
            //    if (condition.PaymentConditionPoint == PaymentConditionPoint.ProductionEnd) payment.Date = ProductionEndDateCalculated.AddDays(condition.DaysToPoint);
            //    if (condition.PaymentConditionPoint == PaymentConditionPoint.Shipment) payment.Date = ProductShipmentUnit.ShipmentDateCalculated.AddDays(condition.DaysToPoint);
            //    if (condition.PaymentConditionPoint == PaymentConditionPoint.Delivery) payment.Date = ProductShipmentUnit.DeliveryDateCalculated.AddDays(condition.DaysToPoint);

            //    var paymentWrapper = WrappersFactory.GetWrapper<PaymentPlanWrapper>(payment);
            //    PaymentsPlanned.Add(paymentWrapper);
            //}
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
        public CostWrapper SumPaid => WrappersFactory.GetWrapper<CostWrapper>(new Cost { Sum = Payments.Where(x => x.Document != null).Sum(x => x.Cost.Sum) });

        /// <summary>
        /// Неоплаченная сумма
        /// </summary>
        public CostWrapper SumDontPaid => WrappersFactory.GetWrapper<CostWrapper>(new Cost { Sum = Cost.Sum - SumPaid.Sum });

        /// <summary>
        /// Сумама, необходимая для начала производства
        /// </summary>
        public CostWrapper SumToStartProduction
        {
            get
            {
                //условия, связанные с датой запуска производства
                var conditions = PaymentsConditions.Where(x => x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart && x.DaysToPoint <= 0);
                var sum = conditions.Sum(condition => Cost.Sum*condition.Part);
                return WrappersFactory.GetWrapper<CostWrapper>(new Cost {Sum = sum});
            }
        }

        /// <summary>
        /// Сумма, необходимая для отгрузки
        /// </summary>
        public CostWrapper SumToShipping
        {
            get
            {
                //условия, связанные с датой отгрузки
                var conditions = PaymentsConditions.Where(x =>
                    (x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart) ||
                    (x.PaymentConditionPoint == PaymentConditionPoint.ProductionEnd) ||
                    (x.PaymentConditionPoint == PaymentConditionPoint.Shipment && x.DaysToPoint <= 0));
                var sum = conditions.Sum(condition => Cost.Sum*condition.Part);
                return WrappersFactory.GetWrapper<CostWrapper>(new Cost {Sum = sum});
            }
        }

        #endregion

        #region Dates

        public DateTime OrderInTakeDate
        {
            get
            {
                //по дате запуска производства
                if (StartProductionDate.HasValue) return StartProductionDate.Value;
                return ProductionStartDateCalculated;
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
                    sum += payment.Cost.Sum;
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
                foreach (var payment in Payments)
                {
                    sum += payment.Cost.Sum;
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
                return ProductShipmentUnit.DeliveryDateCalculated;
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
        public double MarginalIncomeSingle => Cost.Sum - Product.GetTotalPrice(MarginalIncomeDate);

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

        public IEnumerable<PaymentWrapper> PaymentsPlanned => Payments.Where(x => x.Document == null);

        public void ReGeneratePlanPaymentsHard()
        {
            PaymentsPlanned.ToList().ForEach(x => Payments.Remove(x));
            foreach (var condition in PaymentConditionsToDone)
            {
                var payment = new Payment { Cost = new Cost { Sum = Cost.Sum * condition.Part } };

                //дата платежа
                if (condition.PaymentConditionPoint == PaymentConditionPoint.ProductionStart) payment.Date = ProductionStartDateCalculated.AddDays(condition.DaysToPoint);
                if (condition.PaymentConditionPoint == PaymentConditionPoint.ProductionEnd) payment.Date = ProductionEndDateCalculated.AddDays(condition.DaysToPoint);
                if (condition.PaymentConditionPoint == PaymentConditionPoint.Shipment) payment.Date = ProductShipmentUnit.ShipmentDateCalculated.AddDays(condition.DaysToPoint);
                if (condition.PaymentConditionPoint == PaymentConditionPoint.Delivery) payment.Date = ProductShipmentUnit.DeliveryDateCalculated.AddDays(condition.DaysToPoint);

                var paymentWrapper = WrappersFactory.GetWrapper<PaymentWrapper>(payment);
                Payments.Add(paymentWrapper);
            }
        }

        public void ReGeneratePlanPaymentsLight()
        {
            //если плановых платежей еще нет, либо был удален совершенный платеж.
            if (!PaymentsPlanned.Any() || SumDontPaid.Sum > PaymentsPlanned.Sum(x => x.Cost.Sum))
            {
                ReloadPaymentsPlannedFull();
                return;
            }

            var paymentsToRemove = new List<PaymentWrapper>(PaymentsPlanned);
            var sumRest = SumDontPaid.Sum;
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