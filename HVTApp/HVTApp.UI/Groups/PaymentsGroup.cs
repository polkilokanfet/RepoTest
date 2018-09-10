using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Mvvm;

namespace HVTApp.UI.Groups
{
    public class PaymentsGroup : BindableBase
    {
        private readonly List<PaymentWrapper> _payments;
        private DateTime _date;
        private bool _willSave;

        public IEnumerable<Guid> Ids => _payments.Select(x => x.PaymentPlannedWrapper.Id);

        public int Amount => _payments.Count;

        public double Sum => Amount * SalesUnit.Cost;

        public SalesUnitWrapper SalesUnit { get; set; }
        public PaymentConditionWrapper Condition { get; }

        public bool WillSave
        {
            get { return _willSave; }
            private set
            {
                if (Equals(_willSave, value)) return;
                _willSave = value;
                WillSaveChanged?.Invoke(this);
                OnPropertyChanged();
            }
        }

        public event Action<PaymentsGroup> WillSaveChanged; 

        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (Equals(_date, value)) return;
                if (value < DateTime.Today) return;
                _date = value;
                if(Groups.Any())
                    Groups.ForEach(x => x.Date = value);
                else
                    _payments.ForEach(x => x.PaymentPlannedWrapper.Date = value);
                DateChanged?.Invoke(this);
                OnPropertyChanged();
            }
        }

        public event Action<PaymentsGroup> DateChanged; 

        public ObservableCollection<PaymentsGroup> Groups { get; } = new ObservableCollection<PaymentsGroup>();

        public PaymentsGroup(IEnumerable<PaymentWrapper> payments)
        {
            _payments = payments.ToList();
            _date = _payments.First().PaymentPlannedWrapper.Date;
            WillSave = _payments.First().WillSave;
            Condition = _payments.First().PaymentPlannedWrapper.Condition;
            SalesUnit = _payments.First().SalesUnit;


            if (_payments.Count > 1)
            {
                Groups.AddRange(_payments.Select(x => new PaymentsGroup(new List<PaymentWrapper> {x})));
                Groups.ForEach(x => x.DateChanged += OnGroupDateChanged);
            }

            _payments.ForEach(x => x.PropertyChanged += OnPaymentPropertyChanged);
        }

        private void OnPaymentPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            WillSave = _payments.All(x => x.WillSave);
        }

        private void OnGroupDateChanged(PaymentsGroup group)
        {
            if(group.Date != Date)
                DateChanged?.Invoke(group);
        }

        public void RemoveSubscribes()
        {
            Groups.ForEach(x => x.DateChanged -= OnGroupDateChanged);
            _payments.ForEach(x => x.PropertyChanged -= OnPaymentPropertyChanged);
            _payments.ForEach(x => x.UnSubskribe());
        }

        public void RemovePayments(IUnitOfWork unitOfWork)
        {
            if (Groups.Any())
            {
                Groups.ForEach(x => x.RemovePayments(unitOfWork));
                WillSave = false;
                return;
            }

            _payments.ForEach(x => x.Remove(unitOfWork));
            WillSave = false;
        }
    }
}