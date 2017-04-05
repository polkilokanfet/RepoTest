using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace HVTApp.Model.Wrapper
{
    public partial class ProductWrapper
    {
        protected override void RunInConstructor()
        {
            PaymentsInfo.PaymentsActual.CollectionChanged += PaymentsActualOnCollectionChanged;
            CostInfo.PropertyChanged += CostInfoOnPropertyChanged;
        }

        /// <summary>
        /// Реакция на событие изменения коллекции фактических платежей.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="notifyCollectionChangedEventArgs"></param>
        private void PaymentsActualOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            var planedPayments = this.PaymentsInfo.PaymentsPlanned;
        }

        private void CostInfoOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            
        }
    }
}
