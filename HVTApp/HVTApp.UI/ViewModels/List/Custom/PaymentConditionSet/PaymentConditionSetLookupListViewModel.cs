using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.ViewModels
{
    public partial class PaymentConditionSetLookupListViewModel
    {
        private List<PaymentConditionSetLookup> _allPaymentConditionSets;

        public PaymentConditionFilterViewModel PaymentConditionFilterViewModelStartProduction { get; }
            = new PaymentConditionFilterViewModel(PaymentConditionPointEnum.ProductionStart);

        public PaymentConditionFilterViewModel PaymentConditionFilterViewModelFinishProduction { get; }
            = new PaymentConditionFilterViewModel(PaymentConditionPointEnum.ProductionEnd);

        public PaymentConditionFilterViewModel PaymentConditionFilterViewModelShipment { get; }
            = new PaymentConditionFilterViewModel(PaymentConditionPointEnum.Shipment);

        public PaymentConditionFilterViewModel PaymentConditionFilterViewModelDelivery { get; }
            = new PaymentConditionFilterViewModel(PaymentConditionPointEnum.Delivery);

        private List<PaymentConditionFilter> PaymentConditionFilterList
        {
            get
            {
                List<PaymentConditionFilter> result = new List<PaymentConditionFilter>();

                if (PaymentConditionFilterViewModelStartProduction.PaymentConditionFilter != null)
                    result.Add(PaymentConditionFilterViewModelStartProduction.PaymentConditionFilter);

                if (PaymentConditionFilterViewModelFinishProduction.PaymentConditionFilter != null)
                    result.Add(PaymentConditionFilterViewModelFinishProduction.PaymentConditionFilter);

                if (PaymentConditionFilterViewModelShipment.PaymentConditionFilter != null)
                    result.Add(PaymentConditionFilterViewModelShipment.PaymentConditionFilter);

                if (PaymentConditionFilterViewModelDelivery.PaymentConditionFilter != null)
                    result.Add(PaymentConditionFilterViewModelDelivery.PaymentConditionFilter);

                return result;
            }
        }

        protected override void InitSpecialCommands()
        {
            this.NewItemCommand = new DelegateLogCommand(
                () =>
                {
                    Container.Resolve<IUpdateDetailsService>().UpdateDetails(new PaymentConditionSet());
                });
        }
        protected override void LastActionInCtor()
        {
            Container.Resolve<IEventAggregator>().GetEvent<AfterSavePaymentConditionSetEvent>().Subscribe(
                paymentConditionsSet => 
                {
                    if(paymentConditionsSet != null && _allPaymentConditionSets.Any(x => x.Equals(paymentConditionsSet)) == false)
                    {
                        var set = this.UnitOfWork.Repository<PaymentConditionSet>().GetById(paymentConditionsSet.Id);
                        _allPaymentConditionSets.Add(new PaymentConditionSetLookup(set));
                    }
                });
        }
        protected override void SubscribesToEvents()
        {
            this.Loaded += () =>
            {
                _allPaymentConditionSets = Lookups.ToList();
            };

            this.PaymentConditionFilterViewModelStartProduction.IsChanged += ToFilter;
            this.PaymentConditionFilterViewModelFinishProduction.IsChanged += ToFilter;
            this.PaymentConditionFilterViewModelShipment.IsChanged += ToFilter;
            this.PaymentConditionFilterViewModelDelivery.IsChanged += ToFilter;
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