using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;

namespace HVTApp.UI.ViewModels
{
    public class PaymentConditionFilterViewModel
    {
        private readonly PaymentConditionPointEnum _point;
        private double? _part;
        private int? _daysTo;

        public double? Part
        {
            get => _part;
            set
            {
                if (Equals(_part, value)) return;
                _part = value;
                this.IsChanged?.Invoke();
            }
        }

        public int? DaysTo
        {
            get => _daysTo;
            set
            {
                if (Equals(_daysTo, value)) return;
                _daysTo = value;
                this.IsChanged?.Invoke();
            }
        }

        public PaymentConditionFilter PaymentConditionFilter
        {
            get
            {
                if (Part.HasValue && DaysTo.HasValue)
                    return new PaymentConditionFilter(_point, Part.Value / 100.0, DaysTo.Value);

                if (Part.HasValue)
                    return new PaymentConditionFilter(_point, Part.Value / 100.0);

                if (DaysTo.HasValue)
                    return new PaymentConditionFilter(_point, DaysTo.Value);

                return null;
            }
        }

        public PaymentConditionFilterViewModel(PaymentConditionPointEnum point)
        {
            _point = point;
        }

        public event Action IsChanged;
    }
    public partial class PaymentConditionSetLookupListViewModel
    {
        private List<PaymentConditionSetLookup> _allPaymentConditionSets;

        public PaymentConditionFilterViewModel PaymentConditionFilterViewModelStartProduction { get; } 
            = new PaymentConditionFilterViewModel(PaymentConditionPointEnum.ProductionStart);

        private List<PaymentConditionFilter> PaymentConditionFilterList
        {
            get
            {
                List<PaymentConditionFilter> result = new List<PaymentConditionFilter>();
                if (PaymentConditionFilterViewModelStartProduction.PaymentConditionFilter != null)
                    result.Add(PaymentConditionFilterViewModelStartProduction.PaymentConditionFilter);
                return result;
            }
        }

        protected override void SubscribesToEvents()
        {
            this.Loaded += () =>
            {
                _allPaymentConditionSets = Lookups.ToList();
            };
        }

        private void ToFilter()
        {
            List<PaymentConditionSetLookup> filteredSets = _allPaymentConditionSets.ToList();

            foreach (var conditionFilter in this.PaymentConditionFilterList)
            {
                foreach (var set in filteredSets.ToList())
                {
                    if (set.PaymentConditions.All(x => !conditionFilter.Includes(x.Entity)))
                    {
                        filteredSets.Remove(set);
                    }
                }
            }

            this.LookupsCollection.Clear();
            this.LookupsCollection.AddRange(filteredSets);
        }
    }
}