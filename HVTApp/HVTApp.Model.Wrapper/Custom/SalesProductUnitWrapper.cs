using System;
using System.Collections.Specialized;
using System.Linq;

namespace HVTApp.Model.Wrapper
{
    public partial class SalesProductUnitWrapper
    {
        protected override void RunInConstructor()
        {
            this.PaymentsActual.CollectionChanged += PaymentsActualOnCollectionChanged;
        }

        private void PaymentsActualOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            
        }

        public void ReloadPaymentsPlanned()
        {
            PaymentsPlanned.Clear();

            foreach (var condition in PaymentsConditions)
            {
                Payment payment = new Payment {SumAndVat = new SumAndVat { Sum = Cost.SumWithVat*condition.PartInPercent }};
                PaymentsPlanned.Add(PaymentWrapper.GetWrapper(payment));
            }
        }
    }
}
