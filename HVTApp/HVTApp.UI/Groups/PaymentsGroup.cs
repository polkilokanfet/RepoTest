using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.UI.Modules.Sales.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Mvvm;

namespace HVTApp.UI.Groups
{
    public class PaymentsGroup : BindableBase
    {
        private readonly PaymentWrapper _payment;

        public ObservableCollection<PaymentsGroup> Groups { get; }

        public IEnumerable<Guid> Ids { get; }

        public int Amount => Groups?.Count ?? 1;

        public double Sum => Groups?.Sum(x => x.Sum) ?? _payment.Sum;

        public string Position => Groups == null ? SalesUnit.Model.OrderPosition : "...";

        public SalesUnitPaymentsPlannedWrapper SalesUnit { get; set; }
        public PaymentConditionWrapper Condition => _payment.PaymentPlanned.Condition;

        public bool? WillSave
        {
            get
            {
                if (Groups == null) return _payment.IsInPlanPayments;
                if (Groups.All(x => x.WillSave.HasValue && x.WillSave.Value)) return true;
                if (Groups.All(x => x.WillSave.HasValue && !x.WillSave.Value)) return false;
                return null;
            }
        }

        public int Year => Date.Year;
        public string Month => Date.MonthName();
        public int Week => Date.WeekNumber();

        public DateTime Date
        {
            get { return _payment.PaymentPlanned.Date; }
            set
            {
                if (value < DateTime.Today) return;

                if (Groups == null)
                {
                    _payment.PaymentPlanned.Date = value;
                    OnPropertyChanged();
                }

                Groups?.ForEach(x => x.DateChanged -= OnGroupDateChanged);
                Groups?.ForEach(x => x.Date = value);
                Groups?.ForEach(x => x.DateChanged += OnGroupDateChanged);

                DateChanged?.Invoke(this);
            }
        }

        public event Action<PaymentsGroup> DateChanged; 

        public PaymentsGroup(IEnumerable<PaymentWrapper> payments)
        {
            var paymentWrappers = payments as PaymentWrapper[] ?? payments.ToArray();

            _payment = paymentWrappers.First();
            SalesUnit = _payment.SalesUnit;
            Ids = payments.Select(x => x.PaymentPlanned.Id);

            if (paymentWrappers.Length > 1)
            {
                Groups = new ObservableCollection<PaymentsGroup>(paymentWrappers.Select(x => new PaymentsGroup(new List<PaymentWrapper> {x})));
                Groups.ForEach(x => x.DateChanged += OnGroupDateChanged);
            }

            paymentWrappers.ForEach(x => x.PropertyChanged += OnPaymentPropertyChanged);
        }

        private void OnPaymentPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            OnPropertyChanged(nameof(WillSave));
        }

        private void OnGroupDateChanged(PaymentsGroup group)
        {
            //if(group.Date != Date)
                DateChanged?.Invoke(group);
        }

        public void RemoveSubscribes()
        {
            Groups?.ForEach(x => x.DateChanged -= OnGroupDateChanged);
            _payment.PropertyChanged -= OnPaymentPropertyChanged;
            _payment.UnSubsñribe();
        }

        public void RemovePayments(IUnitOfWork unitOfWork)
        {
            _payment.Remove(unitOfWork);
            Groups?.ForEach(x => x.RemovePayments(unitOfWork));
            OnPropertyChanged(nameof(WillSave));
        }
    }
}