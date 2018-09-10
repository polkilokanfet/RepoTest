using System.ComponentModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Prism.Mvvm;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class PaymentWrapper : BindableBase
    {
        private bool _willSave;

        public PaymentPlannedWrapper PaymentPlannedWrapper { get; }
        public SalesUnitWrapper SalesUnit { get; }

        /// <summary>
        /// был ли платеж изначально сохранен в юните
        /// </summary>
        private bool InUnit { get; }

        /// <summary>
        /// будет ли платеж сохранен
        /// </summary>
        public bool WillSave
        {
            get { return _willSave; }
            private set
            {
                _willSave = value;
                OnPropertyChanged();
            }
        }

        public PaymentWrapper(PaymentPlannedWrapper paymentPlannedWrapper, SalesUnitWrapper salesUnit, bool inUnit, bool willSave)
        {
            SalesUnit = salesUnit;
            this.InUnit = inUnit;
            WillSave = willSave;
            PaymentPlannedWrapper = paymentPlannedWrapper;

            PaymentPlannedWrapper.Sum = paymentPlannedWrapper.Part * paymentPlannedWrapper.Condition.Part * SalesUnit.Cost;

            //подписка на событие изменения свойств
            PaymentPlannedWrapper.PropertyChanged += PaymentPlannedWrapperOnPropertyChanged;
        }

        private void PaymentPlannedWrapperOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (!Equals(args.PropertyName, nameof(PaymentPlannedWrapper.Date)))
                return;

            //если дата изменилась, платеж нужно запомнить
            if (PaymentPlannedWrapper.IsChanged)
            {
                //если он уже не добавлен
                if (SalesUnit.PaymentsPlanned.Contains(PaymentPlannedWrapper))
                    return;
                SalesUnit.PaymentsPlanned.Add(PaymentPlannedWrapper);
                WillSave = true;
            }
            else
            {
                //если изменения убрали - забыть платеж, если он не был запомнен изначально
                if (SalesUnit.PaymentsPlanned.Contains(PaymentPlannedWrapper) && !InUnit)
                {
                    SalesUnit.PaymentsPlanned.Remove(PaymentPlannedWrapper);
                    WillSave = false;
                }
            }
        }

        public void Remove(IUnitOfWork unitOfWork)
        {
            PaymentPlannedWrapper.RejectChanges();

            if (SalesUnit.PaymentsPlanned.Contains(PaymentPlannedWrapper))
                SalesUnit.PaymentsPlanned.Remove(PaymentPlannedWrapper);

            if(unitOfWork.GetRepository<PaymentPlanned>().Find(x => Equals(x, PaymentPlannedWrapper.Model)).Any())
                unitOfWork.GetRepository<PaymentPlanned>().Delete(PaymentPlannedWrapper.Model);
        }

        public void UnSubskribe()
        {
            PaymentPlannedWrapper.PropertyChanged -= PaymentPlannedWrapperOnPropertyChanged;
        }
    }
}