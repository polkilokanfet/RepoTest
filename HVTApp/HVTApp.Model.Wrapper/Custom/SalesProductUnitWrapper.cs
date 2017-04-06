using System;
using System.Collections.Specialized;

namespace HVTApp.Model.Wrapper
{
    public partial class SalesProductUnitWrapper
    {
        protected override void RunInConstructor()
        {
            this.Payments.PaymentsActual.CollectionChanged += PaymentsActualOnCollectionChanged;
        }

        private void PaymentsActualOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            
        }
    }
}
