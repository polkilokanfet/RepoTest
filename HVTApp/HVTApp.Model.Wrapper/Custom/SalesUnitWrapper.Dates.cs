using System;
using System.ComponentModel;
using System.Linq;

namespace HVTApp.Model.Wrapper
{
    public partial class SalesUnitWrapper
    {
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
                var conditions = PaymentsConditions.Where(x => x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart && x.DaysToPoint <= 0);
                return conditions.Sum(condition => Cost.Sum * condition.PartInPercent / 100);
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
                return conditions.Sum(condition => Cost.Sum * condition.PartInPercent / 100);
            }
        }

        public DateTime? StartProductionConditionsDoneDate
        {
            get
            {
                var payments = PaymentsActual.Concat(PaymentsPlanned).OrderBy(x => x.Date);
                double sum = 0;
                foreach (var payment in payments)
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
                var payments = PaymentsActual.Concat(PaymentsPlanned).OrderBy(x => x.Date);
                double sum = 0;
                foreach (var payment in payments)
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
    }
}
