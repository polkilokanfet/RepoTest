using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;

namespace HVTApp.UI.ViewModels
{
    public partial class PaymentConditionSetLookupListViewModel
    {
        private List<PaymentConditionSetLookup> _allPaymentConditionSets;

        public PaymentConditionFilterViewModel PaymentConditionFilterViewModelStartProduction { get; }
            = new PaymentConditionFilterViewModel(PaymentConditionPointEnum.ProductionStart);

        public PaymentConditionFilterViewModel PaymentConditionFilterViewModelFinishProduction { get; }
            = new PaymentConditionFilterViewModel(PaymentConditionPointEnum.ProductionEnd);

        private List<PaymentConditionFilter> PaymentConditionFilterList
        {
            get
            {
                List<PaymentConditionFilter> result = new List<PaymentConditionFilter>();
                if (PaymentConditionFilterViewModelStartProduction.PaymentConditionFilter != null)
                    result.Add(PaymentConditionFilterViewModelStartProduction.PaymentConditionFilter);
                if (PaymentConditionFilterViewModelFinishProduction.PaymentConditionFilter != null)
                    result.Add(PaymentConditionFilterViewModelFinishProduction.PaymentConditionFilter);
                return result;
            }
        }

        protected override void SubscribesToEvents()
        {
            this.Loaded += () =>
            {
                _allPaymentConditionSets = Lookups.ToList();
            };

            this.PaymentConditionFilterViewModelStartProduction.IsChanged += ToFilter;
            this.PaymentConditionFilterViewModelFinishProduction.IsChanged += ToFilter;
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

            if (this.SelectedLookup != null)
            {
                if (!this.Lookups.Contains(this.SelectedLookup))
                {
                    SelectedLookup = null;
                }
            }
        }
    }
}