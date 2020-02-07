using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.PlanAndEconomy.ViewModels
{
    public class PaymentsPlanGroup : BindableBase
    {
        private DateTime _date;
        public SalesUnit SalesUnit { get; }
        public PaymentPlanned PaymentPlanned { get; }
        public int Amount { get; }
        public double Sum { get; }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (Equals(_date, value)) return;
                _date = value;
                OnPropertyChanged(nameof(Week));
                OnPropertyChanged(nameof(Month));
                OnPropertyChanged(nameof(Year));
                OnPropertyChanged(nameof(Days));
                OnPropertyChanged(nameof(PaymentType));
            }
        }

        public int Week => Date.WeekNumber();
        public string Month => Date.MonthName();
        public int Year => Date.Year;

        public DateTime DateContract => SalesUnit.GetPaymentDate(PaymentPlanned.Condition);
        public int Days => (Date - DateContract).Days;

        public Company Contragent => Contract?.Contragent ?? SalesUnit.Facility.OwnerCompany;
        public Contract Contract => SalesUnit.Specification?.Contract;
        public Region Region => Facility.GetRegion();
        public Facility Facility => SalesUnit.Facility;

        public string Rf
        {
            get
            {
                if (Region != null)
                {
                    return Region.District.Country.Name == "Ðîññèÿ" ? "ÐÔ" : "Ýêñïîðò";
                }
                return string.Empty;
            }
        }

        public string PaymentType
        {
            get
            {
                var realization = SalesUnit.RealizationDateCalculated;
                if (Date <= realization && Date.Year == realization.Year && Date.Month == realization.Month)
                    return "ÒÏ";
                return Date < realization ? "ÀÂ" : "ÄÇ";
            }
        }

        public string Currency => "rub";

        public PaymentsPlanGroup(IEnumerable<Payment1> payments1)
        {
            var payments = payments1.ToList();
            SalesUnit = payments.First().SalesUnit;
            PaymentPlanned = payments.First().PaymentPlanned;
            Amount = payments.Count;
            var vat = SalesUnit.Specification?.Vat ?? 20;
            Sum = SalesUnit.Cost * PaymentPlanned.Condition.Part * PaymentPlanned.Part * (1.0 + vat / 100.0) * Amount;
            _date = PaymentPlanned.Date;
        }
    }
}