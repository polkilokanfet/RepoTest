using System;
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

            this.MarginalIncome.PropertyChanged += MarginalIncomeOnPropertyChanged;
            this.PropertyChanged += OnMarginalIncomeInPercentChanged;
        }

        private void MarginalIncomeOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MarginalIncome.Date))
                CalculateMarginalIncome();
        }

        private void PaymentsActualOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            
        }

        public void ReloadPaymentsPlanned()
        {
            PaymentsPlanned.Clear();

            foreach (var condition in PaymentsConditions.OrderBy(x => x.PaymentConditionPoint))
            {
                Payment payment = new Payment { SumAndVat = new SumAndVat { Sum = Cost.Sum * condition.PartInPercent / 100, Vat = this.Cost.Vat } };
                PaymentsPlanned.Add(PaymentWrapper.GetWrapper(payment));
            }
        }
    }
}
