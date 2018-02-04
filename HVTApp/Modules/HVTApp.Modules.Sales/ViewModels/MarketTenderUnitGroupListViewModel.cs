using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;
using HVTApp.UI.Events;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class MarketTenderUnitGroupListViewModel : TenderUnitGroupListViewModel
    {
        public MarketTenderUnitGroupListViewModel(IUnityContainer container) : base(container)
        {
            Container.Resolve<IEventAggregator>().GetEvent<AfterSelectTenderEvent>().Subscribe(OnTenderSelect);
        }

        private Tender _tender;

        private async void OnTenderSelect(PubSubEventArgs<Tender> e)
        {
            if (!Equals(e.Sender.GetType(), typeof(MarketTenderListViewModel))) return;
            _tender = e.Entity;
            await LoadAsync();
        }

        protected override async Task<IEnumerable<TenderUnitGroup>> GetItems()
        {
            var result = new List<TenderUnitGroup>();
            if (_tender == null) return result;

            var tenderUnits = await UnitOfWork.GetRepository<TenderUnit>().GetAllAsNoTrackingAsync();
            tenderUnits = tenderUnits.FindAll(x => Equals(x.Tender, _tender));
            var groups = tenderUnits.GroupBy(x => x, new TenderUnitComparer());
            foreach (var group in groups)
                result.Add(new TenderUnitGroup { TenderUnits = group.ToList() });
            return result;
        }
    }
}