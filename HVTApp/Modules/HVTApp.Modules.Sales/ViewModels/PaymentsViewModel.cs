using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HVTApp.DataAccess.Annotations;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class PaymentsViewModel : NotifyPropertyChanged
    {
        private readonly IUnityContainer _container;
        private IUnitOfWork UnitOfWork { get; }
        private IValidatableChangeTrackingCollection<SalesUnitWrapper> _salesUnitWrappers;
        private bool _isLoaded = false;
        public ObservableCollection<PaymentWrapper> Payments { get; } = new ObservableCollection<PaymentWrapper>();

        public bool IsLoaded
        {
            get { return _isLoaded; }
            set
            {
                _isLoaded = value;
                OnPropertyChanged();
            }
        }

        public PaymentsViewModel(IUnityContainer container)
        {
            _container = container;
            UnitOfWork = container.Resolve<IUnitOfWork>();
        }

        public async Task LoadAsync()
        {
            //загружаем все юниты
            var salesUnitWrappers = (await UnitOfWork.GetRepository<SalesUnit>().GetAllAsync()).Select(x => new SalesUnitWrapper(x));

            //фиксируем их в коллекции для отслеживания изменений
            _salesUnitWrappers = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(salesUnitWrappers);

            foreach (var salesUnitWrapper in _salesUnitWrappers)
            {
                //платежи, находящиеся в юните
                foreach (var paymentPlannedWrapper in salesUnitWrapper.PaymentsPlanned)
                {
                    Payments.Add(new PaymentWrapper(paymentPlannedWrapper, salesUnitWrapper, true));
                }

                //сгенерированные платежи
                var lookup = new SalesUnitLookup(salesUnitWrapper.Model);
                await lookup.LoadOther(UnitOfWork);
                var paymentLookupsToAdd = lookup.PaymentPlannedWithSaved.Except(lookup.PaymentsPlanned);
                foreach (var paymentPlannedLookup in paymentLookupsToAdd)
                {
                    Payments.Add(new PaymentWrapper(new PaymentPlannedWrapper(paymentPlannedLookup.Entity), salesUnitWrapper, false));
                }
            }

            IsLoaded = true;
        }

    }

    public class PaymentWrapper : NotifyPropertyChanged
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

        public PaymentWrapper(PaymentPlannedWrapper paymentPlannedWrapper, SalesUnitWrapper salesUnit, bool inUnit)
        {
            SalesUnit = salesUnit;
            this.InUnit = inUnit;
            WillSave = inUnit;
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
    }

    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
