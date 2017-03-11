using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HVTApp.Model.PaymentsCollections
{
    public abstract class PaymentsCollectionBase<T> : ICollection<T> 
        where T : IPaymentBase
    {
        #region ICollection
        protected readonly List<T> Payments = new List<T>();

        public IEnumerator<T> GetEnumerator()
        {
            return Payments.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Payments).GetEnumerator();
        }

        public virtual void Add(T item)
        {
            Payments.Add(item);

            if (item.Id == 0)
                OnCollectionChanged();
        }

        public void Clear()
        {
            Payments.Clear();
            OnCollectionChanged();
        }

        public bool Contains(T item)
        {
            return Payments.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Payments.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            var result = Payments.Remove(item);
            OnCollectionChanged();
            return result;
        }

        public int Count => Payments.Count;

        public bool IsReadOnly => false;
        #endregion

        protected PaymentsCollectionBase(PaymentsInfo paymentsInfo)
        {
            PaymentsInfo = paymentsInfo;
        }

        /// <summary>
        /// Сумма платежей.
        /// </summary>
        public double TotalSum => Payments.Sum(x => x.Sum);

        /// <summary>
        /// Продукт, с которым связана коллекция.
        /// </summary>
        protected PaymentsInfo PaymentsInfo { get; }


        /// <summary>
        /// Событие изменения коллекции.
        /// </summary>
        public event EventHandler CollectionChanged;

        protected virtual void OnCollectionChanged()
        {
            CollectionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}