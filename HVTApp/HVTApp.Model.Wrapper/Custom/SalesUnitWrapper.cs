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

        }

        /// <summary>
        /// Еще не исполненные платежные условия
        /// </summary>
        public List<PaymentConditionWrapper> PaymentConditionsToDone
        {
            get
            {
                var conditions = PaymentsConditions.OrderBy(x => x.PaymentConditionPoint).ThenBy(x => x.DaysToPoint).ToList();
                var paidSum = PaymentsActual.Sum(x => x.SumAndVat.Sum);
                double conditionSum = Cost.Sum * conditions.First().PartInPercent / 100;
                while (conditionSum <= paidSum)
                {
                    conditions.Remove(conditions.First());
                    conditionSum += Cost.Sum * conditions.First().PartInPercent / 100;
                }

                if (conditions.Any())
                {
                    var condition = conditions.First();
                    var oldSum = condition.PartInPercent/100*Cost.Sum;
                    var dSum = conditions.Sum(x => x.PartInPercent/100*Cost.Sum) - paidSum;
                    if (Math.Abs(dSum) > 0.00001)
                    {
                        var newPart = ((oldSum - dSum)*condition.PartInPercent)/oldSum;
                        conditions.Remove(condition);
                        conditions.Add(PaymentConditionWrapper.GetWrapper(new PaymentCondition {PartInPercent = newPart, PaymentConditionPoint = condition.PaymentConditionPoint, DaysToPoint = condition.DaysToPoint}));

                        return conditions.OrderBy(x => x.PaymentConditionPoint).ThenBy(x => x.DaysToPoint).ToList();
                    }
                    return conditions;
                }
                return new List<PaymentConditionWrapper>();
            }
        }

        public void ReloadPaymentsPlanned()
        {
            PaymentsPlanned.Clear();

            foreach (var condition in PaymentConditionsToDone)
            {
                var payment = new Payment();
                payment.SumAndVat = new SumAndVat { Sum = Cost.Sum*condition.PartInPercent/100, Vat = Cost.Vat };

                PaymentsPlanned.Add(PaymentWrapper.GetWrapper(payment));
            }
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

        public List<PaymentWrapper> AllPayments => PaymentsActual.Concat(PaymentsPlanned).OrderBy(x => x.Date).ToList();

        public DateTime? StartProductionConditionsDoneDate
        {
            get
            {
                double sum = 0;
                foreach (var payment in AllPayments)
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
                foreach (var payment in AllPayments)
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
