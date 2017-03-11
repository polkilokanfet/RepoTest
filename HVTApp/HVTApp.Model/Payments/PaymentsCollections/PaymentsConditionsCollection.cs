using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HVTApp.Model.PaymentsCollections
{
    public class PaymentsConditionsCollection : ICollection<PaymentsCondition>
    {
        #region ICollection
        private readonly List<PaymentsCondition> _paymentConditions = new List<PaymentsCondition>();
        public IEnumerator<PaymentsCondition> GetEnumerator()
        {
            return _paymentConditions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public void Clear()
        {
            _paymentConditions.Clear();
            OnCollectionChanged();
        }

        public bool Contains(PaymentsCondition item)
        {
            return _paymentConditions.Contains(item);
        }

        public void CopyTo(PaymentsCondition[] array, int arrayIndex)
        {
            _paymentConditions.CopyTo(array, arrayIndex);
        }

        public bool Remove(PaymentsCondition item)
        {
            var result = _paymentConditions.Remove(item);
            OnCollectionChanged();
            return result;
        }

        public int Count => _paymentConditions.Count;

        public bool IsReadOnly => false;
        #endregion



        /// <summary>
        /// Добавление нового платежного условия в коллекцию.
        /// </summary>
        /// <param name="item">Платежное условие.</param>
        public void Add(PaymentsCondition item)
        {
            double totalPart = _paymentConditions.Select(x => x.PartInPercent).Sum();

            if (totalPart >= 100)
                return;

            if ((totalPart + item.PartInPercent) > 100)
                item.PartInPercent = 100 - totalPart;

            _paymentConditions.Add(item);

            //если добавлен новый платеж бросаем событие изменения коллекции.
            //проверка на Id нужна для того, чтобы не бросать событие при генерации сущности из Entity Framework
            if (item.Id == 0)
                OnCollectionChanged();
        }

        /// <summary>
        /// Событие изменения коллекции.
        /// </summary>
        public event EventHandler CollectionChanged;

        /// <summary>
        /// Формирует платежные условия по образцу.
        /// </summary>
        /// <param name="paymentConditionsTemplate">Образец по которому необходимо сформировать текущие платежные условия.</param>
        public void MakeConditionsByTemplate(StandartPaymentConditions paymentConditionsTemplate)
        {
            this.Clear();
            foreach (PaymentsCondition paymentCondition in paymentConditionsTemplate.PaymentsConditionsCollection)
            {
                this.Add(new PaymentsCondition
                {
                    PartInPercent = paymentCondition.PartInPercent,
                    DaysToPoint = paymentCondition.DaysToPoint,
                    PaymentConditionPoint = paymentCondition.PaymentConditionPoint
                });
            }
        }

        protected virtual void OnCollectionChanged()
        {
            CollectionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}