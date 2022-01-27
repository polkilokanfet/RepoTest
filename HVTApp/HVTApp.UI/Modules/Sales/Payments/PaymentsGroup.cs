using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.Wrapper;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Sales.Payments
{
    public class PaymentsGroup : BindableBase
    {
        public ObservableCollection<PaymentWrapper> Payments { get; }

        public int Amount => Payments.Count;

        public double Sum => Payments.Sum(paymentWrapper => paymentWrapper.Sum);

        public SalesUnitWrapper1 SalesUnit => Payments.First().SalesUnit;

        public PaymentConditionWrapper Condition => Payments.First().PaymentPlanned.Condition;

        public bool? IsCustom
        {
            get
            {
                if (Payments.All(paymentWrapper => paymentWrapper.IsInPlanPayments)) return true;
                if (Payments.All(paymentWrapper => !paymentWrapper.IsInPlanPayments)) return false;
                return null;
            }
        }

        public int Year => Date.Year;
        public string Month => Date.MonthName();
        public string Week => Date.WeekNumberString();

        public DateTime Date
        {
            get { return Payments.Min(paymentWrapper => paymentWrapper.PaymentPlanned.Date); }
            set
            {
                if (value < DateTime.Today) return;
                if (value > DateTime.Today.AddYears(50)) return;
                Payments.ForEach(paymentWrapper => paymentWrapper.Date = value);
                RaisePropertyChanged(nameof(IsCustom));
            }
        }

        public PaymentsGroup(IEnumerable<PaymentWrapper> payments)
        {
            Payments = new ObservableCollection<PaymentWrapper>(payments);
        }

        public void RemovePayments(IUnitOfWork unitOfWork)
        {
            Payments.ForEach(paymentWrapper => paymentWrapper.Remove(unitOfWork));
            RaisePropertyChanged(nameof(Date));
            RaisePropertyChanged(nameof(IsCustom));
        }
    }
}