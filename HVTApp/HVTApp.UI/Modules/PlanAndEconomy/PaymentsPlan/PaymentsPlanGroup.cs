using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.PlanAndEconomy.PaymentsPlan
{
    public class PaymentsPlanGroup : BindableBase
    {
        private DateTime _date;
        public SalesUnit SalesUnit { get; }
        public PaymentPlanned PaymentPlanned { get; }
        public int Amount { get; }

        /// <summary>
        /// Ñóììà ñ ÍÄÑ
        /// </summary>
        public double Sum { get; }

        /// <summary>
        /// Ñóììà áåç ÍÄÑ
        /// </summary>
        public double SumWithoutVat { get; }

        public DateTime Date
        {
            get => _date;
            set
            {
                if (Equals(_date, value)) return;
                _date = value;
                RaisePropertyChanged(nameof(Week));
                RaisePropertyChanged(nameof(Month));
                RaisePropertyChanged(nameof(Year));
                RaisePropertyChanged(nameof(Days));
                RaisePropertyChanged(nameof(PaymentType));
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

        public string OrderNumber { get; }

        public PaymentsPlanGroup(IEnumerable<Payment1> payments1)
        {
            var payments = payments1.ToList();
            SalesUnit = payments.First().SalesUnit;
            PaymentPlanned = payments.First().PaymentPlanned;
            Amount = payments.Count;
            SumWithoutVat = SalesUnit.Cost * PaymentPlanned.Condition.Part * PaymentPlanned.Part * Amount;
            var vat = SalesUnit.Specification?.Vat ?? 20;
            Sum = SumWithoutVat * (1.0 + vat / 100.0);
            _date = PaymentPlanned.Date;
            OrderNumber = SalesUnit.Order?.Number;
        }
    }
}