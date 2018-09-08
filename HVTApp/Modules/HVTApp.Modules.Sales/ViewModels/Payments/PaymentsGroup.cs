using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Mvvm;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class PaymentsGroup : BindableBase
    {
        private readonly List<PaymentWrapper> _payments;
        private DateTime _date;
        private bool _willSave;

        public int Amount => _payments.Count;

        public double Sum => Amount * SalesUnit.Cost;

        public SalesUnitWrapper SalesUnit { get; set; }
        public PaymentConditionWrapper Condition { get; }

        public bool WillSave
        {
            get { return _willSave; }
            private set
            {
                _willSave = value;
                OnPropertyChanged();
            }
        }

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
                OnPropertyChanged();
            }
        }

        public ObservableCollection<PaymentsGroup> Groups { get; } = new ObservableCollection<PaymentsGroup>();

        public PaymentsGroup(IEnumerable<PaymentWrapper> payments)
        {
            _payments = payments.ToList();
            _date = payments.First().PaymentPlannedWrapper.Date;
            WillSave = payments.First().WillSave;
            Condition = payments.First().PaymentPlannedWrapper.Condition;
            SalesUnit = payments.First().SalesUnit;


            if(payments.Count() > 1)
                Groups.AddRange(payments.Select(x => new PaymentsGroup(new List<PaymentWrapper> {x})));

            foreach (var paymentWrapper in _payments)
            {
                paymentWrapper.PropertyChanged += (sender, args) => WillSave = _payments.All(x => x.WillSave);
            }
        }

        public void RemovePayments(IUnitOfWork unitOfWork)
        {
            if (Groups.Any())
            {
                Groups.ForEach(x => x.RemovePayments(unitOfWork));
                WillSave = false;
                return;
            }

            foreach (var paymentWrapper in _payments)
            {
                paymentWrapper.PaymentPlannedWrapper.RejectChanges();
                if (paymentWrapper.SalesUnit.PaymentsPlanned.Contains(paymentWrapper.PaymentPlannedWrapper))
                    paymentWrapper.SalesUnit.PaymentsPlanned.Remove(paymentWrapper.PaymentPlannedWrapper);
                unitOfWork.GetRepository<PaymentPlanned>().Delete(paymentWrapper.PaymentPlannedWrapper.Model);
            }
            WillSave = false;
        }
    }
}